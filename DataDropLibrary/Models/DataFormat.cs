using DataDropLibrary.Interfaces;
using DataDropLibrary.Utilities;
using System.Collections.Generic;

namespace DataDropLibrary.Models
{
    public abstract class DataFormat : From, IDataFormat
    {
        public List<DataObject> DataObjects { get; set; }
        public From From { get; set; }

        public abstract object GenerateWriteData();
        public abstract void WriteData(string destinationDirectory, string destinationFileName);

        public DataFormat(string source, List<string> KeepValues, SourceDataType sourceDataType)
        {
            switch (sourceDataType)
            {
                case SourceDataType.API:
                    DataObjects = API(source, KeepValues);
                    break;
                case SourceDataType.Excel:
                    DataObjects = Excel(source, KeepValues);
                    break;
                case SourceDataType.JSON:
                    DataObjects = JSON(source, KeepValues);
                    break;
                case SourceDataType.XML:
                    DataObjects = XML(source, KeepValues);
                    break;
            }
        }
        public DataFormat() => DataObjects = new List<DataObject>();
        public DataFormat(List<DataObject> dataObjects) => DataObjects = dataObjects;
    }
}
