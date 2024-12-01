using PatzminiHD.CSLib.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLibTest.ExtensionMethodsTests
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void TestCount()
        {
            Assert.AreEqual(3, "This is a test string".Count('i'));
            Assert.AreEqual(0, "This is a test string".Count('z'));
        }
    }
}
