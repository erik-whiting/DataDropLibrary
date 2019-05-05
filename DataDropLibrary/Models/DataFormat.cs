using DataDropLibrary.Interfaces;
using DataDropLibrary.Utilities;
using System.Collections.Generic;

namespace DataDropLibrary.Models
{
    public abstract class DataFormat : IDataFormat
    {
        public List<DataObject> DataObjects { get; set; }
        public To To { get; set; }

        public abstract object GenerateWriteData();
        public abstract void WriteData(string destinationDirectory, string destinationFileName);

        public DataFormat()
        {
            DataObjects = new List<DataObject>();
            To = new To();
        }
        public DataFormat(List<DataObject> dataObjects)
        {
            DataObjects = dataObjects;
            To = new To();
        }
    }
}
