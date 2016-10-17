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
    public class CsvDynamicDataParserTest
    {
        public DataParser<CsvDynamicData> TargetXml;
        public Mock<CsvDynamicData> CsvDataMock;
        
        [TestInitialize]
        public void init()
        {

            CsvDataMock = new Mock<CsvDynamicData>();

            var testDate = DateTime.Now.AddDays(1);
            var properties = new Dictionary<string, string>();

            properties.Add("bit Depth", "14933.3");
            properties.Add("Time SDL Fast SPP Avg", "1816");
            properties.Add("Total Flow", "300");
            properties.Add("TQ", "12");
            properties.Add("rpm", "79");

            var list = new List<CsvDynamicItem>
            {
                new CsvDynamicItem
                {
                    Ticks = testDate.Ticks, 
                    Time=DateTime.Now.ToShortTimeString(), 
                    Properties = properties
                },
                 new CsvDynamicItem
                {
                    Ticks = testDate.Ticks, 
                    Time=DateTime.Now.ToShortTimeString(), 
                    Properties = properties
                }
            };

            CsvDataMock.Object.Items = new List<Item>(list);
            CsvDataMock.SetupAllProperties();
            TargetXml = new DataParser<CsvDynamicData>(CsvDataMock.Object);
        }

        
        [TestMethod]
        public void Json_Should_Contain_BitDepth()
        {
            TargetXml.Parse();

            var json = TargetXml.TransformToJson();

            Assert.AreEqual(true, json.Contains("bit Depth"));
            Assert.AreEqual(true, json.Contains("\"bit Depth\":\"14933.3\""));

            

        }
    }
}
