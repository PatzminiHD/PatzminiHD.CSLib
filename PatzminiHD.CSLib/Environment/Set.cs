using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Environment
{
    /// <summary>
    /// Set Info that is used by the Library
    /// </summary>
    public static class Set
    {
        private static bool forceConsoleColor = false;
        /// <summary> Constrain the ConsoleColors to be of type <see cref="ConsoleColor"/> </summary>
        public static bool ForceConsoleColor
        {
            get { return forceConsoleColor; }
            set
            {
                forceConsoleColor = value;
                if(value == true)
                    Output.Console.Color.ResetColor();
            }
        }


        /// <summary> The Default Console Foreground Color, only used when <see cref="ForceConsoleColor"/> is true </summary>
        public static ConsoleColor DefaultConsoleForegroundColor = ConsoleColor.Green;
        /// <summary> The Default Console Background Color, only used when <see cref="ForceConsoleColor"/> is true </summary>
        public static ConsoleColor DefaultConsoleBackgroundColor = ConsoleColor.Black;
    }
}
