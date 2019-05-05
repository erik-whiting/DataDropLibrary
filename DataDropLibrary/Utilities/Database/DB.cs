using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities.Database
{
    public class DB
    {
        private DatabaseConnect DatabaseConnect { get;set; }
        private SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }
        public string Status { get; set; }

        public DB(
            string source, string catalog, string username,
            string password, string dbSystem, string port = ""
            )
        {
            DatabaseConnect = new DatabaseConnect(source, catalog, username, password, port, dbSystem);
            Connection = new SqlConnection(DatabaseConnect.GenerateConnectionString());
            try
            {
                Connection.Open();
                Status = "Connected";
            }
            catch (Exception ex)
            {
                Status = "Connection Failed: " + ex;
            }
        }

        public DB() { }

        public void CloseConnection()
        {
            Connection.Close();
            Status = "Disconnected";
        }

        public SqlDataReader Query(string SQL) => new SqlCommand(SQL, Connection).ExecuteReader();

    }
}
