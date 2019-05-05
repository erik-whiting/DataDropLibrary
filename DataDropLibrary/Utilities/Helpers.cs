using DataDropLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities
{
    class Helpers
    {
        public static List<string> GetAttributes(List<DataObject> dataObjects)
        {
            var attributes = new List<string>();
            foreach (var attr in dataObjects.FirstOrDefault().DataPairs)
            {
                foreach (var a in attr) attributes.Add(a.Key);
            }
            return attributes;
        }

        public static object GenerateSqlInsert(List<DataObject> DataObjects, string TableName)
        {
            var columns = GetAttributes(DataObjects);

            string InsertStatement = "INSERT INTO " + TableName + " (";
            foreach (var column in columns)
            {
                if (column != columns.Last())
                {
                    InsertStatement += column + ", ";
                }
                else
                {
                    InsertStatement += column;
                }
            }
            InsertStatement += ") VALUES" + Environment.NewLine;
            InsertStatement += "(";
            foreach (var value in DataObjects)
            {
                foreach (var column in columns)
                {
                    foreach (var val in value.DataPairs)
                    {
                        if (column != columns.Last())
                        {
                            InsertStatement += "\"" + val[column] + "\", ";
                        }
                        else
                        {
                            if (value != DataObjects.Last())
                            {
                                InsertStatement += "\"" + val[column] + "\"),";
                                InsertStatement += Environment.NewLine + "(";
                            }
                            else
                            {
                                InsertStatement += "\"" + val[column] + "\")";
                            }

                        }
                    }
                }
            }
            InsertStatement += ";";

            return InsertStatement;
        }

    }
}
