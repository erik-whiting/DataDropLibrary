using DataDropLibrary.Interfaces;
using System.Collections.Generic;

namespace DataDropLibrary.Models
{
    public abstract class RawSource : ISource
    {
        public string Source { get; set; }
        public List<Dictionary<string, string>> Attributes { get; set; }



        public RawSource(string source)
        {
            Source = source;
            Attributes = new List<Dictionary<string, string>>();
        }

        public RawSource()
        {
            Source = string.Empty;
            Attributes = new List<Dictionary<string, string>>();
        }
    }
}
