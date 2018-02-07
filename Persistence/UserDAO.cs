using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Domain;
using System.Security.Cryptography;

namespace Persistence
{
    public class UserDAO
    {
        private string ConnectionString;
        private MD5CryptoServiceProvider md5enc;

        public UserDAO(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
            md5enc = new MD5CryptoServiceProvider();
        }

        public Domain.User GetUserDetails(string UserName, string Password)
        {
            Domain.User user = new Domain.User();

            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetUserDetails", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    while (sdr.Read())
                    {
                        user.ID = Int32.Parse(sdr["ID"].ToString());
                        user.UserName = sdr["Name"].ToString();
                        user.Password = sdr["Password"].ToString();
                        user.IsLoggedIn = Boolean.Parse(sdr["IsLoggedIn"].ToString());
                    }
                }
            }
            catch (FormatException fe)
            {
                throw fe;
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }

        public bool CheckForMultipleLogins(User user)
        {
            bool IsMultipleLogIn = false;
            try
            {
                using (SqlConnection con = new SqlConnection(this.ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_ReturnLoggedInUserNums", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CurrentUserId", user.ID);

                    if ((int)cmd.ExecuteScalar() > 0)
                        IsMultipleLogIn = true;
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsMultipleLogIn;
        }

        public int SetUserLogIn(User user)
        {
            int NumRowsAffected = 0;
            string LoginUpdateQuery = string.Empty;

            try
            {
                if (user.IsLoggedIn == true)
                    LoginUpdateQuery = "UPDATE [dbo].[UserMaster] set IsLoggedIn=0 WHERE ID=" + user.ID;
                else
                {
                    if (!CheckForMultipleLogins(user))
                        LoginUpdateQuery = "UPDATE [dbo].[UserMaster] set IsLoggedIn=1 WHERE IsLoggedIn=0 AND ID=" + user.ID;
                }

                if (!string.IsNullOrEmpty(LoginUpdateQuery))
                {
                    using (SqlConnection con = new SqlConnection(this.ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = LoginUpdateQuery;
                        NumRowsAffected = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NumRowsAffected;
        }

        public bool GetAllDuplicateUsersByUserName(string UserName)
        {
            bool IsDuplicate = false;
            int NumUsers = 0;

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetAllDuplicateUsers", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    if (!Int32.TryParse(cmd.ExecuteScalar().ToString(), out NumUsers))
                        throw new FormatException("Can not parse number of duplicate users");

                    if (NumUsers > 0)
                        IsDuplicate = true;
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsDuplicate;
        }

        public bool GetAllDuplicateUsersByUserEmail(string UserEmail)
        {
            bool IsDuplicate = false;
            int NumUsers = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetAllDuplicateUsersByEmail", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserEmail", UserEmail);
                    if (!Int32.TryParse(cmd.ExecuteScalar().ToString(), out NumUsers))
                        throw new FormatException("Can not parse number of duplicate users");

                    if (NumUsers > 0)
                        IsDuplicate = true;
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return IsDuplicate;
        }

        public int InsertNewUser(string UserName, string Password, string FirstName, string LastName, string Email)
        {
            int RowsInserted = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertNewuser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@Email", Email);

                    RowsInserted = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException se)
            {
                throw se;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RowsInserted;
        }
    }
}
