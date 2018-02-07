using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Domain;
using Persistence;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace MSTPLFixedBarcodeReaderScannerApp
{
    public partial class Form1 : Form
    {
        private string ConnectionString;
        internal User user;
        private UserDAO udao;

        public Form1()
        {
            InitializeComponent();
            this.ConnectionString = ConfigurationManager.ConnectionStrings["DBServerConnectionString"].ToString();
            user = new User();
            udao = new UserDAO(this.ConnectionString);
            txt_Password.UseSystemPasswordChar = true;
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_UserName.Text.Length > 0 && txt_Password.Text.Length > 0)
                {
                    user = udao.GetUserDetails(txt_UserName.Text, txt_Password.Text);

                    if (user.ID > 0)
                    {
                        if (udao.SetUserLogIn(user) > 0)
                        {
                            user.IsLoggedIn = true;
                            BarcodeDetailsForm bdf = new BarcodeDetailsForm(user,this);
                            this.Hide();
                            bdf.Show();
                        }
                        else
                        {
                            MessageBox.Show("Multiple user log ins are not allowed", "Login error:");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to log in using provided credentials", "Login error:");
                    }
                }
                else
                {
                    MessageBox.Show("User name and password is required", "Login");
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show("Can not login due to unexpected sql exception" + se.Message, "Sql Exception:");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not login due to unexpected exception" + ex.Message, "Exception:");
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_Password.UseSystemPasswordChar = true;
        }

        private void btn_UserReg_Click(object sender, EventArgs e)
        {
            UserRegistration ur = new UserRegistration(this);
            this.Hide();
            ur.Show();
        }
    }
}
