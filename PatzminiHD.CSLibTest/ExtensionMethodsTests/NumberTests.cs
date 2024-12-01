using PatzminiHD.CSLib.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLibTest.ExtensionMethodsTests
{
    [TestClass]
    public class NumberTests
    {
        [TestMethod]
        public void TestClampInLimit()
        {
            Assert.AreEqual(10, 10.Clamp(0, 20));
            Assert.AreEqual(10.5, 10.5.Clamp(0.5, 20.25));
            Assert.AreEqual(3.141592, 3.141592.Clamp(0, 20));
            Assert.AreEqual(10m, 10m.Clamp(0, 20));
        }

        [TestMethod]
        public void TestClampAboveLimit()
        {
            Assert.AreEqual(5, 10.Clamp(0, 5));
            Assert.AreEqual(5.5, 10.5.Clamp(0, 5.5));
            Assert.AreEqual(0.5, 3.141592.Clamp(0, 0.5));
            Assert.AreEqual(5m, 10m.Clamp(0, 5m));
        }

        [TestMethod]
        public void TestClampBelowLimit()
        {
            Assert.AreEqual(15, 10.Clamp(15, 20));
            Assert.AreEqual(15.5, 10.5.Clamp(15.5, 20.25));
            Assert.AreEqual(15, 3.141592.Clamp(15, 20));
            Assert.AreEqual(15m, 10m.Clamp(15m, 20));
        }


        [TestMethod]
        public void TestMapInLimit()
        {
            Assert.AreEqual(50, 5.Map(0, 10, 0, 100));
            Assert.AreEqual(55, 5.5.Map(0, 10, 0, 100));
        }

        [TestMethod]
        public void TestMapAboveLimit()
        {
            Assert.AreEqual(150, 15.Map(0, 10, 0, 100));
            Assert.AreEqual(155, 15.5.Map(0, 10, 0, 100));
        }

        [TestMethod]
        public void TestMapBelowLimit()
        {
            Assert.AreEqual(-150, -15.Map(0, 10, 0, 100));
            Assert.AreEqual(-155, -15.5.Map(0, 10, 0, 100));
        }

        [TestMethod]
        public void TestIsEven()
        {
            int[] evenNumbers = { 2, -4, 6, -8, 16, -32, 64 };
            uint[] evenUnumbers = { 2, 6, 16, 64 };

            foreach (int number in evenNumbers)
            {
                Assert.IsTrue(number.IsEven());
            }

            foreach (int number in evenUnumbers)
            {
                Assert.IsTrue(number.IsEven());
            }

            int[] unevenNumbers = { 3, -5, 7, -9, 17, -33, 65 };
            uint[] unevenUnumbers = { 3, 7, 17, 65 };

            foreach (int number in unevenNumbers)
            {
                Assert.IsFalse(number.IsEven());
            }

            foreach (int number in unevenUnumbers)
            {
                Assert.IsFalse(number.IsEven());
            }
        }
    }
}
