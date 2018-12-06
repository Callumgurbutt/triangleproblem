using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    class AccumulatedProducts : IAccumulatedProducts
    {
        public IMinMax Range { get; }
        public IEnumerable<IAccumulatedProduct> Products{get;}
        public AccumulatedProducts(MinMax range, IEnumerable<IAccumulatedProduct> products)
        {
            this.Range = range;
            this.Products = products.ToList();

        }

    }
}
