using System;
using DataDropLibrary.Utilities.Database;
using DataDropLibrary.Utilities;
using System.Collections.Generic;

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
            Table = destinationFileName;
            string insertStatement = (string)GenerateWriteData();
            insertStatement = insertStatement.Replace("\"", "'").Replace("\\r\\n", " ");
            DB.Query(insertStatement);
            DB.CloseConnection();
        }

        public DatabaseDataFormat(
            string server, string catalog, string username,
            string password, string dbSystem,
            string source, List<string> KeepValues, SourceDataType sourceDataType,
            string port = ""
            ) : base(source, KeepValues, sourceDataType)
        {
            DB = new DB(server, catalog, username, password, dbSystem, port);
        }
    }
}
