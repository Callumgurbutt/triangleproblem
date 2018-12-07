using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSV_file_reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace CsvReaderUnitTests
{
    [TestClass]
    public class OutputBuilderTests
    {
        private static IMinMax GetMinMaxMock(int start, int end, int numberOfProducts)
        {
            var mockMinMax = new Mock<IMinMax>();
            mockMinMax.SetupGet(mmm => mmm.Start).Returns(start);
            mockMinMax.SetupGet(mmm => mmm.End).Returns(end);
            mockMinMax.SetupGet(mmm => mmm.NumberOfProducts).Returns(numberOfProducts);
            return mockMinMax.Object;
        }
        private static IAccumulatedProduct GetAccumulatedProductMock(string name, IEnumerable<double> values)
        {
            var mockAccProduct = new Mock<IAccumulatedProduct>();
            mockAccProduct.SetupGet(map => map.Name).Returns(name);
            mockAccProduct.SetupGet(map => map.Values).Returns(values);
            return mockAccProduct.Object;
        }

        private static IAccumulatedProducts GetAccumulatedProductsMock(IMinMax range, IEnumerable<IAccumulatedProduct> products)
        {
            var mockAccProducts = new Mock<IAccumulatedProducts>();
            mockAccProducts.SetupGet(maps => maps.Range).Returns(range);
            mockAccProducts.SetupGet(maps => maps.Products).Returns(products);
            return mockAccProducts.Object;
        }

        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_NullInput_ThrowsException()
        {
            //arrange
            var sut = new OutputBuilder();
            bool exceptionThrown = false;
            string exceptionMessage = "";
            //act
            try
            {
                sut.BuildProductLines(null);
            }
            catch (ArgumentNullException e)
            {
                exceptionThrown = true;
                exceptionMessage = e.Message;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
            Assert.AreEqual("Value cannot be null." + Environment.NewLine + "Parameter name: products", exceptionMessage);
        }

        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_NullProducts_ThrowsException()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, null);
            bool exceptionThrown = false;
            string exceptionMessage = "";
            //act
            try
            {
                sut.BuildProductLines(mockDatas);
            }
            catch (InvalidOperationException e)
            {
                exceptionThrown = true;
                exceptionMessage = e.Message;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
            Assert.AreEqual("The list of accumulated products is not allowed to be null", exceptionMessage);
        }

        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_IfNoDataIn_ThrowsException()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, new IAccumulatedProduct[] { });
            bool exceptionThrown = false;
            string exceptionMessage = "";
            //act
            try
            {
                sut.BuildProductLines(mockDatas);
            }
            catch (InvalidOperationException e)
            {
                exceptionThrown = true;
                exceptionMessage = e.Message;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
            Assert.AreEqual("The list of accumulated products is not allowed to be empty", exceptionMessage);
        }

        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_NullProduct_ThrowsException()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test

            IAccumulatedProduct[] mockData =
            {
                GetAccumulatedProductMock("a", new double[0]),
                null,
                GetAccumulatedProductMock("b", new double[0]),
            };
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, mockData);
            bool exceptionThrown = false;
            string exceptionMessage = "";
            //act
            try
            {
                sut.BuildProductLines(mockDatas);
            }
            catch (InvalidOperationException e)
            {
                exceptionThrown = true;
                exceptionMessage = e.Message;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
            Assert.AreEqual("Accumulated product 1: Not allowed to be null", exceptionMessage);
        }
        
        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_NullProductName_ThrowsException()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test
            IAccumulatedProduct[] mockData =
            {
                GetAccumulatedProductMock("a", new double[0]),
                GetAccumulatedProductMock(null, new double[0]),
                GetAccumulatedProductMock("b", new double[0]),
            };
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, mockData);
            bool exceptionThrown = false;
            string exceptionMessage = "";
            //act
            try
            {
                sut.BuildProductLines(mockDatas);
            }
            catch (InvalidOperationException e)
            {
                exceptionThrown = true;
                exceptionMessage = e.Message;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
            Assert.AreEqual("Accumulated product 1 name: Not allowed to be null", exceptionMessage);
        }

        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_NullProductValue_ThrowsException()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test
            IAccumulatedProduct[] mockData =
            {
                GetAccumulatedProductMock("a", new double[0]),
                GetAccumulatedProductMock("b", null),
                GetAccumulatedProductMock("c", new double[0]),
            };
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, mockData);
            bool exceptionThrown = false;
            string exceptionMessage = "";
            //act
            try
            {
                sut.BuildProductLines(mockDatas);
            }
            catch (InvalidOperationException e)
            {
                exceptionThrown = true;
                exceptionMessage = e.Message;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
            Assert.AreEqual("Accumulated product 1 value: not allowed to be null", exceptionMessage);
        }

        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_EmptyProductName_StringStartsWithAComma()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test
            string names = string.Empty;
            IAccumulatedProduct[] mockData = {GetAccumulatedProductMock(names, new double[0]) };
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, mockData);
            //act
            List<string> outputString = sut.BuildProductLines(mockDatas);
            //assert
            Assert.AreEqual(", ", outputString.First());
        }
        [TestMethod]
        public void OutputBuilderTests_BuildProductLines_CorrectProductName_StringStartsWithProductNameAndAComma()
        {
            //arrange
            var sut = new OutputBuilder();
            IMinMax range = GetMinMaxMock(1, 5, 3); // Irrelevant to this test
            string names = "Working";
            IAccumulatedProduct[] mockData = { GetAccumulatedProductMock(names, new double[0]) };
            IAccumulatedProducts mockDatas = GetAccumulatedProductsMock(range, mockData);
            //act
            List<string> outputString = sut.BuildProductLines(mockDatas);
            //assert
            Assert.AreEqual("Working, ", outputString.First());
        }
    }
}
