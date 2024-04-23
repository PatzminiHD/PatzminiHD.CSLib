﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace PatzminiHD.CSLib.Math.Extensions
{
    /// <summary>
    /// Extensions Methods for numbers
    /// </summary>
    public static class Number
    {
        /// <summary>
        /// Clamp a number between a min and a max value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to clamp</param>
        /// <param name="minValue">The min value to clamp to</param>
        /// <param name="maxValue">The max value to clamp to</param>
        /// <returns>The clamped value</returns>
        public static T Clamp<T>(this T value, T minValue, T maxValue) where T : INumber<T>
        {
            if (maxValue < minValue)
                throw new ArgumentException($"{nameof(maxValue)} can not be less then {nameof(minValue)}");

            if (value < minValue)
            {
                return minValue;
            }
            if (value > maxValue)
            {
                return maxValue;
            }
            return value;
        }

        /// <summary>
        /// Map a value from a range to a new range
        /// </summary>
        /// <param name="value">The value to map</param>
        /// <param name="currRangeMin">Min value of the current range</param>
        /// <param name="currRangeMax">Max value of the current range</param>
        /// <param name="toRangeMin">Min value of the output range</param>
        /// <param name="toRangeMax">Max value of the output range</param>
        /// <returns></returns>
        public static int Map(this int value, int currRangeMin, int currRangeMax, int toRangeMin, int toRangeMax)
        {
            //https://stackoverflow.com/questions/5731863/mapping-a-numeric-range-onto-another
            double slope = 1.0 * (toRangeMax - toRangeMin) / (double)(currRangeMax - currRangeMin);
            return (int)(toRangeMin + slope * (value - currRangeMin));
        }
        /// <summary>
        /// Map a value from a range to a new range
        /// </summary>
        /// <param name="value">The value to map</param>
        /// <param name="currRangeMin">Min value of the current range</param>
        /// <param name="currRangeMax">Max value of the current range</param>
        /// <param name="toRangeMin">Min value of the output range</param>
        /// <param name="toRangeMax">Max value of the output range</param>
        /// <returns></returns>
        public static double Map(this double value, double currRangeMin, double currRangeMax, double toRangeMin, double toRangeMax)
        {
            //hint64tps://stackoverflow.com/questions/5731863/mapping-a-numeric-range-onto-another
            return toRangeMin + ((toRangeMax - toRangeMin) / (currRangeMax - currRangeMin)) * (value - currRangeMin);
        }
    }
}
