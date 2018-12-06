using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;



namespace CSV_file_reader
{
    class Program
    {

        
        private static List<RowData> ParseRawDataInToRows(StreamReader rawdata)
        {
            var rows = new List<RowData>( );
            string line;
            rawdata.ReadLine();
            while ((line = rawdata.ReadLine()) != null) 
                {
                    var fields = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    RowData data = new RowData(fields[0], int.Parse(fields[1]), int.Parse(fields[2]), double.Parse(fields[3]));
                    rows.Add(data);
                }
            return rows;
        }
        private static List<string> FindingDistinctProducts(List<RowData> rows)
        {
            List<string> norepeats = rows.Select(d => d.Product).Distinct().ToList();
            return norepeats;
        }
        private static MinMax CalculateMinMaxDurationandProductCount(List<RowData> rows, List<string> norepeats)
        {
            int Start = rows.Select(d => d.OriginYear).Min();
            int End = rows.Select(d => d.DevelopmentYear).Max();
            int Duration = End + 1 - Start;
            int NumberOfProducts = norepeats.Count;
            return new MinMax(Start, End, NumberOfProducts);
        }
        private static AccumulatedProducts AlgorithmCompressingList(MinMax minmax, List<RowData> rows, List<string> norepeats)
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
                        if(dictionary.TryGetValue(key, out double rowValue))
                        {
                            currentIncrementalValue = rowValue;
                        }
                        
                        originTotal += currentIncrementalValue;
                        accumulatedValues.Add(originTotal);
                    }
                }

                products[productCount-1] = new AccumulatedProduct(product, accumulatedValues);
            }

            return new AccumulatedProducts(minmax, products);
        }
        private static void Output(AccumulatedProducts accumulatedProducts)
        {
            string fileName = "C:\\users\\callum\\Documents\\WTW\\interleaved_productsoutput.csv";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                IMinMax range = accumulatedProducts.Range;
                writer.WriteLine(range.Start + ", " + range.Duration);
                foreach (var product in accumulatedProducts.Products )
                {
                    string toOutput = string.Join(", ", product.Values);
                    string finalOutput = product.Name + ", " + toOutput;
                    writer.WriteLine(finalOutput);
                }
            }
        }
        static void Main(string[] args)
        {
            try
            {
                using (var readerdata = new StreamReader("C:\\users\\callum\\Documents\\WTW\\interleaved_products.csv"))
                {
                    List<RowData> rows = ParseRawDataInToRows(readerdata);
                    List<string> distinctProducts = FindingDistinctProducts(rows);
                    MinMax minmax = CalculateMinMaxDurationandProductCount(rows, distinctProducts);
                    AccumulatedProducts accumulatedProducts = AlgorithmCompressingList(minmax, rows, distinctProducts);
                    Output(accumulatedProducts);
                    Console.WriteLine("done");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }  
    }
}
