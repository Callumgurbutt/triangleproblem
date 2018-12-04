using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;



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
            MinMax minmax = new MinMax(Start, End, NumberOfProducts);
            return minmax;
        }
        
        private static RowData[] FindingAccumulatedValueAtSpecificYear(List<RowData> rows, List<string> norepeats, MinMax minmax)
        {
            RowData[] Accumulated = new RowData[rows.Count];
            for (int l = 0; l < minmax.NumberOfProducts; l++)
            {
                for (int i = minmax.Start; i <= minmax.End; i++)
                {
                    double originTotal = 0;
                    for (int j = i; j <= minmax.End; j++)
                    {
                        double currentIncrementalValue = 0;
                        for (int k = 0; k < rows.Count; k++)
                        {
                            if (norepeats[l] == rows[k].Product)
                            {
                                if (i == rows[k].OriginYear)
                                {
                                    if (j == rows[k].DevelopmentYear)
                                    {
                                        currentIncrementalValue = rows[k].IncrementalYear;
                                    }
                                }
                            }
                            Accumulated[k] = new RowData(rows[l].Product, rows[i].OriginYear, rows[j].DevelopmentYear, originTotal);
                        }
                        originTotal += currentIncrementalValue;
                    }
                }
            }
            return Accumulated;
        }
        private static AccumulatedProducts AlgorithmCompressingList(MinMax minmax, List<RowData> rows, List<string> norepeats)
        {
            List<double> accumulatedValues = null;
            AccumulatedProduct[] products = new AccumulatedProduct[minmax.NumberOfProducts];
            for (int l = 0; l < minmax.NumberOfProducts; l++)
            {
                for (int i = minmax.Start; i <= minmax.End; i++)
                {
                    double originTotal = 0;
                    for (int j = i; j <= minmax.End; j++)
                    {
                        double currentIncrementalValue = 0;
                        for (int k = 0; k < rows.Count; k++)
                        {
                            if (norepeats[l] == rows[k].Product)
                            {
                                if (i == rows[k].OriginYear)
                                {
                                    if (j == rows[k].DevelopmentYear)
                                    {
                                        currentIncrementalValue = rows[k].IncrementalYear;
                                    }
                                }
                            }
                        }
                        originTotal += currentIncrementalValue;
                    }
                    accumulatedValues.Add(originTotal); 
                }
                string name = norepeats[l] ;
                products[l] = new AccumulatedProduct(name, accumulatedValues);
            }
            return new AccumulatedProducts(minmax, products);
        }











        static void Main(string[] args)
        {

            try
            {

                using (var readerdata = new StreamReader("C:\\users\\callum\\Documents\\TWdata.csv"))
                {
                    List<RowData> rows = ParseRawDataInToRows(readerdata);
                    List<string> distinctProducts = FindingDistinctProducts(rows);
                    MinMax minmax = CalculateMinMaxDurationandProductCount(rows, distinctProducts);
                    double yearSpecificIncrementalValue = FindingIncrementalValueAtSpecificYear(rows, distinctProducts, l, i, j);
                    AlgorithmCompressingListAndOutputtingResult(minmax, rows, distinctProducts);
                    Console.Write(minmax.Start); Console.Write("  "); Console.WriteLine(minmax.End + 1 - minmax.Start);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Could not read file due to" + e);
            }

            Console.ReadLine();

        }   
            
            
            
            
            
            
            
            
            
            
            
            
            /*var rows = new List<RowData>();
                    string line;
                    readerdata.ReadLine();
                    while ((line = readerdata.ReadLine()) != null)
                    {
                        var fields = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        RowData data = new RowData(fields[0], int.Parse(fields[1]), int.Parse(fields[2]), double.Parse(fields[3]));
                        rows.Add(data);
                    }
                    List<string> norepeats = rows.Select(d => d.Product).Distinct().ToList();
                    int Start = rows.Select(d => d.OriginYear).Min();
                    int End = rows.Select(d =>d.DevelopmentYear).Max();
                    int Duration = End + 1 - Start;
                    int NoP = norepeats.Count;
                    string product2 = null;
                    string product1 = null;
                    string[] products = new string[NoP];
                    for (int l = 0; l < NoP; l++)
                    {
                        product1 = null;
                        for (int i = Start; i <= End; i++)
                        {
                            double originTotal = 0;
                            for (int j = i; j <= End; j++)
                            {
                                double currentIncrementalValue = 0;
                                for (int k = 0; k < rows.Count; k++)
                                {
                                    if (norepeats[l] == rows[k].Product)
                                    {
                                        if (i == rows[k].OriginYear)
                                        {
                                            if (j == rows[k].DevelopmentYear)
                                            {
                                                currentIncrementalValue = rows[k].IncrementalYear;
                                            }
                                        }
                                    }
                                }
                                originTotal += currentIncrementalValue;
                                if (i == End)
                                {
                                    if (j == End)
                                    {
                                        product1 += originTotal;
                                    }
                                    else
                                    {
                                        product1 += originTotal + ",";
                                    }
                                }
                                else
                                {
                                    product1 += originTotal + ",";
                                }
                            }
                        }
                        product2= norepeats[l] += "," + product1;
                        Console.WriteLine(product2);
                        products[l]=product2;
                    }*/
        
    }
}
