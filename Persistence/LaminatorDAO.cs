using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class LaminatorDAO
    {
        private string ConnectionString;

        public LaminatorDAO(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public DataTable GetLaminatorDetailsByBarcodeReaderLaminatorMappingID(int BarcodeReaderLaminatorMappingID)
        {
            DataTable LaminatorDT = new DataTable("AllDataTables");

            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_GetLaminatorDetailsbyLaminatorBarcodeMappingID", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LaminatorBarcodeMappingID", BarcodeReaderLaminatorMappingID);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(LaminatorDT);
                    con.Close();
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

            return LaminatorDT;
        }
    }
}
