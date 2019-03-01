using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    public class FileNames
    {   public string InputFileName { get; }
        public string OutputFileName { get; }

        public FileNames (string input, string output)
        {
            this.InputFileName = input;
            this.OutputFileName = output;
        }

    }
}
