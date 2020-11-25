using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionNoMetrolite
{
    public partial class DB
    {
        private static string DBSERVER = "192.168.7.11";
        private static string DBNAME = "UXFMUNI";
        private static string DBUSER = "sa";
        private static string DBPASS = "utaxthailand";

        private static string ConnectionStr { get { return string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}",DBSERVER,DBNAME,DBUSER,DBPASS); } }

        public static DataSet execSQL(string sql)
        {
            SqlConnection conn = new SqlConnection(ConnectionStr);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            try
            {
                cmd.CommandText = sql;
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                conn.Open();
                da.Fill(ds);
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                da.Dispose();
                cmd.Dispose();
            }
        }
    }
}
