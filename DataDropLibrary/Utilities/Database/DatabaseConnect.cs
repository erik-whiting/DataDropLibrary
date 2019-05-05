using DataDropLibrary.Utilities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities
{
    public class DatabaseConnect
    {
        private string Source { get; set; }
        private string Catalog { get; set; }
        private string UserName { get; set; }
        private string Password { get; set; }
        private string Port { get; set; }
        private string ConnectionString { get; set; }
        private DBMS DBMS;

        public DatabaseConnect(
            string source, string catalog, string username,
            string password, string dbSystem, string port = ""
            )
        {
            Source = source;
            Catalog = catalog;
            UserName = username;
            Password = password;
            Port = port;
            switch (dbSystem.ToLower())
            {
                case "sqlserver":
                    DBMS = DBMS.SQLServer;
                    break;
                case "oracle":
                    DBMS = DBMS.Oracle;
                    break;
                case "postgressql":
                    DBMS = DBMS.Postgres;
                    break;
                case "mysql":
                    DBMS = DBMS.MySQL;
                    break;
                default:
                    DBMS = DBMS.SQLServer;
                    break;
            }

        }

        public string GenerateConnectionString()
        {
            var connString = string.Empty;
            switch (DBMS)
            {
                case DBMS.SQLServer:
                    connString = ConnStringValues(SQLServerConnectString());
                    break;
                case DBMS.Oracle:
                    connString = ConnStringValues(OracleConnectString());
                    break;
                case DBMS.Postgres:
                    connString = ConnStringValues(PostgreConnectString());
                    break;
                case DBMS.MySQL:
                    connString = ConnStringValues(MySQLConnectString());
                    break;
                default:
                    connString = ConnStringValues(SQLServerConnectString());
                    break;
            }
                
            return connString;
        }

        public string ConnStringValues(string baseString)
        {
            string connString = baseString.Replace("GETSOURCE", Source);
            connString = connString.Replace("GETDB", Catalog);
            connString = connString.Replace("GETUSER", UserName);
            connString = connString.Replace("GETPASSWORD", Password);
            connString = connString.Replace("GETPORT", Port);

            return connString;
        }
        
        public string SQLServerConnectString()
        {
            return "Data Source=GETSOURCE;Initial Catalog=GETDB;User ID=GETUSER;Password=GETPASSWORD";
        }
        public string OracleConnectString()
        {
            return "Data Source=GETSOURCE;User Id=GETUSER;Password=GETPASSWORD";
        }
        public string PostgreConnectString()
        {
            return "User ID=GETUSER;Password=GETPASSWORD;Host=GETHOST;Port=GETPORT;Database=GETDB";
        }
        public string MySQLConnectString()
        {
            return "Server=GETSOURCE;Database=GETDB;Uid=GETUSER;Pwd=GETPASSWORD;";
        }
    }
}
