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
        static void Main(string[] args)
        {
            
            var Product = new List<string>();
            var OriginYear = new List<string>();
            var DevelopmentYear = new List<string>();
            var IncrementalValue = new List<string>();


            try
            {
                using (var readerdata = new StreamReader("C:\\users\\callum\\Documents\\TWdata.csv"))
                //read the data from the csv file
                {
                    string line;
                    while ((line = readerdata.ReadLine()) != null)
                    //split the data up into individual lines
                    {
                        var fields = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        //split the lines into columns by the commas
                        Product.Add(fields[0]);
                        if (fields.Length > 1)
                            OriginYear.Add(fields[1]);
                        if (fields.Length > 2)
                            DevelopmentYear.Add(fields[2]);
                        if (fields.Length > 3)
                            IncrementalValue.Add(fields[3]);
                        //assign each column to list strings
                    }
                    Product.RemoveAt(0);
                    OriginYear.RemoveAt(0);
                    DevelopmentYear.RemoveAt(0);
                    IncrementalValue.RemoveAt(0);
                    //remove the column heading so that it is just the data in the lists

                    List<int> Origin = OriginYear.Select(x => int.Parse(x)).ToList();
                    List<int> development = DevelopmentYear.Select(x => int.Parse(x)).ToList();
                    List<double> incremental = IncrementalValue.Select(x => double.Parse(x)).ToList();
                    //change the data from strings to numbers so that the can be analysed

                    /*foreach (var i in Product)
                    {
                        Console.WriteLine(i);
                    }
                    Console.WriteLine(" ");
                    foreach (double x in Origin)
                    {
                        Console.WriteLine(x);
                    }
                    Console.WriteLine(" ");
                    foreach (double x in development)
                    {
                        Console.WriteLine(x);
                    }
                    Console.WriteLine(" ");
                    foreach (double x in incremental)
                    {
                        Console.WriteLine(x);
                    }*/
                    double Start = Origin.Min();
                    double Duration = development.Max() + 1 - Start;
                    Console.Write(Origin.Min()); Console.Write("  "); Console.WriteLine(development.Max() + 1 - Start);
                    // write start year and how many years the program runs for
                    string product2 = null;
                    string product1 = null;
                    List<string> norepeats = Product.Distinct().ToList();
                    string[] products = new string[norepeats.Count];
                    for (int l = 0; l < norepeats.Count; l++)
                    {
                        product1 = null;
                        for (int i = Origin.Min(); i <= development.Max(); i++)
                        {
                            double originTotal = 0;
                            for (int j = i; j <= development.Max(); j++)
                            {
                                double currentIncrementalValue = 0;
                                for (int k = 0; k < Product.Count; k++)
                                {
                                    if (norepeats[l] == Product[k])
                                    {
                                        if (i == Origin[k])
                                        {
                                            if (j == development[k])
                                            {
                                                currentIncrementalValue = incremental[k];
                                            }

                                        }
                                    }
                                    

                                    
                                }
                                originTotal += currentIncrementalValue;
                                //Console.WriteLine(originTotal );
                                if (i == Origin.Max())
                                {
                                    if (j == development.Max())
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
                        //products.Add(product1);
                        
                    }
                }
            }
            
            catch (Exception e)
            {
                Console.WriteLine("Could not read file due to" + e);
            }
            
            Console.ReadLine();
            //string data = File.ReadAllText ("C:\\Users\\Callum\\Documents\\TWdata.csv");
            //Console.WriteLine(data);
        }
    }
}
