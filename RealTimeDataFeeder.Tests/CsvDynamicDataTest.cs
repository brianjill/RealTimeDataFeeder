using System;
using System.Configuration;
using DataParser;
using DataParser.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RealTimeDataFeeder.Tests
{
    [TestClass]
    public class CsvDynamicDataTest
    {
        public AbstractData Target;
        
        
        [TestInitialize]
        public void init()
        {
            Target = new CsvDynamicData();
        }

        [TestMethod]
        public void FilePath_Should_Be_CementDataxml()
        {

            Assert.AreEqual(@"D:\jill\RTI\RealTimeDataFeeder\data\DynamicData.csv", Target.FilePath);

            Assert.IsTrue(Target.FilePath.Contains("DynamicData.csv"));
        }

        [TestMethod]
        public void Items0_Time_Should_Be_()
        {
            Target.Parse();

            Assert.IsNotNull(Target.Items[0]);

            var items0 = (CsvDynamicItem)Target.Items[0];

            CsvDynamicItem Item0 = (CsvDynamicItem) Target.Items[0];
            Assert.AreEqual("04:00", Item0.Time);

            
            
        }
    }
}
