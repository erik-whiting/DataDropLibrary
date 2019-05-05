using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Interfaces
{
    interface IFrom
    {
        List<Dictionary<string, string>> API(string source);
        List<Dictionary<string, string>> Excel(string source);
        List<Dictionary<string, string>> JSON(string source);
        List<Dictionary<string, string>> XML(string source);
    }
}
