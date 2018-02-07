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
using System.Data.SqlClient;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using MSTPLBarcodeDataReceiveSrvc;
using System.Net;
using System.Collections;

namespace MSTPLFixedBarcodeReaderScannerApp
{
    public partial class BarcodeAdditionForm : Form
    {
        private BarcodeReaderDAO brdao;
        private BarcodeTransactionDAO btdao;
        private BarcodeDetailsForm bcdf;
        private string ConnectionString;
        private User LoggedInUser;
        private UserDAO udao;
        private Int64 BarcodeTransactionID = 0;
        private string BarcodeReaderSerialNumber = string.Empty;
        private int BarcodeTransErrorID = 0;
        private ExcelReportUpdateUtility eru;
        private LaminatorDAO ldao;
        private string DeviceIP;

        public BarcodeAdditionForm(BarcodeDetailsForm bf)
        {
            InitializeComponent();
            ConnectionString = ConfigurationManager.ConnectionStrings["DBServerConnectionString"].ToString();
            brdao = new BarcodeReaderDAO(ConnectionString);
            ldao = new LaminatorDAO(ConnectionString);
            btdao = new BarcodeTransactionDAO(ConnectionString);
            bcdf = bf;
            udao = new UserDAO(ConnectionString);
            DeviceIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.AsEnumerable().Where(s => s.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(s => s).First().ToString();
            PopulateBarcodeReaderDD();

            if(ChannelServices.RegisteredChannels.Count<IChannel>() <= 0)
                ChannelServices.RegisterChannel(new ChannelSrvc(new HttpChannel(), true), true);

            IEnumerable a = RemotingConfiguration.GetRegisteredWellKnownClientTypes().AsEnumerable();
            if(!RemotingConfiguration.GetRegisteredWellKnownClientTypes().AsEnumerable().Where(s => s.ObjectUrl == "http://" + ConfigurationManager.AppSettings["RemoteServerIP"].ToString() + ":" + ConfigurationManager.AppSettings["RemoteServerPort"] + "/ExcelReportUpdateUtility").Select(s => s).Any())
                RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(typeof(ExcelReportUpdateUtility), "http://" + ConfigurationManager.AppSettings["RemoteServerIP"].ToString() + ":" + ConfigurationManager.AppSettings["RemoteServerPort"] + "/ExcelReportUpdateUtility"));

            eru = new ExcelReportUpdateUtility();
            eru.SetClientSystemIP(DeviceIP);

        }

        public BarcodeAdditionForm(BarcodeDetailsForm bf, User user) : this(bf)
        {
            this.LoggedInUser = user;
        }

