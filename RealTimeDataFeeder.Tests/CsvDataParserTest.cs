using System;
using System.Text;
using System.Collections.Generic;
using DataParser;
using DataParser.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RealTimeDataFeeder.Tests
{
    /// <summary>
    /// Summary description for DataParserTest
    /// </summary>
    [TestClass]
    public class CsvDataParserTest
    {
        public DataParser<CsvData> TargetXml;
        public Mock<CsvData> CsvDataMock;
        
        [TestInitialize]
        public void init()
        {

            CsvDataMock = new Mock<CsvData>();

            var testDate = DateTime.Now.AddDays(1);
            var list = new List<CsvItem>
            {
                new CsvItem {Ticks = testDate.Ticks, BitDepth = (float) 0.33},
                new CsvItem {Ticks = testDate.Ticks, BitDepth = (float) 0.37}
            };

            CsvDataMock.Object.Items = new List<Item>(list);
            CsvDataMock.SetupAllProperties();
            TargetXml = new DataParser<CsvData>(CsvDataMock.Object);
        }

        
        [TestMethod]
        public void Json_Should_Contain_BitDepth()
        {
            TargetXml.Parse();

            var json = TargetXml.TransformToJson();

            Assert.AreEqual(true, json.Contains("BitDepth"));
            Assert.AreEqual(true, json.Contains("\"BitDepth\":0.33"));

            

        }
    }
}
