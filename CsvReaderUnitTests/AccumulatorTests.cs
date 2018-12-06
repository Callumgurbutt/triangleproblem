using System;
using System.Collections.Generic;
using System.Linq;
using CSV_file_reader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CsvReaderUnitTests
{
    [TestClass]
    public class AccumulatorTests
    {
        private Mock<IRowData> CreateRowDataMock(string product, int originYear, int devYear, double incrementalValue)
        {
            var mockData = new Mock<IRowData>();
            mockData.Setup(md => md.Product).Returns(product);
            mockData.Setup(md => md.OriginYear).Returns(originYear);
            mockData.Setup(md => md.DevelopmentYear).Returns(devYear);
            mockData.Setup(md => md.IncrementalValue).Returns(incrementalValue);
            return mockData;
        }

        [TestMethod]
        public void AccumulatorTests_IfSingleRow_ThenStartsAtSpecifiedOrigin()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            Assert.AreEqual(1, accumulated.Range.Start);
        }

        [TestMethod]
        public void AccumulatorTests_IfSingleRow_ThenEndsAtSpecifiedDevelopement()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            Assert.AreEqual(1, accumulated.Range.End);
        }

        [TestMethod]
        public void AccumulatorTests_Ifnegativevalue_ThenNegativeAccumulatedResult()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, -10);
            var mockData1 = CreateRowDataMock("P1", 1, 2, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.Single().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(-10, accumulatedValues[0]);
            Assert.AreEqual(10, accumulatedValues[1]);
        }

        [TestMethod]
        public void AccumulatorTests_IfRepeatedValue_ThenExceptionThrown()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P1", 1, 1, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };
            bool exceptionThrown = false;
            //act
            
            try
            {
                var accumulated = sut.Accumulate(mockDatas);
            }
            catch (ArgumentException)
            {

                exceptionThrown = true;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
        }
        [TestMethod]
        public void AccumulatorTests_IfNoDataIn_ThenThrownException()
        {
            //arrange
            var sut = new Accumulator();
            
            List<IRowData> mockDatas = new List<IRowData>() { };
            bool exceptionThrown = false;
            //act
            try
            {
                var accumulated = sut.Accumulate(mockDatas);
            }
            catch (InvalidOperationException)
            {
                exceptionThrown = true;
            }
            //assert
            Assert.IsTrue(exceptionThrown);
        }
        [TestMethod]
        public void AccumulatorTests_IfWrongOrder_ThenCorrectOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 2, 10);
            var mockData1 = CreateRowDataMock("P1", 1, 1, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.Single().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(20, accumulatedValues[0]);
            Assert.AreEqual(30, accumulatedValues[1]);
            Assert.AreEqual(0, accumulatedValues[2]);
        }
        [TestMethod]
        public void AccumulatorTests_IfMaxValue_ThenOverflows()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, double.MaxValue);
            var mockData1 = CreateRowDataMock("P1", 1, 2, double.MaxValue);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.Single().Values.ToList();     
            Assert.AreNotEqual(double.MaxValue, accumulatedValues[1]);

        }
        [TestMethod]
        public void AccumulatorTests_IfCorrectInput_ThenCorrectOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P1", 1, 2, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.Single().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(30, accumulatedValues[1]);
            Assert.AreEqual(0, accumulatedValues[2]);
        }
        [TestMethod]
        public void AccumulatorTests_IfGapsinData_ThenTreatAsValueIsZero()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P1", 2, 2, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.Single().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(10, accumulatedValues[1]);
            Assert.AreEqual(20, accumulatedValues[2]);
        }
        [TestMethod]
        public void AccumulatorTests_IfMultipleProductInput_ThenCorrectOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P2", 1, 1, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.First().Values.ToList();
            List<double> accumulatedValues1 = accumulated.Products.Last().Values.ToList();
            Assert.AreEqual(1, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(20, accumulatedValues1[0]);
        }
        [TestMethod]
        public void AccumulatorTests_IfMultipleProductInsideEachOther_ThenCorrectOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P2", 1, 1, 20);
            var mockData2 = CreateRowDataMock("P2", 1, 2, 10);
            var mockData3 = CreateRowDataMock("P1", 1, 2, 30);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object, mockData2.Object, mockData3.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.First().Values.ToList();
            List<double> accumulatedValues1 = accumulated.Products.Last().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(40, accumulatedValues[1]);
            Assert.AreEqual(20, accumulatedValues1[0]);
            Assert.AreEqual(30, accumulatedValues1[1]);
        }
        [TestMethod]
        public void AccumulatorTests_IfMultipleProductInterweaved_ThenCorrectOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P2", 1, 1, 20);
            var mockData2 = CreateRowDataMock("P1", 1, 2, 30);
            var mockData3 = CreateRowDataMock("P2", 1, 2, 10);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object, mockData2.Object, mockData3.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.First().Values.ToList();
            List<double> accumulatedValues1 = accumulated.Products.Last().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(40, accumulatedValues[1]);
            Assert.AreEqual(20, accumulatedValues1[0]);
            Assert.AreEqual(30, accumulatedValues1[1]);
        }
        [TestMethod]
        public void AccumulatorTests_IfMultipleProductSeperated_ThenCorrectOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P1", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P1", 1, 2, 30);
            var mockData2 = CreateRowDataMock("P2", 1, 1, 20);
            var mockData3 = CreateRowDataMock("P2", 1, 2, 10);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object, mockData2.Object, mockData3.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.First().Values.ToList();
            List<double> accumulatedValues1 = accumulated.Products.Last().Values.ToList();
            Assert.AreEqual(3, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(40, accumulatedValues[1]);
            Assert.AreEqual(20, accumulatedValues1[0]);
            Assert.AreEqual(30, accumulatedValues1[1]);
        }
        [TestMethod]
        public void AccumulatorTests_If2ndProductis1st_Then2ndProductis1stOutput()
        {
            //arrange
            var sut = new Accumulator();
            var mockData = CreateRowDataMock("P2", 1, 1, 10);
            var mockData1 = CreateRowDataMock("P1", 1, 1, 20);
            List<IRowData> mockDatas = new List<IRowData>() { mockData.Object, mockData1.Object };

            //act
            var accumulated = sut.Accumulate(mockDatas);

            //assert
            List<double> accumulatedValues = accumulated.Products.First().Values.ToList();
            List<double> accumulatedValues1 = accumulated.Products.Last().Values.ToList();
            Assert.AreEqual(1, accumulatedValues.Count);
            Assert.AreEqual(10, accumulatedValues[0]);
            Assert.AreEqual(20, accumulatedValues1[0]);
        }
    }
}
