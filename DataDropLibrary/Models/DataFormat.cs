using DataDropLibrary.Interfaces;
using DataDropLibrary.Utilities;
using System.Collections.Generic;

namespace DataDropLibrary.Models
{
    public abstract class DataFormat : IDataFormat
    {
        public List<DataObject> DataObjects { get; set; }
        public From From { get; set; }

        public abstract object GenerateWriteData();
        public abstract void WriteData(string destinationDirectory, string destinationFileName);

        public DataFormat()
        {
            DataObjects = new List<DataObject>();
            From = new From();
        }
        public DataFormat(List<DataObject> dataObjects)
        {
            DataObjects = dataObjects;
            From = new From();
        }
    }
}
