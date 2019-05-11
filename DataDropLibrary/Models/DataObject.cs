using DataDropLibrary.Utilities;
using System.Collections.Generic;

namespace DataDropLibrary.Models
{
    public class DataObject
    {
        public List<Option<Dictionary<string, string>>> DataPairOptions { get; set; }
        public List<Dictionary<string, string>> DataPairs { get; set; }
        public DataObject()
        {
            DataPairOptions = new List<Option<Dictionary<string, string>>>();
            DataPairs = new List<Dictionary<string, string>>();
        }

        public DataObject(List<Dictionary<string, string>> pairs)
        {
            foreach (var pair in pairs) TryGetPairs(pair);
        }

        public void TryGetPairs(Dictionary<string, string> pairs)
        {
            bool keysAreGood = false;
            bool valsAreGood = false;
            foreach (var x in pairs)
            {
                keysAreGood = !(x.Key == null);
                valsAreGood = !(x.Value == null);
            }
            if (keysAreGood && valsAreGood) DataPairs.Add(new Some<Dictionary<string, string>>(pairs).Content);
        }
    }
}
