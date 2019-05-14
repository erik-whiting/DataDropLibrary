using DataDropLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities.ModelExtensions
{
    public static class BulkActions
    {
        public delegate object DataGenerator();
        public delegate void DataWriter(string destinationDir, string destinationFile);

        public static IEnumerator<DataGenerator> BulkGenerator(IEnumerable<DataFormat> dataFormats)
        {
            foreach (var df in dataFormats) yield return new DataGenerator(df.GenerateWriteData);
        }

        public static void WriteDataFormatsToFile(IEnumerable<DataFormat> dataFormats, string targetFolder, string fileSequenceStart)
        {
            var dataGenerator = BulkGenerator(dataFormats);
            int sequence = 1;
            while (dataGenerator.MoveNext())
            {
                var objectType = dataGenerator.Current.Target.GetType();
                string fileExtension = "";
                switch (objectType.Name.ToLower())
                {
                    case "jsondataformat":
                        fileExtension = ".json";
                        break;
                    case "xmldataformat":
                        fileExtension = ".xml";
                        break;
                    case "sqldataformat":
                        fileExtension = ".sql";
                        break;
                    case "exceldataformat":
                        fileExtension = ".xlsx";
                        break;
                    default:
                        fileExtension = ".json";
                        break;
                }
                try
                {
                    using (StreamWriter file = new StreamWriter(targetFolder + "\\" + fileSequenceStart + sequence.ToString() + fileExtension))
                    {
                        file.WriteLine(dataGenerator.Current());
                        sequence += 1;
                    }
                } catch (Exception ex)
                {
                    using (StreamWriter file = new StreamWriter(targetFolder + "\\" + fileSequenceStart + sequence.ToString() + fileExtension))
                    {
                        file.WriteLine(ex.Message);
                        sequence += 1;
                    }
                }
            }
        }

    }
}
