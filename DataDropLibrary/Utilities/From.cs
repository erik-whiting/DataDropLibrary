﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;
using DataDropLibrary.Models;
using Bytescout.Spreadsheet;

namespace DataDropLibrary.Utilities
{
    public abstract class From
    {
        public static List<DataObject> API(string url, List<string> KeepValues)
        {
            List<Dictionary<string, string>> response = new List<Dictionary<string, string>>();
            var client = new WebClient();
            var rawResponse = client.DownloadString(url);
            var jsonResponse = JsonConvert.DeserializeObject(rawResponse);
            JObject jObject = JObject.Parse(jsonResponse.ToString());
            List<JToken> jList = new List<JToken>();
            foreach (var j in jObject.Values().ToArray()) jList.Add(j);

            foreach (var items in jList)
            {
                foreach (var item in items.Children<JObject>())
                {
                    var itemDict = new Dictionary<string, string>();
                    foreach (JProperty prop in item.Properties())
                    {
                        itemDict.Add(prop.Name, prop.Value.ToString());
                    }
                    response.Add(itemDict);
                }
            }
            return Set(response, KeepValues);
        }

        public static List<DataObject> Excel(string source, List<string> KeepValues)
        {
            List<Dictionary<string, string>> response = new List<Dictionary<string, string>>();
            Spreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.LoadFromFile(source);
            Worksheet sheet = spreadsheet.Workbook.Worksheets.ByName("Sheet1");
            List<string> columns = new List<string>();
            var headerRange = sheet.UsedRangeColumnMax;
            for (int i = 0; i <= headerRange; i++) columns.Add(sheet.Cell(0, i).Value.ToString());
            for (int i = 1; i <= sheet.UsedRangeRowMax; i++)
            {
                var itemDict = new Dictionary<string, string>();
                for (int x = 0; x < columns.Count; x++) itemDict.Add(columns[x], sheet.Cell(i, x).Value.ToString());
                response.Add(itemDict);
            }
            return Set(response, KeepValues);
        }

        public static List<DataObject> JSON(string source, List<string> KeepValues)
        {
            var response = new List<Dictionary<string, string>>();
            var loadedJson = new LoadJson(source).JsonObjects;
            foreach (var j in loadedJson)
            {
                var itemDict = new Dictionary<string, string>();
                foreach (var item in j) itemDict.Add(item.Key, item.Value.ToString());
                response.Add(itemDict);
            }
            return Set(response, KeepValues);
        }

        public static List<DataObject> XML(string source, List<string> KeepValues)
        {
            var response = new List<Dictionary<string, string>>();
            XmlDocument doc = new XmlDocument();
            doc.Load(source);
            XDocument docx = XDocument.Parse(doc.InnerXml);
            var itemDict = new Dictionary<string, string>();
            foreach (XElement element in docx.Descendants().Where(p => p.HasElements == false))
            {
                string keyName = element.Name.LocalName;
                if (!itemDict.ContainsKey(keyName))
                {
                    itemDict.Add(keyName, element.Value);
                }
                else
                {
                    response.Add(itemDict);
                    itemDict = new Dictionary<string, string>();
                    itemDict.Add(keyName, element.Value);
                }
            }
            response.Add(itemDict);
            return Set(response, KeepValues);
        }

        public static List<DataObject> Set(List<Dictionary<string, string>> AllValues, List<string> KeepValues)
        {
            var DataObjects = new List<DataObject>();
            foreach (var value in AllValues)
            {
                var dataObject = new DataObject();
                var dict = new Dictionary<string, string>();
                foreach (var attribute in KeepValues) dict.Add(attribute, value[attribute]);
                dataObject.DataPairs.Add(dict);
                DataObjects.Add(dataObject);
            }
            return DataObjects;
        }

    }
}
