using System;
using DataDropLibrary.Utilities.Database;
using DataDropLibrary.Utilities;

namespace DataDropLibrary.Models
{
    public class DatabaseDataFormat : DataFormat
    {
        public DB DB { get; set; }
        public string Table { get; set; }

        public override object GenerateWriteData()
        {
            return Helpers.GenerateSqlInsert(DataObjects, Table);
        }

        public override void WriteData(string destinationDirectory, string destinationFileName)
        {
            string insertStatement = (string)GenerateWriteData();
            DB.Query(insertStatement.Replace("\"", "'").Replace("\\r\\n", " "));
            DB.CloseConnection();
        }

        public DatabaseDataFormat(
            string source, string catalog, string username,
            string password, string dbSystem, string port = ""
            ) : base()
        {
            DB = new DB(source, catalog, username, password, dbSystem, port);
        }
    }
}
