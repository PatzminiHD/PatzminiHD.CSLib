namespace PatzminiHD.CSLib.Math
{
    /// <summary>
    /// This class contains Methods for statistical analysis
    /// </summary>
    public class Statistics
    {
        /// <summary> Get the average value of a list of integers </summary>
        /// <param name="data">A list of intergers</param>
        /// <returns>The average</returns>
        public static double Average(IEnumerable<int> data)
        {
            return data.Sum() / (double)data.Count();
        }
        /// <summary> Get the average value of a list of longs </summary>
        /// <param name="data">A list of longs</param>
        /// <returns>The average</returns>
        public static double Average(IEnumerable<long> data)
        {
            return data.Sum() / (double)data.Count();
        }
        /// <summary> Get the average value of a list of floats </summary>
        /// <param name="data">A list of floats</param>
        /// <returns>The average</returns>
        public static double Average(IEnumerable<float> data)
        {
            return data.Sum() / data.Count();
        }
        /// <summary> Get the average value of a list of decimals </summary>
        /// <param name="data">A list of decimals</param>
        /// <returns>The average</returns>
        public static decimal Average(IEnumerable<decimal> data)
        {
            return data.Sum() / data.Count();
        }
        /// <summary> Get the average value of a list of doubles </summary>
        /// <param name="data">A list of doubles</param>
        /// <returns>The average</returns>
        public static double Average(IEnumerable<double> data)
        {
            return data.Sum() / data.Count();
        }


        /// <summary>
        /// Get the median for a list of integers
        /// </summary>
        /// <param name="data">A list of integers</param>
        /// <returns>The median</returns>
        public static double Median(IEnumerable<int> data)
        {
            var orderedData = data.Order();
            if (data.Count() % 2 == 0) //Is even
            {
                return (orderedData.ElementAt((data.Count() - 2) / 2) + orderedData.ElementAt(data.Count() / 2)) / 2;
            }
            else
            {
                return orderedData.ElementAt((data.Count() - 1) / 2);
            }
        }
        /// <summary>
        /// Get the median for a list of longs
        /// </summary>
        /// <param name="data">A list of longs</param>
        /// <returns>The median</returns>
        public static double Median(IEnumerable<long> data)
        {
            var orderedData = data.Order();
            if (data.Count() % 2 == 0) //Is even
            {
                return (orderedData.ElementAt((data.Count() - 2) / 2) + orderedData.ElementAt(data.Count() / 2)) / 2;
            }
            else
            {
                return orderedData.ElementAt((data.Count() - 1) / 2);
            }
        }
        /// <summary>
        /// Get the median for a list of floats
        /// </summary>
        /// <param name="data">A list of floats</param>
        /// <returns>The median</returns>
        public static double Median(IEnumerable<float> data)
        {
            var orderedData = data.Order();
            if (data.Count() % 2 == 0) //Is even
            {
                return (orderedData.ElementAt((data.Count() - 2) / 2) + orderedData.ElementAt(data.Count() / 2)) / 2;
            }
            else
            {
                return orderedData.ElementAt((data.Count() - 1) / 2);
            }
        }
        /// <summary>
        /// Get the median for a list of decimals
        /// </summary>
        /// <param name="data">A list of decimals</param>
        /// <returns>The median</returns>
        public static decimal Median(IEnumerable<decimal> data)
        {
            var orderedData = data.Order();
            if (data.Count() % 2 == 0) //Is even
            {
                return (orderedData.ElementAt((data.Count() - 2) / 2) + orderedData.ElementAt(data.Count() / 2)) / 2;
            }
            else
            {
                return orderedData.ElementAt((data.Count() - 1) / 2);
            }
        }
        /// <summary>
        /// Get the median for a list of doubles
        /// </summary>
        /// <param name="data">A list of doubles</param>
        /// <returns>The median</returns>
        public static double Median(IEnumerable<double> data)
        {
            var orderedData = data.Order();
            if (data.Count() % 2 == 0) //Is even
            {
                return (orderedData.ElementAt((data.Count() - 2) / 2) + orderedData.ElementAt(data.Count() / 2)) / 2;
            }
            else
            {
                return orderedData.ElementAt((data.Count() - 1) / 2);
            }
        }


        /// <summary>
        /// Get the standard deviation for a list of integers
        /// </summary>
        /// <param name="data">A list of integers</param>
        /// <returns>The standard deviation</returns>
        public static double StandardDeviation(IEnumerable<int> data)
        {
            double squaredDifferenceSum = 0;
            double average = Average(data);
            foreach (var value in data)
            {
                squaredDifferenceSum += (value - average) * (value - average);
            }

            return System.Math.Sqrt(squaredDifferenceSum / (data.Count() - 1));
        }
        /// <summary>
        /// Get the standard deviation for a list of longs
        /// </summary>
        /// <param name="data">A list of longs</param>
        /// <returns>The standard deviation</returns>
        public static double StandardDeviation(IEnumerable<long> data)
        {
            double squaredDifferenceSum = 0;
            double average = Average(data);
            foreach (var value in data)
            {
                squaredDifferenceSum += (value - average) * (value - average);
            }

            return System.Math.Sqrt(squaredDifferenceSum / (data.Count() - 1));
        }
        /// <summary>
        /// Get the standard deviation for a list of floats
        /// </summary>
        /// <param name="data">A list of floats</param>
        /// <returns>The standard deviation</returns>
        public static double StandardDeviation(IEnumerable<float> data)
        {
            double squaredDifferenceSum = 0;
            double average = Average(data);
            foreach (var value in data)
            {
                squaredDifferenceSum += (value - average) * (value - average);
            }

            return System.Math.Sqrt(squaredDifferenceSum / (data.Count() - 1));
        }
        /// <summary>
        /// Get the standard deviation for a list of decimals
        /// </summary>
        /// <param name="data">A list of decimals</param>
        /// <returns>The standard deviation</returns>
        public static decimal StandardDeviation(IEnumerable<decimal> data)
        {
            decimal squaredDifferenceSum = 0;
            decimal average = Average(data);
            foreach (var value in data)
            {
                squaredDifferenceSum += (value - average) * (value - average);
            }
            return Sqrt(squaredDifferenceSum / (data.Count() - 1));
        }
        /// <summary>
        /// Get the standard deviation for a list of doubles
        /// </summary>
        /// <param name="data">A list of doubles</param>
        /// <returns>The standard deviation</returns>
        public static double StandardDeviation(IEnumerable<double> data)
        {
            double squaredDifferenceSum = 0;
            double average = Average(data);
            foreach (var value in data)
            {
                squaredDifferenceSum += (value - average) * (value - average);
            }

            return System.Math.Sqrt(squaredDifferenceSum / (data.Count() - 1));
        }


        /// <summary>
        /// Returns the square root of a specified number
        /// </summary>
        /// <param name="d"></param>
        /// <returns>The square root of the specified number</returns>
        /// <throws><see cref="ArgumentException"/> if d is negative</throws>
        public static decimal Sqrt(decimal d)
        {
            return Sqrt(d);
        }
        /// <summary>
        /// Returns the square root of a specified number
        /// </summary>
        /// <param name="d"></param>
        /// <param name="guess">a guess for the square root</param>
        /// <returns>The square root of the specified number</returns>
        /// <throws><see cref="ArgumentException"/> if d is negative</throws>
        public static decimal Sqrt(decimal d, decimal? guess = null)
        {
            if (d < 0)
                throw new ArgumentException("Argument can not be negative");

            var ourGuess = guess.GetValueOrDefault(d / 2m);
            var result = d / ourGuess;
            var average = (ourGuess + result) / 2m;

            if (average == ourGuess) // This checks for the maximum precision possible with a decimal.
                return average;
            else
                return Sqrt(d, average);
        }
    }
}
