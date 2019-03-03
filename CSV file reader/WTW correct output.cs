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
        static void Main(string[] args)
        {
            try
            {
                TriangleProcessor processor = new TriangleProcessor(
                    "C:\\users\\callum\\Documents\\WTW\\interleaved_products.csv",
                    "C:\\users\\callum\\Documents\\WTW\\interleaved_productsoutput.csv");
                processor.ProcessTriangleProblem();
                Console.WriteLine("done");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadLine();
        }  
    }
}
