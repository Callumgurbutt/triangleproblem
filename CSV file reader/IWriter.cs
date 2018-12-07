using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    public interface IWriter
    {
        void Write(string summaryLine, IEnumerable<string> productLines);
    }
}
