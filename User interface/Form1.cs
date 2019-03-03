using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                 this.outputFileName = this.inputFileName.Substring(0, index + 1) + outputName + ".csv";
        }
        private void Analyser_Click(object sender, EventArgs e)
        {
            try
            {
                TriangleProcessor processor = new TriangleProcessor(this.inputFileName, this.outputFileName);
                processor.ProcessTriangleProblem();
                Finished.Text = "finished";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.ReadLine();
        }        
    }
}


