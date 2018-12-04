using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    public class RowData
    {
        public string Product { get; }
        public int OriginYear { get; }
        public int DevelopmentYear { get; }
        public double IncrementalYear { get; }

        public RowData(string product, int origin, int development, double incremental)
        {
            this.Product = product;
            this.OriginYear = origin;
            this.DevelopmentYear = development;
            this.IncrementalYear = incremental;
        }
    }
}
