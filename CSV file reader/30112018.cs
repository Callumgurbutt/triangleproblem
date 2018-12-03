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
                {
                    string line;
                    //string[] row;
                    while ((line = readerdata.ReadLine()) != null)
                    {
                        var fields = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Product.Add(fields[0]);
                        if (fields.Length > 1)
                            OriginYear.Add(fields[1]);
                        if (fields.Length > 2)
                            DevelopmentYear.Add(fields[2]);
                        if (fields.Length > 3)
                            IncrementalValue.Add(fields[3]);
                    }
                    foreach (var i in Product)
                    {
                        Console.WriteLine(i);
                    }
                    //while (i == "comp");
                    foreach (var j in OriginYear)
                    {
                        Console.WriteLine(j);
                    }
                    foreach (var i in DevelopmentYear)
                    {
                        Console.WriteLine(i);
                    }
                    foreach (var i in IncrementalValue)
                    {
                        Console.WriteLine(i);
                    }
                }
                

            }
            catch(Exception e)
            {
                Console.WriteLine("Could not read file due to" + e);
            }
            
            Console.ReadLine();
            //string data = File.ReadAllText ("C:\\Users\\Callum\\Documents\\TWdata.csv");
            //Console.WriteLine(data);
        }
    }
}
