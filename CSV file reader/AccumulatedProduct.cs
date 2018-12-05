using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    class AccumulatedProduct
    {
        public string Name { get; }
        public IEnumerable<double> Values { get; }
        public AccumulatedProduct(string name, IEnumerable<double> values)
        {
            this.Name = name;
            this.Values = values.ToList();

        }
    }
}
