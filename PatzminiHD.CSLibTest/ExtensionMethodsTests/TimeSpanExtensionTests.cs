using PatzminiHD.CSLib.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLibTest.ExtensionMethodsTests
{
    [TestClass]
    public class TimeSpanExtensionTests
    {
        [TestMethod]
        public void TestToString()
        {
            TimeSpan timeSpan = new TimeSpan(10, 5, 3, 6);

            Assert.AreEqual("10:5:03:06", TimeSpanExtension.ToString(timeSpan, "DD:HH:MM:SS"));
        }

    }
}
