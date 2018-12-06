using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    public class Accumulator : IAccumulator
    {
        private static List<string> FindingDistinctProducts(List<IRowData> rows)
        {
            List<string> norepeats = rows.Select(d => d.Product).Distinct().ToList();
            return norepeats;
        }

        private static MinMax CalculateMinMaxDurationandProductCount(List<IRowData> rows, List<string> norepeats)
        {
            int Start = rows.Select(d => d.OriginYear).Min();
            int End = rows.Select(d => d.DevelopmentYear).Max();
            int Duration = End + 1 - Start;
            int NumberOfProducts = norepeats.Count;
            return new MinMax(Start, End, NumberOfProducts);
        }

        private static AccumulatedProducts AlgorithmCompressingList(MinMax minmax, List<IRowData> rows, List<string> norepeats)
        {
            List<double> accumulatedValues = new List<double>();
            AccumulatedProduct[] products = new AccumulatedProduct[norepeats.Count];
            int productCount = 0;
            int totalNumberOfRows = rows.Count;

            Dictionary<Tuple<string, int, int>, double> dictionary = rows.ToDictionary(
                (rd) => new Tuple<string, int, int>(rd.Product, rd.OriginYear, rd.DevelopmentYear),
                (rd) => rd.IncrementalValue);

            foreach (var product in norepeats)
            {
                accumulatedValues.Clear();
                productCount++;

                for (int originYear = minmax.Start; originYear <= minmax.End; originYear++)
                {
                    double originTotal = 0;
                    for (int developmentYear = originYear; developmentYear <= minmax.End; developmentYear++)
                    {
                        double currentIncrementalValue = 0;

                        Tuple<string, int, int> key = new Tuple<string, int, int>(product, originYear, developmentYear);
                        if (dictionary.TryGetValue(key, out double rowValue))
                        {
                            currentIncrementalValue = rowValue;
                        }

                        originTotal += currentIncrementalValue;
                        accumulatedValues.Add(originTotal);
                    }
                }

                products[productCount - 1] = new AccumulatedProduct(product, accumulatedValues);
            }

            return new AccumulatedProducts(minmax, products);
        }

        public IAccumulatedProducts Accumulate(IEnumerable<IRowData> rowData)
        {
            List<IRowData> rows = rowData.ToList();
            List<string> distinctProducts = FindingDistinctProducts(rows);
            MinMax minmax = CalculateMinMaxDurationandProductCount(rows, distinctProducts);
            return AlgorithmCompressingList(minmax, rows, distinctProducts);
        }
    }
}
