using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Input.Console
{
    /// <summary>
    /// Contains Classes for displaying Yes or No Questions, ending in [Y/N]
    /// </summary>
    public class YesNo
    {
        /// <summary>
        /// Show a message, followed by [Y/N]<br/>Returns the users response
        /// </summary>
        /// <param name="message">The question you want to ask</param>
        /// <returns>True if the answer was yes, otherwise false</returns>
        public static bool Show(string message)
        {
            string? response = "";
            while (true)
            {
                System.Console.Write(message + " [Y/N] ");

                response = System.Console.ReadLine();

                if(response == null)
                    continue;

                if (response.ToLower() == "n")
                    return false;

                if (response.ToLower() == "y")
                    return true;
            }
        }
        /// <summary>
        /// Show a message, followed by [Y/n] or [y/N] depending on default response<br/>Returns the users response
        /// </summary>
        /// <param name="message">The question you want to ask</param>
        /// <param name="defaultResponse">The deafult response to the question</param>
        /// <returns>True if the answer was yes, otherwise false</returns>
        public static bool Show(string message, bool defaultResponse)
        {
            string? response = "";
            while(true)
            {
                if (defaultResponse)
                    System.Console.Write(message + " [Y/n] ");
                else
                    System.Console.Write(message + " [y/N] ");

                response = System.Console.ReadLine();

                if (response == null || response.Length == 0)
                    return defaultResponse;

                if (response.ToLower() == "n")
                    return false;

                if (response.ToLower() == "y")
                    return true;
            }
        }
    }
}
