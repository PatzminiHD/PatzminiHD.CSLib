using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

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

        /// <summary>
        /// Return a new string, where "{0}" is replaced with <paramref name="arg"/>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string Interpolate(this string s, string arg)
        {
            return s.Replace("{0}", arg);
        }
        /// <summary>
        /// Return a new string, where "{0}" is replaced with <paramref name="arg"/>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static string Interpolate<T>(this string s, T arg) where T : INumber<T>
        {
            return s.Replace("{0}", arg.ToString("0.#####", CultureInfo.InvariantCulture));
        }
        /// <summary>
        /// Return a new string, where "{0}", "{1}" and so on are replaced with the contents of <paramref name="args"/>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Interpolate(this string s, params string[] args)
        {
            for(int i = 0; i < args.Length; i++)
            {
                s = s.Replace($"{{{i}}}", args[i]);
            }
            return s;
        }
        /// <summary>
        /// Return a new string, where "{0}", "{1}" and so on are replaced with the contents of <paramref name="args"/>
        /// </summary>
        /// <param name="s"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Interpolate<T>(this string s, params T[] args) where T : INumber<T>
        {
            for(int i = 0; i < args.Length; i++)
            {
                s = s.Replace($"{{{i}}}", args[i].ToString("0.#####", CultureInfo.InvariantCulture));
            }
            return s;
        }
    }
}
