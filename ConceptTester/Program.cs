using DataDropLibrary.Models;
using DataDropLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConceptTester
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> attrs = new List<string>();
            attrs.Add("name");
            attrs.Add("job");
            attrs.Add("age");
            SourceDataType dataType = new SourceDataType();
            string initialDirectory = "C:\\Users\\eedee\\Documents";
            string jsonSource = "test_json.json";
            string jsonForDB = "teste_db_json.json";
            string excelSource = "testExcel.xlsx";
            string apiSource = "http://ewhiting.eastus.cloudapp.azure.com/midterm/Classes/Genre/read.php";

            DataFormat excelDF = new ExcelDataFormat(excelSource, attrs, SourceDataType.Excel);
        }
    }
}
