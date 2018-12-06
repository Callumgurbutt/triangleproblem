using System;
using System.Collections.Generic;
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
    }
}
