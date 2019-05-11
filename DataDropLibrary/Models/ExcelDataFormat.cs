using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using DataDropLibrary.Utilities;

namespace DataDropLibrary.Models
{
    public class ExcelDataFormat : DataFormat
    {
        public override object GenerateWriteData()
        {
            Dictionary<string, object> ExcelWriteData = new Dictionary<string, object>();

            List<string[]> headerRow = new List<string[]>();
            string headers = "";
            var DOHeaders = Helpers.GetAttributes(DataObjects);
            foreach (var h in DOHeaders) headers = headers == "" ? headers += h : headers += "," + h;
            headerRow.Add(headers.Split(','));
            string headerRange = "A1: " + char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";
            ExcelWriteData.Add("HeaderData", headerRow);
            ExcelWriteData.Add("HeaderRange", headerRange);

            string cellDataString = "";
            var cellData = new List<string[]>();
            foreach (var header in DOHeaders)
            {
                foreach (var dataObject in DataObjects)
                {
                    foreach (var pair in dataObject.DataPairs)
                    {
                        
                        cellDataString += (cellDataString == "" ? pair[header] : "," + pair[header]);
                    }
                }
                cellData.Add(cellDataString.Split(','));
                cellDataString = "";
            }
            ExcelWriteData.Add("CellData", cellData);
            return ExcelWriteData;
        }

        public override void WriteData(string destinationDirectory, string destinationFileName)
        {
            string filePath = destinationDirectory + "\\" + destinationFileName + ".xlsx";
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("DataDrop Worksheet");
                var dataSheet = excel.Workbook.Worksheets["DataDrop Worksheet"];

                var ExcelWriterData = (Dictionary<string, object>)GenerateWriteData();
                var headerRow = (List<string[]>)ExcelWriterData["HeaderData"];
                var headerRange = (string)ExcelWriterData["HeaderRange"];
                var cellData = (List<string[]>)ExcelWriterData["CellData"];

                dataSheet.Cells[headerRange].LoadFromArrays(headerRow);
                int columnStart = 1;
                int rowStart = 2;

                for (int i = 0; i < cellData.Count; i++)
                {
                    var listItem = new List<object[]>();
                    var item = cellData[i];
                    listItem.Add(item);
                    for (int x = 0; x < item.Length; x++)
                    {
                        dataSheet.Cells[rowStart + x, columnStart + i].LoadFromText(item[x]);
                    }
                }
                FileInfo excelFile = new FileInfo(filePath);
                excel.SaveAs(excelFile);
            }
        }
        public ExcelDataFormat() : base() { }
        public ExcelDataFormat(string source, List<string> KeepValues, SourceDataType sourceDataType) :
            base(source, KeepValues, sourceDataType) { }
        public ExcelDataFormat(List<DataObject> dataObjects) : base(dataObjects) { }
    }
}
