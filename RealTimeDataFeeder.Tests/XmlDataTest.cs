using System;
using System.Configuration;
using DataParser;
using DataParser.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RealTimeDataFeeder.Tests
{
    [TestClass]
    public class XmlDataTest
    {
        public AbstractData Target;
        
        
        [TestInitialize]
        public void init()
        {
            Target = new XmlData();
        }

        [TestMethod]
        public void FilePath_Should_Be_CementDataxml()
        {

            Assert.AreEqual(@"D:\jill\RTI\RealTimeDataFeeder\data\CementData.xml", Target.FilePath);

            Assert.IsTrue(Target.FilePath.Contains("CementData.xml"));
        }

        [TestMethod]
        public void Items0_Ticks_Should_Be_635976576000000000()
        {
            Target.Parse();

            Assert.IsNotNull(Target.Items[0]);

            var items0 = (XmlItem) Target.Items[0];

            Assert.AreEqual(635976576000000000, Target.Items[0].Ticks);

            Assert.AreEqual((float)0.87, items0.ANNVEL_MMAX);
            
        }
    }
}
