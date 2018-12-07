using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSV_file_reader
{
    class CsvFileWriter : IWriter
    {
        public void Write(string summaryLine, IEnumerable<string> productLine)
        {
            string fileName = "C:\\users\\callum\\Documents\\WTW\\interleaved_productsoutput.csv";
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine(summaryLine);
                foreach (string product in productLine)
                {
                    writer.WriteLine(product);
                }
            }
        }
    }
}
