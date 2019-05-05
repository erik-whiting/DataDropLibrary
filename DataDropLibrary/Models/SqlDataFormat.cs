using System;
using DataDropLibrary.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Models
{
    class SqlDataFormat : DataFormat
    {
        public string TableName { get; set; }

        public override object GenerateWriteData() => Helpers.GenerateSqlInsert(DataObjects, TableName);

        public override void WriteData(string destinationDirectory, string destinationFileName)
        {
            TableName = destinationFileName.Split('.').First();
            string filePath = destinationDirectory + "\\" + DateTime.Now.ToString("MM-dd-yyyy") + destinationFileName;
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(GenerateWriteData());
            }
        }

        public SqlDataFormat() : base() { }
    }
}
