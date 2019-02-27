using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CSV_file_reader;

namespace User_interface
{
    public partial class Form1 : Form
    {
        string inputFileName;
        string outputFileName;

        public Form1()
        {
            InitializeComponent();
        }
        private void FileFinder_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV|*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                inputFileName = ofd.FileName;
            }
        }
        private void OutputFileNameBox_TextChanged(object sender, EventArgs e)
        {
            string outputName = Convert.ToString(OutputFileNameBox.Text);
            int index = inputFileName.LastIndexOf("\\");
            if (index > 0)
                 outputFileName = inputFileName.Substring(0, index + 1) + outputName + ".csv";
        }
        public class MinMax
        {
            public int Start { get; }
            public int End { get; }
            public int Duration { get; }
            public int NumberOfProducts { get; }

            public MinMax(int start, int end, int nop)
            {
                this.Start = start;
                this.End = end;
                this.Duration = (end - start) + 1;
                this.NumberOfProducts = nop;

            }
            private static List<RowData> ParseRawDataInToRows(StreamReader rawdata)
            {
                var rows = new List<RowData>();
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
        }
        class AccumulatedProducts
        {
            public MinMax Range { get; }
            public IEnumerable<IAccumulatedProduct> Products { get; }
            public AccumulatedProducts(MinMax range, IEnumerable<IAccumulatedProduct> products)
            {
                this.Range = range;
                this.Products = products.ToList();

            }
        }
        class AccumulatedProduct : IAccumulatedProduct
        {
            public string Name { get; }
            public IEnumerable<double> Values { get; }
            public AccumulatedProduct(string name, IEnumerable<double> values)
            {
                this.Name = name;
                this.Values = values.ToList();

            }
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
        private void Output(AccumulatedProducts accumulatedProducts)
        {
            string fileName = outputFileName;
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                MinMax range = accumulatedProducts.Range;
                writer.WriteLine(range.Start + ", " + range.Duration);
                foreach (var product in accumulatedProducts.Products)
                {
                    string toOutput = string.Join(", ", product.Values);
                    string finalOutput = product.Name + ", " + toOutput;
                    writer.WriteLine(finalOutput);
                }
            }
        }
        private static List<RowData> ParseRawDataInToRows(StreamReader rawdata)
        {
            var rows = new List<RowData>();
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
        private void Analyser_Click(object sender, EventArgs e)
        {
            try
            {
                using (var readerdata = new StreamReader(inputFileName))
                {
                    List<RowData> rows = ParseRawDataInToRows(readerdata);
                    List<string> distinctProducts = FindingDistinctProducts(rows);
                    MinMax minmax = CalculateMinMaxDurationandProductCount(rows, distinctProducts);
                    AccumulatedProducts accumulatedProducts = AlgorithmCompressingList(minmax, rows, distinctProducts);
                    Output(accumulatedProducts);
                    Console.WriteLine("done");
                    Finished.Text = "finished";
                }
            }
            catch (Exception )
            {
                Console.WriteLine("error");
            }
            Console.ReadLine();
        }
        private void Finished_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


