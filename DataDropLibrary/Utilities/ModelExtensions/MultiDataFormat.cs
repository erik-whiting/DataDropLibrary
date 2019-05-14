using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Utilities.ModelExtensions
{
    public class MultiDataFormat<T>
    {
        public string Source;
        public IEnumerable<string> KeepValues;
        public SourceDataType dataType;

        public MultiDataFormat(string source, IEnumerable<string> keepValues, SourceDataType sourceDataType)
        {
            Source = source;
            KeepValues = keepValues;
            dataType = sourceDataType;
        }

        public IEnumerable<string> ValsToList()
        {
            foreach (var val in KeepValues)
            {
                yield return val;
            }
        }

    }
}
