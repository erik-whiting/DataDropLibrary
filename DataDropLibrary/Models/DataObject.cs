﻿using System.Collections.Generic;

namespace DataDropLibrary.Models
{
    public class DataObject
    {
        public List<Dictionary<string, string>> DataPairs { get; set; }
        public DataObject() => DataPairs = new List<Dictionary<string, string>>();
    }
}
