using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Domain;
using Persistence;
using System.Configuration;
using System.Timers;
using System.Threading;
using System.Data.SqlClient;
using System.Net;

namespace MSTPLFixedBarcodeReaderScannerApp
{
    public partial class BarcodeDetailsForm : Form
    {
        private User LoggedInUser;
        private BarcodeTransactionDAO btdao;
        private string ConnectionString;
        private UserDAO udao;
        private Mutex ThreadSafety;
        private delegate void UpdateDataGridView();
        private UpdateDataGridView UpdateDataGridViewDelegate;
        private Form1 CurrentLoggedInForm;
        private ThreadStart ts;
        Thread BarcodeTransactionUpdateThread;
        private delegate void CheckBarcodeTransDGVCellVals();
        private CheckBarcodeTransDGVCellVals CheckBarcodeTransDGVCellValsDel;
        private ThreadStart ts1;
        Thread BarcodeTransDGVContentChanger;
        private Int64 TimerElapsedPeriod = 0;
        private string DeviceIP;

        public BarcodeDetailsForm()
        {
            InitializeComponent();
        }

        public BarcodeDetailsForm(User user) : this()
        {
            try
            {
                this.LoggedInUser = user;
                ConnectionString = ConfigurationManager.ConnectionStrings["DBServerConnectionString"].ToString();
                btdao = new BarcodeTransactionDAO(ConnectionString);
                udao = new UserDAO(ConnectionString);

                ts = new ThreadStart(SetBarcodeTransactionUpdateTimer);
                BarcodeTransactionUpdateThread = new Thread(ts);
                BarcodeTransactionUpdateThread.Start();
                ThreadSafety = new Mutex();
                UpdateDataGridViewDelegate = new UpdateDataGridView(AppendBarcodeDataToBarcodeTransDGV);
                CheckBarcodeTransDGVCellValsDel = new CheckBarcodeTransDGVCellVals(ChangeBarcodeTransDGVCellValues);
                ts1 = new ThreadStart(BarcodeTransDGVCellValCheckTimer);
                BarcodeTransDGVContentChanger = new Thread(ts1);
                BarcodeTransDGVContentChanger.Start();
                if (!Int64.TryParse(ConfigurationManager.AppSettings["TimerElapsedPeriodInMilliSeconds"].ToString(), out TimerElapsedPeriod))
                {
                    throw new FormatException("Can not parse Timer elapsed period, hence Barcode transaction table won't be able to update automatically");
                }

                DeviceIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.AsEnumerable().Where(s => s.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(s => s).First().ToString();
                txt_ip.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                txt_ip.Text = DeviceIP;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured during BarcodeDetailsForm() execution: "+ex.Message, "Exception");
            }
        }

        public BarcodeDetailsForm(User user, Form1 f) : this(user)
        {
            CurrentLoggedInForm = f;
        }

        private void BarcodeDetailsForm_Load(object sender, EventArgs e)
        {
            txt_username.Text = LoggedInUser.UserName;
            PopulateBarcodeTransactionsGridView();
        }

        private void SetBarcodeTransactionUpdateTimer()
        {
            if (TimerElapsedPeriod > 0)
            {
                System.Timers.Timer timer = new System.Timers.Timer(TimerElapsedPeriod);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(PerformThreadSafeDGVUpdations);
                timer.Enabled = true;
            }
        }

        private void BarcodeTransDGVCellValCheckTimer()
        {
            if (TimerElapsedPeriod > 0)
            {
                System.Timers.Timer timer = new System.Timers.Timer(TimerElapsedPeriod);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(PerformThreadSafeBarcodeTransDGVCellValueChanges);
                timer.Enabled = true;
            }
        }

        private void PerformThreadSafeDGVUpdations(object sender, ElapsedEventArgs eea)
        {
            BarcodeTransDGV.Invoke(this.UpdateDataGridViewDelegate);
        }

        private void PerformThreadSafeBarcodeTransDGVCellValueChanges(object sender, ElapsedEventArgs eea)
        {
            BarcodeTransDGV.Invoke(this.CheckBarcodeTransDGVCellValsDel);
        }

        private void PopulateBarcodeTransactionsGridView()
        {
            DataTable BarcodeTransactions = btdao.GetAllBarcodeTransactions(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, DeviceIP);
            BarcodeTransDGV.DataSource = BarcodeTransactions;
            DataTable BtID = new DataTable();
            BtID.Columns.Add("ID", typeof(Int64));
            DataGridViewButtonColumn dgvbtncol = new DataGridViewButtonColumn();
            dgvbtncol.UseColumnTextForButtonValue = true;
            dgvbtncol.Text = "Edit";
            dgvbtncol.Name = "Edit Transaction";
            BarcodeTransDGV.Columns.Add(dgvbtncol);
            SetDataGridViewStyles(BarcodeTransactions);

            foreach (DataRow dr in BarcodeTransactions.Rows)
            {
                BtID.Rows.Add(Int64.Parse(dr["ID"].ToString()));   
            }

            btdao.MarkBarcodeTransDisplayed(BtID);
        }

        private void ChangeBarcodeTransDGVCellValues()
        {
            try
            {
                BarcodeTransDGV.EditMode = DataGridViewEditMode.EditProgrammatically;

                foreach (DataGridViewRow dgvr in BarcodeTransDGV.Rows)
                {
                    DataTable dt = btdao.GetAllBarcodeTransactions(dgvr.Cells[0].Value.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, true);

                    if (!dt.Rows[0]["Module_Serial_Number"].ToString().Equals(dgvr.Cells[1].Value.ToString()))
                    {
                        BarcodeTransDGV.CurrentCell = dgvr.Cells[1];
                        BarcodeTransDGV.BeginEdit(true);
                        dgvr.Cells[1].Value = dt.Rows[0]["Module_Serial_Number"].ToString();
                        BarcodeTransDGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        BarcodeTransDGV.EndEdit();
                    }

                    if (!dt.Rows[0]["BarcodeReaderSerialNumber"].ToString().Equals(dgvr.Cells[2].Value.ToString()))
                    {
                        BarcodeTransDGV.CurrentCell = dgvr.Cells[2];
                        BarcodeTransDGV.BeginEdit(true);
                        dgvr.Cells[2].Value = dt.Rows[0]["BarcodeReaderSerialNumber"].ToString();
                        BarcodeTransDGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        BarcodeTransDGV.EndEdit();
                        BarcodeTransDGV.CurrentCell = dgvr.Cells[3];
                        BarcodeTransDGV.BeginEdit(true);
                        dgvr.Cells[3].Value = dt.Rows[0]["LaminatorNumber"].ToString();
                        BarcodeTransDGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        BarcodeTransDGV.EndEdit();
                    }

                    if ((!string.IsNullOrEmpty(dt.Rows[0]["ErrorID"].ToString())) && (Int32.Parse(dt.Rows[0]["ErrorID"].ToString()) > 0) && (!string.IsNullOrEmpty(dt.Rows[0]["IsErrorResolved"].ToString())))
                    {
                        BarcodeTransDGV.CurrentCell = dgvr.Cells[7];
                        BarcodeTransDGV.BeginEdit(true);
                        dgvr.Cells[7].Value = dt.Rows[0]["IsErrorResolved"].ToString();
                        BarcodeTransDGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        BarcodeTransDGV.EndEdit();
                    }

                    //BarcodeTransDGV.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    //BarcodeTransDGV.EndEdit(DataGridViewDataErrorContexts.Commit);
                    BarcodeTransDGV.Refresh();
                }
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show("Invalid operation exception occured at ChangeBarcodeTransDGVCellValues(): " + ioe.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("exception occured at ChangeBarcodeTransDGVCellValues(): " + ex.Message);
            }
            //DataTable CurrentRecordsInBarcodeTransDGV = (DataTable)BarcodeTransDGV.DataSource;
        }

        private void SetDataGridViewStyles(DataTable ContentTable)
        { 
            //BarcodeTransDGV.AutoGenerateColumns = false;
            BarcodeTransDGV.AllowUserToAddRows = false;
            groupBox2.Controls.Add(BarcodeTransDGV);
            BarcodeTransDGV.Visible = true;
            //BarcodeTransDGV.Columns[0].HeaderCell.Value = "ID";
            BarcodeTransDGV.Columns[0].Visible = false;
            BarcodeTransDGV.Columns[1].HeaderCell.Value = "Module Serial Number";
            BarcodeTransDGV.Columns[2].HeaderCell.Value = "Reader Serial Number";
            BarcodeTransDGV.Columns[3].HeaderCell.Value = "Laminator Number";
            BarcodeTransDGV.Columns[4].HeaderCell.Value = "Creation Time";
            BarcodeTransDGV.Columns[5].Visible = false;
            BarcodeTransDGV.Columns[6].Visible = false;
            BarcodeTransDGV.Columns[7].Visible = false;
            BarcodeTransDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BarcodeTransDGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            BarcodeTransDGV.MultiSelect = false;
            BarcodeTransDGV.Columns[0].ReadOnly = true;
            BarcodeTransDGV.Columns[1].ReadOnly = true;
            BarcodeTransDGV.Columns[2].ReadOnly = true;
            BarcodeTransDGV.Columns[3].ReadOnly = true;
            BarcodeTransDGV.Columns[4].ReadOnly = true;
            BarcodeTransDGV.Columns[5].ReadOnly = true;
            BarcodeTransDGV.Columns[6].ReadOnly = true;
            BarcodeTransDGV.Columns[7].ReadOnly = true;
            BarcodeTransDGV.Columns[8].Visible = false;
            
            DataGridViewCellStyle HeaderStyle = new DataGridViewCellStyle();
            HeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            HeaderStyle.BackColor = System.Drawing.Color.Gray;
            HeaderStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            DataGridViewCellStyle ContentStyle = new DataGridViewCellStyle();
            ContentStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ContentStyle.Font = new Font("Microsoft Sans Serif", 8);

            DataGridViewCellStyle ErrorContentStyle = new DataGridViewCellStyle();
            ErrorContentStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ErrorContentStyle.Font = new Font("Microsoft Sans Serif", 8);
            ErrorContentStyle.ForeColor = Color.Red;

            foreach (DataGridViewColumn col in BarcodeTransDGV.Columns)
            {
                col.HeaderCell.Style = HeaderStyle;
            }

            foreach (DataGridViewRow row in BarcodeTransDGV.Rows)
            {
                row.DefaultCellStyle = ContentStyle;
            }

            BarcodeTransDGV.Columns[0].ReadOnly = true;

            if (ContentTable.Rows.Count > 0)
            {

                foreach (DataRow dr in ContentTable.Rows)
                {
                    if ((!string.IsNullOrEmpty(dr["ErrorID"].ToString()) && Int32.Parse(dr["ErrorID"].ToString()) > 0) && (!string.IsNullOrEmpty(dr["IsErrorResolved"].ToString()) && !Boolean.Parse(dr["IsErrorResolved"].ToString())))
                    {
                        using (DataGridViewRow dgvr = BarcodeTransDGV.Rows.Cast<DataGridViewRow>().Where(r => ((Int64.Parse(dr["ID"].ToString()) == Int64.Parse(r.Cells[0].Value.ToString())) && (!string.IsNullOrEmpty(r.Cells[5].Value.ToString())) && (Int32.Parse(dr["ErrorID"].ToString()) == Int32.Parse(r.Cells[5].Value.ToString())) && (!string.IsNullOrEmpty(r.Cells[7].Value.ToString())) && (!Boolean.Parse(r.Cells[7].Value.ToString())))).Select(r => r).First())
                        {
                            dgvr.DefaultCellStyle = ErrorContentStyle;
                            foreach (DataGridViewCell dgvc in dgvr.Cells)
                            {
                                dgvc.ToolTipText = dr["ErrorMessage"].ToString();
                            }
                        }
                        //using (DataGridViewRow dgvr = BarcodeTransDGV.Rows.Cast<DataGridViewRow>().Where(r => (!string.IsNullOrEmpty(r.Cells[5].Value.ToString()) && (Int32.Parse(r.Cells[5].Value.ToString()) == Int32.Parse(dr["ErrorID"].ToString())) && ())).)
                        //{
                        //    dgvr.DefaultCellStyle = ErrorContentStyle;
                        //    foreach (DataGridViewCell dgvc in dgvr.Cells)
                        //    {
                        //        dgvc.ToolTipText = dr["ErrorMessage"].ToString();
                        //    }
                        //}
                    }
                }
            }
        }

        private void BarcodeDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            udao.SetUserLogIn(this.LoggedInUser);
            Application.Exit();
        }

        private void AppendBarcodeDataToBarcodeTransDGV()
        {
            try
            {
                ThreadSafety.WaitOne();
                DataTable LiveBarcodeTransactions = btdao.GetAllBarcodeTransactions(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, DeviceIP);
                DataTable BarcodeTransInGV = (DataTable)BarcodeTransDGV.DataSource;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(Int64));

                foreach (DataRow dr in LiveBarcodeTransactions.Rows)
                {
                    DataRow datarow = BarcodeTransInGV.NewRow();
                    datarow[0] = Int64.Parse(dr["ID"].ToString());
                    datarow[1] = dr["Module_Serial_Number"].ToString();
                    datarow[2] = dr["BarcodeReaderSerialNumber"].ToString();
                    datarow[3] = dr["LaminatorNumber"].ToString();
                    datarow[4] = DateTime.Parse(dr["CreationTime"].ToString());

                    if (!string.IsNullOrEmpty(dr["ErrorID"].ToString()))
                        datarow[5] = Int32.Parse(dr["ErrorID"].ToString());
                    if (!string.IsNullOrEmpty(dr["ErrorMessage"].ToString()))
                        datarow[6] = dr["ErrorMessage"].ToString();
                    if (!string.IsNullOrEmpty(dr["IsErrorResolved"].ToString()))
                        datarow[7] = Boolean.Parse(dr["IsErrorResolved"].ToString());

                    BarcodeTransInGV.Rows.Add(datarow);
                    BarcodeTransInGV.AcceptChanges();
                    dt.Rows.Add(Int64.Parse(dr["ID"].ToString()));
                }

                SetDataGridViewStyles(BarcodeTransInGV);
                BarcodeTransDGV.DataSource = BarcodeTransInGV;
                BarcodeTransDGV.Visible = true;

                btdao.MarkBarcodeTransDisplayed(dt);
                ThreadSafety.ReleaseMutex();
            }
            catch (SqlException se)
            {
                MessageBox.Show("SQL Exception thrown at AppendBarcodeDataToBarcodeTransDGV(): " + se.Message, "SqlException");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception thrown at AppendBarcodeDataToBarcodeTransDGV(): " + ex.Message, "SqlException");
            }
        }

