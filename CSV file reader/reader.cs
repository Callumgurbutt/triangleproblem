using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    class Reader : IReader
    {
        public IEnumerable<RowData> Rows(IEnumerable<IRowData> GetRows)
        {
            List<RowData> rows = new List<RowData>();
            using (var readerdata = new StreamReader("C:\\users\\callum\\Documents\\WTW\\interleaved_products.csv"))
            {
                string line;
                readerdata.ReadLine();
                while ((line = readerdata.ReadLine()) != null)
                {
                    var fields = line.Split(new Char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    RowData data = new RowData(fields[0], int.Parse(fields[1]), int.Parse(fields[2]), double.Parse(fields[3]));
                    rows.Add(data);
                }
                return rows;
            }
        }
    }
}
