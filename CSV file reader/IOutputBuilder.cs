using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    public interface IOutputBuilder
    {
        string BuildSummaryLine(IMinMax range);

        List<string> BuildProductLines(IAccumulatedProducts products);
    }
}