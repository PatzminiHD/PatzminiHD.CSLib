﻿using System;
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
    }
}
