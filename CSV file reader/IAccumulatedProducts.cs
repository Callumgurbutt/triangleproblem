using System.Collections.Generic;

namespace CSV_file_reader
{
    public interface IAccumulatedProducts
    {
        IEnumerable<IAccumulatedProduct> Products { get; }
        IMinMax Range { get; }
    }
}