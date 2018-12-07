using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CSV_file_reader
{
    public class OutputBuilder : IOutputBuilder
    {
        public string BuildSummaryLine(IMinMax range)
        {            
            string summaryLine =range.Start + ", " + range.Duration;
            return summaryLine;
        }

        public List<string> BuildProductLines(IAccumulatedProducts products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));
            if (products.Products == null)
                throw new InvalidOperationException("The list of accumulated products is not allowed to be null");
            if (!products.Products.Any())
                throw new InvalidOperationException("The list of accumulated products is not allowed to be empty");

            List<string> productLines = new List<string>();
            int index = 0;
            foreach (IAccumulatedProduct product in products.Products)
            {
                if (product == null)
                    throw new InvalidOperationException($"Accumulated product {index}: Not allowed to be null");
                if (product.Name == null)
                    throw new InvalidOperationException($"Accumulated product {index} name: Not allowed to be null");
                if (product.Values == null)
                    throw new InvalidOperationException($"Accumulated product {index} value: not allowed to be null");
                string toOutput = string.Join(", ", product.Values);
                string finalOutput = product.Name + ", " + toOutput;
                productLines.Add(finalOutput);

                index++;
            }
            return productLines;
        }
    }
}
