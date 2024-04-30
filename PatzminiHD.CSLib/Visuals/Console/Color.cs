using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Visuals.Console
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
    }
}
