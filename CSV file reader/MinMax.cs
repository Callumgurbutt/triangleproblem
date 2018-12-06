using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    class MinMax : IMinMax
    {
        public int Start { get; }
        public int End { get; }
        public int Duration { get; }
        public int NumberOfProducts { get; }

        public MinMax(int start, int end, int nop)
        {
            this.Start = start;
            this.End = end;
            this.Duration = (end - start) + 1;
            this.NumberOfProducts = nop;

        }
    }
}
