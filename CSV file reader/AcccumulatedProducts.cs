using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    class AccumulatedProducts
    {
        public MinMax Range { get; }
        public IEnumerable<AccumulatedProduct> Products{get;}
        public AccumulatedProducts(MinMax range, IEnumerable<AccumulatedProduct> products)
        {
            this.Range = range;
            this.Products = products;

        }

    }
}
