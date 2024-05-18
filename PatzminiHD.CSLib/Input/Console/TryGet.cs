using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Input.Console
{
    /// <summary>
    /// Contains Methods for trying to get inputs of a specific type
    /// </summary>
    public class TryGet
    {
        /// <summary>
        /// Get an unsigned integer from the user
        /// </summary>
        /// <param name="value">The value the user entered. 0 if the user cancelled the input</param>
        /// <param name="message">The message to display to the user</param>
        /// <param name="emptyToCancel">True if the user can enter nothing to cancel the input</param>
        /// <returns>True if the input was valid<br/>False if the input was cancelled</returns>
        public static bool UInt(out uint value, string message, bool emptyToCancel = true)
        {
            value = 0;
            System.Console.Write(message);
            var userInput = System.Console.ReadLine();
            while (!uint.TryParse(userInput, out value))
            {
                if ((userInput == null || userInput == "") && emptyToCancel)
                    break;
                System.Console.Write("Invalid input. " + message);
                userInput = System.Console.ReadLine();
            }

            return userInput == null || userInput == "" ? false : true;
        }
        /// <summary>
        /// Get an integer from the user
        /// </summary>
        /// <param name="value">The value the user entered. 0 if the user cancelled the input</param>
        /// <param name="message">The message to display to the user</param>
        /// <param name="emptyToCancel">True if the user can enter nothing to cancel the input</param>
        /// <returns>True if the input was valid<br/>False if the input was cancelled</returns>
        public static bool Int(out int value, string message, bool emptyToCancel = true)
        {
            value = 0;
            System.Console.Write(message);
            var userInput = System.Console.ReadLine();
            while (!int.TryParse(userInput, out value))
            {
                if ((userInput == null || userInput == "") && emptyToCancel)
                    break;
                System.Console.Write("Invalid input. " + message);
                userInput = System.Console.ReadLine();
            }

            return userInput == null || userInput == "" ? false : true;
        }

        /// <summary>
        /// Get an double from the user
        /// </summary>
        /// <param name="value">The value the user entered. 0 if the user cancelled the input</param>
        /// <param name="message">The message to display to the user</param>
        /// <param name="emptyToCancel">True if the user can enter nothing to cancel the input</param>
        /// <returns>True if the input was valid<br/>False if the input was cancelled</returns>
        public static bool Double(out double value, string message, bool emptyToCancel = true)
        {
            value = 0;
            System.Console.Write(message);
            var userInput = System.Console.ReadLine();
            while (!double.TryParse(userInput, out value))
            {
                if ((userInput == null || userInput == "") && emptyToCancel)
                    break;
                System.Console.Write("Invalid input. " + message);
                userInput = System.Console.ReadLine();
            }

            return userInput == null || userInput == "" ? false : true;
        }
    }
}
