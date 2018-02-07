using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using BarcodeScanAppForms;
using Persistence;
using Domain;

namespace MSTPLFixedBarcodeReaderScannerApp
{
    public partial class BarcodeSearchForm : Form
    {
        private string DBConnectionString;
        private UserDAO udao;
        private User LoggedInUser;
        private BarcodeDetailsForm BCDetForm;

        public BarcodeSearchForm()
        {
            InitializeComponent();
        }

        public BarcodeSearchForm(User user, BarcodeDetailsForm bdf)
            : this()
        {
            BCDetForm = bdf;
            DBConnectionString = ConfigurationManager.ConnectionStrings["DBServerConnectionString"].ToString();
            UserControl1 uc1 = new UserControl1(DBConnectionString, BCDetForm);
            this.SearchPanel.Controls.Add(uc1);
            udao = new UserDAO(DBConnectionString);
            this.LoggedInUser = user;
        }

        private void BarcodeSearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (udao.SetUserLogIn(LoggedInUser) > 0)
            {
                Application.Exit();
            }
        }
    }
}
