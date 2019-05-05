using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DataDropLibrary.Models
{
    class JsonDataFormat : DataFormat
    {
        public override object GenerateWriteData()
        {
            List<Dictionary<string, string>> SerializableDataObjects = new List<Dictionary<string, string>>();
            foreach (var dataObj in DataObjects)
            {
                foreach (var pair in dataObj.DataPairs) SerializableDataObjects.Add(pair);
            }
            return JsonConvert.SerializeObject(SerializableDataObjects);
        }

        public override void WriteData(string destinationDirectory, string destinationFileName)
        {
            string fullFilePath = Path.Combine(destinationDirectory, destinationFileName);
            using (StreamWriter file = new StreamWriter(fullFilePath)) file.WriteLine(GenerateWriteData());
        }
    }
}
