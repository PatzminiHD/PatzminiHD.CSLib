using PatzminiHD.CSLib.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLibTest.MathTests
{
    [TestClass]
    public class StatisticsTests
    {
        [TestMethod]
        public void TestAverage()
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double intAverage = 5;

            Assert.AreEqual(intAverage, Statistics.Average(ints));

            long[] longs = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double longAverage = 5;

            Assert.AreEqual(longAverage, Statistics.Average(longs));

            float[] floats = { 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f, 9.5f };
            float floatAverage = 5.5f;

            Assert.AreEqual(floatAverage, Statistics.Average(floats));

            decimal[] decimals = { 1.5m, 2.5m, 3.5m, 4.5m, 5.5m, 6.5m, 7.5m, 8.5m, 9.5m };
            decimal decimalAverage = 5.5m;

            Assert.AreEqual(decimalAverage, Statistics.Average(decimals));

            double[] doubles = { 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5 };
            double doubleAverage = 5.5;

            Assert.AreEqual(doubleAverage, Statistics.Average(doubles));
        }

        [TestMethod]
        public void TestMedian()
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double intMedian = 5;

            Assert.AreEqual(intMedian, Statistics.Median(ints));

            long[] longs = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double longMedian = 5;

            Assert.AreEqual(longMedian, Statistics.Median(longs));

            float[] floats = { 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f, 9.5f };
            float floatMedian = 5.5f;

            Assert.AreEqual(floatMedian, Statistics.Median(floats));

            decimal[] decimals = { 1.5m, 2.5m, 3.5m, 4.5m, 5.5m, 6.5m, 7.5m, 8.5m, 9.5m };
            decimal decimalMedian = 5.5m;

            Assert.AreEqual(decimalMedian, Statistics.Median(decimals));

            double[] doubles = { 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5 };
            double doubleMedian = 5.5;

            Assert.AreEqual(doubleMedian, Statistics.Median(doubles));
        }

        [TestMethod]
        public void TestStandardDevitation()
        {
            int[] ints = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double intStdDev = 2.7386127875258;

            Assert.AreEqual(intStdDev, Statistics.StandardDeviation(ints), 0.00001);

            long[] longs = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            double longStdDev = 2.7386127875258;

            Assert.AreEqual(longStdDev, Statistics.StandardDeviation(longs), 0.00001);

            float[] floats = { 1.5f, 2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f, 8.5f, 9.5f };
            float floatStdDev = 2.7386127875258f;

            Assert.AreEqual(floatStdDev, Statistics.StandardDeviation(floats), 0.00001);

            decimal[] decimals = { 1.5m, 2.5m, 3.5m, 4.5m, 5.5m, 6.5m, 7.5m, 8.5m, 9.5m };
            decimal decimalStdDev = 2.7386127875258m;

            Assert.AreEqual(decimalStdDev, Statistics.StandardDeviation(decimals), 0.00001m);

            double[] doubles = { 1.5, 2.5, 3.5, 4.5, 5.5, 6.5, 7.5, 8.5, 9.5 };
            double doubleStdDev = 2.7386127875258;

            Assert.AreEqual(doubleStdDev, Statistics.StandardDeviation(doubles), 0.00001);
        }

        [TestMethod]
        public void TestSqrt()
        {
            Assert.AreEqual(351.36306009596398663933384640418m, Statistics.Sqrt(123456m), 0.00001m);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "A negative number was inappropriately allowed")]
        public void TestSqrtExceptionThrow()
        {
            Statistics.Sqrt(-123456m);
        }
    }
}
