using DataDropLibrary.Models;
using DataDropLibrary.Utilities;
using System;
using System.Collections.Generic;

namespace ConceptTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up data
            string initialDirectory = "C:\\Users\\eedee\\Documents";
            string jsonSource = "test_json.json";
            string jsonForDB = "test_db_json.json";
            string excelSource = "testExcel.xlsx";
            string apiSource = "http://ewhiting.eastus.cloudapp.azure.com/midterm/Classes/Genre/read.php";

            // Basic demo
            List<string> attrs = new List<string>();
            attrs.Add("name");
            attrs.Add("job");
            attrs.Add("age");
            DataFormat excelDF = new ExcelDataFormat(initialDirectory + "\\" + jsonSource, attrs, SourceDataType.JSON);
            DataFormat jsonDF = new JsonDataFormat(initialDirectory + "\\" + excelSource, attrs, SourceDataType.Excel);
            excelDF.WriteData(initialDirectory, "excelTest");
            jsonDF.WriteData(initialDirectory, "jsonTest");

            // API demo
            List<string> apiAttrs = new List<string>();
            apiAttrs.Add("id");
            apiAttrs.Add("name");
            apiAttrs.Add("picture_path");
            DataFormat xmlDF = new XmlDataFormat(apiSource, apiAttrs, SourceDataType.API);
            xmlDF.WriteData(initialDirectory, "xmlTest");

            // Database demo
            List<string> dbAttrs = new List<string>();
            dbAttrs.Add("fname");
            dbAttrs.Add("lname");
            dbAttrs.Add("age");
            string server = Environment.MachineName;
            string db  = "LearningTest";
            string user = "erik_example";
            string password = "abc123";
            DatabaseDataFormat dbDF = new DatabaseDataFormat(
                server: server,
                catalog: db,
                username: user,
                password: password,
                dbSystem: "sqlserver",
                source: initialDirectory + "\\" + jsonForDB,
                KeepValues: dbAttrs,
                sourceDataType: SourceDataType.JSON
                );
            dbDF.WriteData("", "People");
        }
    }
}