        private void btn_manual_entry_Click(object sender, EventArgs e)
        {
            BarcodeAdditionForm baf = new BarcodeAdditionForm(this, LoggedInUser);
            this.Hide();
            baf.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (udao.SetUserLogIn(this.LoggedInUser) > 0)
            {
                Form1 form = new Form1();
                MessageBox.Show("Successfully logged out", "Log Out:");
                BarcodeTransactionUpdateThread.Abort();
                BarcodeTransDGVContentChanger.Abort();
                form.Show();
                //CurrentLoggedInForm.Dispose();
                this.Dispose();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BarcodeSearchForm bsf = new BarcodeSearchForm(LoggedInUser, this);
            this.Hide();
            bsf.Show();
        }

        private void BarcodeDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (udao.SetUserLogIn(LoggedInUser) > 0)
            {
                Application.Exit();
            }
        }

        private void BarcodeTransDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >=0 && BarcodeTransDGV.Columns[e.ColumnIndex].Name == "Edit Transaction")
            {
                DataGridViewRow SelectedDgvr = BarcodeTransDGV.Rows[e.RowIndex];

                BarcodeTransaction bt = new BarcodeTransaction();
                bt.ID = Int64.Parse(SelectedDgvr.Cells[0].Value.ToString());
                bt.ModuleSerialNumber = SelectedDgvr.Cells[1].Value.ToString();
                bt.BarcodeReaderSerialNumber = SelectedDgvr.Cells[2].Value.ToString();

                if (!string.IsNullOrEmpty(SelectedDgvr.Cells[5].Value.ToString()))
                    bt.ErrorID = Int32.Parse(SelectedDgvr.Cells[5].Value.ToString());
                else
                    bt.ErrorID = 0;

                BarcodeAdditionForm baf = new BarcodeAdditionForm(this, LoggedInUser, bt);
                this.Hide();
                baf.Show();
            }
        }
    }
}