        public BarcodeAdditionForm(BarcodeDetailsForm bdf, User user, BarcodeTransaction bt)
            : this(bdf, user)
        {
            this.lbl_addbcdetails.Text = "Update Barcode Details";
            this.txt_bcserial.Text = bt.ModuleSerialNumber.ToString();
            this.dd_bcrserial.SelectedText = bt.BarcodeReaderSerialNumber.ToString();
            BarcodeTransactionID = bt.ID;
            BarcodeTransErrorID = bt.ErrorID;
            BarcodeReaderSerialNumber = bt.BarcodeReaderSerialNumber;

            if (!string.IsNullOrEmpty(BarcodeReaderSerialNumber))
                this.dd_bcrserial.SelectedIndex = dd_bcrserial.FindStringExact(BarcodeReaderSerialNumber);
        }
        private void PopulateBarcodeReaderDD()
        {
            try
            {
                DataTable BarcodeDetails = brdao.GetAllBarcodeReaders(DeviceIP);

                this.dd_bcrserial.DataSource = BarcodeDetails.DefaultView;
                this.dd_bcrserial.DisplayMember = "SerialNumber";
                this.dd_bcrserial.ValueMember = "ID";
            }
            catch (SqlException se)
            {
                MessageBox.Show("SQL Exception thrown at PopulateBarcodeReaderDD(): " + se.Message, "Database Exception:");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception thrown at PopulateBarcodeReaderDD(): " + ex.Message, "Exception:");
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_bcserial.Text))
                    MessageBox.Show("Please provide barcode serial number", "Validation:");
                else
                {
                    string BarcodeSerialNumber = txt_bcserial.Text;
                    int BarcodeReaderSerial = 0;

                    if (!Int32.TryParse(dd_bcrserial.SelectedValue.ToString(), out BarcodeReaderSerial))
                        throw new FormatException("Can not parse barcode reader serial number");

                    int bcrlamnummap = brdao.GetBarcodeReaderLaminatorMappingIDByBarcodeReaderID(BarcodeReaderSerial);
                    DateTime BarcodeCreationDate = System.DateTime.Now;

                    BarcodeTransaction bt = new BarcodeTransaction();
                    bt.ID = BarcodeTransactionID;
                    bt.ModuleSerialNumber = BarcodeSerialNumber;
                    bt.LaminatorBarcodeReaderMappingID = bcrlamnummap;
                    bt.CreationTime = BarcodeCreationDate;
       

                    if (BarcodeTransactionID > 0)
                    {
                        bt.IsErrorResolved = true;
                        bt.ErrorID = BarcodeTransErrorID;
                    }

                    long NumRowsAffected = btdao.InsertNewBarcodeReaderTransaction(bt);

                    bt.BarcodeReaderSerialNumber = brdao.GetReaderDetailsByReaderID(BarcodeReaderSerial).Rows[0]["SerialNumber"].ToString();
                    bt.LaminatorNumber = ldao.GetLaminatorDetailsByBarcodeReaderLaminatorMappingID(bcrlamnummap).Rows[0]["LaminatorNumber"].ToString();

                    if (BarcodeTransactionID > 0)
                    {
                        eru.EditExcelEntries(bt.ID.ToString(), bt.ModuleSerialNumber, bt.BarcodeReaderSerialNumber, bt.LaminatorNumber);
                    }
                    else
                    {
                        bt.ID = NumRowsAffected;
                        eru.UpdateExcelFromExternalApplications(bt.ID, bt.ModuleSerialNumber, bt.BarcodeReaderSerialNumber, bt.LaminatorNumber, bt.CreationTime.ToString());
                    }

                    MessageBox.Show("Barcode transaction saved successfully", "Success:");
                }
             
            }
            catch (SqlException se)
            {
                if (se.Number == 2627)
                    MessageBox.Show("Entry with same module serial number already exists in database, duplicate entries are not allowed.", "Duplicates:");
                else
                MessageBox.Show("SQL Exception thrown at PopulateBarcodeReaderDD(): " + se.Message, "Database Exception:");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception thrown at PopulateBarcodeReaderDD(): " + ex.Message, "Exception:");
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            bcdf.Show();
            this.Dispose();
        }

        private void BarcodeAdditionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (udao.SetUserLogIn(LoggedInUser) > 0)
            {
                Application.Exit();
            }
        }

        private void BarcodeAdditionForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_bcserial.Text.ToString()))
            {
                txt_bcserial.Enabled = false;
                txt_bcserial.ReadOnly = true;
            }
        }
    }

    class ChannelSrvc : IChannel, ISecurableChannel
    {
        //private IpcServerChannel ipcservchan;
        private HttpChannel httpchan;
        private bool IsChannelSecurable;

        //public ChannelSrvc(IpcServerChannel isc, bool IsChannelSecured)
        //{
        //    this.ipcservchan = isc;
        //    this.IsChannelSecurable = IsChannelSecured;
        //}

        public ChannelSrvc(HttpChannel httpchannel, bool IsChannelSecured)
        {
            this.httpchan = httpchannel;
            this.IsChannelSecurable = IsChannelSecured;
        }
        public string ChannelName
        {
            get { return this.httpchan.ChannelName; }
        }

        public int ChannelPriority
        {
            get { return this.httpchan.ChannelPriority; }
        }

        public string Parse(string url, out string objectURI)
        {
            httpchan.CreateMessageSink("http://"+ConfigurationManager.AppSettings["RemoteServerIP"] +":" + "5678", null, out objectURI);
            return objectURI;
        }

        public bool IsSecured
        {
            get
            {
                return httpchan.IsSecured;
            }
            set
            {
                httpchan.IsSecured = IsChannelSecurable;
            }
        }
    }
}
