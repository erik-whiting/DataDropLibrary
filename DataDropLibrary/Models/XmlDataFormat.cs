using System;
using System.Collections.Generic;
using System.IO;
using DataDropLibrary.Utilities;

namespace DataDropLibrary.Models
{
    public class XmlDataFormat : DataFormat
    {
        public override object GenerateWriteData()
        {
            string xmlString = "<?xml version='1.0' encoding='UTF-8'?>";
            xmlString += "<root>";
            foreach (var dataObject in DataObjects)
            {
                xmlString += "<object>";
                foreach (var attr in Helpers.GetAttributes(DataObjects))
                {
                    foreach (var pair in dataObject.DataPairs)
                    {
                        xmlString += "<" + attr + ">";
                        xmlString += pair[attr];
                        xmlString += "</" + attr + ">";
                    }
                }
                xmlString += "</object>";
            }
            xmlString += "</root>";
            return xmlString.Replace("'", "\"");
        }

        public override void WriteData(string destinationDirectory, string destinationFileName)
        {
            using (StreamWriter file = new StreamWriter(destinationDirectory + "\\" + destinationFileName + ".xml"))
            {
                file.WriteLine(GenerateWriteData());
            }
        }

        public XmlDataFormat() : base() { }
        public XmlDataFormat(string source, List<string> KeepValues, SourceDataType sourceDataType) :
            base(source, KeepValues, sourceDataType)
        { }
        public XmlDataFormat(List<DataObject> dataObjects) : base(dataObjects) { }
    }
}
