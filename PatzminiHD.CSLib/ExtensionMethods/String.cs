using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.ExtensionMethods
{
    /// <summary>
    /// Contains extension Methods for the String type
    /// </summary>
    public static class String
    {
        /// <summary>
        /// Count the number of time a character existing in a string
        /// </summary>
        /// <param name="a">The string to search</param>
        /// <param name="charToSearch">The character to search for</param>
        /// <returns>How often the character exists in the string</returns>
        public static int Count(this string a, char charToSearch)
        {
            int counter = 0;

            foreach (char c in a)
            {
                if (c == charToSearch)
                    counter++;
            }

            return counter;
        }
    }
}
