using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Persistence;
using System.Data.SqlClient;

namespace BarcodeScanAppForms
{
    public partial class UserControl1 : UserControl
    {
        private BarcodeTransactionDAO btdao;
        private string DBConnectionString;
        private Form pf;
        private bool UseDateAsSearchParam;

        public UserControl1()
        {
            InitializeComponent();
        }

        public UserControl1(string DBConnectionString, Form f)
            : this()
        {
            pf = f;
            this.DBConnectionString = DBConnectionString;
            btdao = new BarcodeTransactionDAO(this.DBConnectionString);
            UseDateAsSearchParam = false;
            DatePicker.Enabled = false;
            UseScanDate.CheckedChanged +=new EventHandler(UseScanDate_CheckedChanged);
        }

        private void UseScanDate_CheckedChanged(object sender,  EventArgs ea)
        {
            CheckBox cb = (CheckBox)sender;

            if (cb.Checked)
            {
                UseDateAsSearchParam = true;
                DatePicker.Enabled = true;
            }
            else
            {
                UseDateAsSearchParam = false;
                DatePicker.Enabled = false;
            }
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                string ModuleSerialNumber = txt_mod_ser_num.Text;
                string BarcodeReaderSrialNumber = txt_reader_serial_num.Text;
                string LaminatorNumber = txt_laminator_number.Text;
                string BarcodeScanDate = "";
                if(UseDateAsSearchParam)
                    BarcodeScanDate = DatePicker.Value.ToString("yyyy-MM-dd");

                bool IsSearch = true;

                DataTable BarcodeSearchResult = btdao.GetAllBarcodeTransactions(string.Empty,ModuleSerialNumber, BarcodeReaderSrialNumber, LaminatorNumber, BarcodeScanDate, IsSearch);
                if (BarcodeSearchResult.Rows.Count > 0)
                {
                    BarcodeSearchList bsl = new BarcodeSearchList(BarcodeSearchResult, DBConnectionString, pf);
                    this.Parent.Controls.Add(bsl);
                    this.Dispose();
                }
                else
                    MessageBox.Show("Search result returned " + BarcodeSearchResult.Rows.Count + " records.");
            }
            catch (SqlException se)
            {
                MessageBox.Show("Database exception occured at barcode search: " + se.Message, "Database Exception:");
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Format exception occured at barcode search: " + fe.Message, "Data Format Conversion:");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured at barcode search: " + ex.Message, "Exception:");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            pf.Show();
            this.Parent.Parent.Parent.Dispose();
        }
    }


}
