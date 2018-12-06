using System.Collections.Generic;

namespace CSV_file_reader
{
    public interface IAccumulatedProduct
    {
        string Name { get; }
        IEnumerable<double> Values { get; }
    }
}