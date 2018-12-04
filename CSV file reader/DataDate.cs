using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    class DataDate
    {
        public int L { get; }
        public int I { get; }
        public int J { get; }
        public DataDate(int l,int i,int j)
        {
            this.L = l;
            this.I = i;
            this.J = j;
        }
    }
}
