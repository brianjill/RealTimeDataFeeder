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
    public class DataParserTest
    {
        public DataParser<XmlData> TargetXml;
        public Mock<XmlData> XmlDataMock;

        [TestInitialize]
        public void init()
        {
            
            XmlDataMock = new Mock<XmlData>();

            var testDate = DateTime.Now.AddDays(1);
            var list = new List<XmlItem>
            {
                new XmlItem {Ticks = testDate.Ticks, ANNVEL_MMAX = (float) 0.83},
                new XmlItem {Ticks = testDate.Ticks, ANNVEL_MMAX = (float) 0.87}
            };

            XmlDataMock.Object.Items = new List<Item>(list);
            XmlDataMock.SetupAllProperties();
            TargetXml = new DataParser<XmlData>(XmlDataMock.Object);
        }

        
        [TestMethod]
        public void Json_Should_Contain_ANNVEL_MMAX()
        {
            TargetXml.Parse();

            var json = TargetXml.TransformToJson();

            Assert.AreEqual(true, json.Contains("ANNVEL_MMAX"));
            Assert.AreEqual(true, json.Contains("\"ANNVEL_MMAX\":0.83"));

            

        }
    }
}
