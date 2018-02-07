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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace MSTPLFixedBarcodeReaderScannerApp
{
    public partial class UserRegistration : Form
    {
        private UserDAO udao;
        private string ConnectionString;
        private Form1 LoginForm;

        public UserRegistration(Form1 form)
        {
            InitializeComponent();
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBServerConnectionString"].ToString();
            udao = new UserDAO(ConnectionString);
            LoginForm = form;
            txt_Password.UseSystemPasswordChar = true;
            txt_PasswordRetype.UseSystemPasswordChar = true;
        }

        public void ValidateNewUserRegistration(User NewUser)
        {
            string EmptyFieldsMsg = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(NewUser.UserName) && udao.GetAllDuplicateUsersByUserName(NewUser.UserName))
                {
                    throw new Exception("This user name is already taken");
                }
                else if (string.IsNullOrEmpty(NewUser.UserName))
                {
                    EmptyFieldsMsg += "Username,";
                }

                if (string.IsNullOrEmpty(NewUser.Password))
                {
                    EmptyFieldsMsg += "Password,";
                }

                if (string.IsNullOrEmpty(txt_PasswordRetype.Text))
                {
                    EmptyFieldsMsg += "Retype Password,";
                }
                else if (!string.IsNullOrEmpty(NewUser.Password) && !string.IsNullOrEmpty(txt_PasswordRetype.Text) && !NewUser.Password.Equals(txt_PasswordRetype.Text, StringComparison.InvariantCulture))
                {
                    throw new Exception("Values in Password and Password Retype does not match");
                }

                if (string.IsNullOrEmpty(NewUser.FirstName))
                {
                    EmptyFieldsMsg += "First Name,";
                }

                if (string.IsNullOrEmpty(NewUser.LastName))
                {
                    EmptyFieldsMsg += "Last Name,";
                }

                if (string.IsNullOrEmpty(NewUser.Email))
                {
                    EmptyFieldsMsg += "Email";
                }
                else if (!string.IsNullOrEmpty(NewUser.Email) && !Regex.IsMatch(NewUser.Email, @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"))
                {
                    throw new Exception("Please enter valid Email address");
                }
                else if (!string.IsNullOrEmpty(NewUser.Email) && udao.GetAllDuplicateUsersByUserEmail(NewUser.Email))
                {
                    throw new Exception("User with this e-mail already exists, please log in with your credentials or contact system admin for user name and password");
                }

                if (!string.IsNullOrEmpty(EmptyFieldsMsg))
                {
                    throw new Exception(EmptyFieldsMsg + " fields are required.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            User NewUser = new User();

            NewUser.UserName = txt_UserName.Text;
            NewUser.Password = txt_Password.Text;
            NewUser.FirstName = txt_FirstName.Text;
            NewUser.LastName = txt_LastName.Text;
            NewUser.Email = txt_Email.Text;

            try
            {
                ValidateNewUserRegistration(NewUser);
                if (udao.InsertNewUser(NewUser.UserName, NewUser.Password, NewUser.FirstName, NewUser.LastName, NewUser.Email) > -2)
                {
                    MessageBox.Show("User Registration sussessful, log in with the credentials", "Success");
                    LoginForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User registration failed, contact system admin", "Registration failure:");
                }
            }
            catch (SqlException se)
            {
                MessageBox.Show("Sql exception occured during new user registration, try again", "Sql Exception:");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation Error:");
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            LoginForm.Show();
            this.Close();
        }

        private void UserRegistration_Load(object sender, EventArgs e)
        {
            txt_Password.UseSystemPasswordChar = true;
            txt_PasswordRetype.UseSystemPasswordChar = true;
        }
    }
}
