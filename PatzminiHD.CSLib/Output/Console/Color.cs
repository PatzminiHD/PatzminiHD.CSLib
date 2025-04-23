using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Output.Console
{
    /// <summary>
    /// Contains Methods for manipulating Console Colors
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Swap Foreground and Background Color
        /// </summary>
        public static void SwapForegroundBackgroundColor()
        {
            var tmp = System.Console.ForegroundColor;
            System.Console.ForegroundColor = System.Console.BackgroundColor;
            System.Console.BackgroundColor = tmp;
        }
        /// <summary>
        /// Reset the Color of the Console with respect to <see cref="Environment.Set.ForceConsoleColor"/>
        /// </summary>
        public static void ResetColor()
        {
            if(Environment.Set.ForceConsoleColor)
            {
                System.Console.ForegroundColor = Environment.Set.DefaultConsoleForegroundColor;
                System.Console.BackgroundColor = Environment.Set.DefaultConsoleBackgroundColor;
            }
            else
                System.Console.ResetColor();
        }


        /// <summary>
        /// Write a colored line (Writes the text with a \n at the end)
        /// </summary>
        /// <param name="text">The text of the line</param>
        /// <param name="color">The color the text should have</param>
        public static void WriteColoredLine(string text, ConsoleColor color)
        {
            WriteColored(text + "\n", color);
        }
        /// <summary>
        /// Write colored text
        /// </summary>
        /// <param name="text">The text to write</param>
        /// <param name="color">The color the text should have</param>
        public static void WriteColored(string text, ConsoleColor color)
        {
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine(text);
            System.Console.ForegroundColor = currentColor;
        }
    }
}
