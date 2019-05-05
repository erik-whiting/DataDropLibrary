using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDropLibrary.Interfaces
{
    interface IDataFormat
    {
        object GenerateWriteData();
        void WriteData(string destinationDirectory, string desintationFileName);
    }
}
