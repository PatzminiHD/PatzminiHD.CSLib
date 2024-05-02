using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Environment
{
    /// <summary>
    /// Get Info about the environment
    /// </summary>
    public class Get
    {
        /// <summary>
        /// Enum of Operating Systems
        /// </summary>
        public enum OperatingSystem
        {
            /// <summary> Linux Operating System </summary>
            Linux,
            /// <summary> Windows Operating System </summary>
            Windows,
            /// <summary> MacOS Operating System </summary>
            MacOS,
            /// <summary> FreeBSD Operating System </summary>
            FreeBSD,
            /// <summary> Android Operating System </summary>
            Android,
            /// <summary> BrowserOperating System </summary>
            Browser,
            /// <summary> IOS Operating System </summary>
            IOS,
            /// <summary> MacCatalyst Operating System </summary>
            MacCatalyst,
            /// <summary> TV Operating System </summary>
            TvOS,
            /// <summary> Wasi Operating System </summary>
            Wasi,
            /// <summary> WatchOS Operating System </summary>
            WatchOS,
            /// <summary> Other Operating System </summary>
            Other,
        }

        /// <summary>
        /// The Current Operating System
        /// </summary>
        /// <returns></returns>
        public static OperatingSystem OS {
            get
            {
                if (System.OperatingSystem.IsLinux())
                    return OperatingSystem.Linux;
                else if (System.OperatingSystem.IsWindows())
                    return OperatingSystem.Windows;
                else if (System.OperatingSystem.IsMacOS())
                    return OperatingSystem.MacOS;
                else if (System.OperatingSystem.IsFreeBSD())
                    return OperatingSystem.FreeBSD;
                else if (System.OperatingSystem.IsAndroid())
                    return OperatingSystem.Android;
                else if (System.OperatingSystem.IsBrowser())
                    return OperatingSystem.Browser;
                else if (System.OperatingSystem.IsIOS())
                    return OperatingSystem.IOS;
                else if (System.OperatingSystem.IsMacCatalyst())
                    return OperatingSystem.MacCatalyst;
                else if (System.OperatingSystem.IsTvOS())
                    return OperatingSystem.TvOS;
                else if (System.OperatingSystem.IsWasi())
                    return OperatingSystem.Wasi;
                else if (System.OperatingSystem.IsWatchOS())
                    return OperatingSystem.WatchOS;
                else
                    return OperatingSystem.Other;
            }
        }
        /// <summary>
        /// The Current Operating System as a string
        /// </summary>
        public static string OsString
        {
            get
            {
                switch(OS)
                {
                    case OperatingSystem.Linux:         return "Linux";
                    case OperatingSystem.Windows:       return "Windows";
                    case OperatingSystem.MacOS:         return "MacOS";
                    case OperatingSystem.FreeBSD:       return "FreeBSD";
                    case OperatingSystem.Android:       return "Android";
                    case OperatingSystem.Browser:       return "Browser";
                    case OperatingSystem.IOS:           return "IOS";
                    case OperatingSystem.MacCatalyst:   return "MacCatalyst";
                    case OperatingSystem.TvOS:          return "TvOS";
                    case OperatingSystem.Wasi:          return "Wasi";
                    case OperatingSystem.WatchOS:       return "WatchOS";
                    default: return "Other";
                }
            }
        }
        /// <summary>
        /// The Directory to put temporary files in
        /// </summary>
        public static string? TempDirectory
        {
            get {
                    return Path.GetTempPath();
            }
        }

        /// <summary>
        /// Get the default Console Color
        /// </summary>
        /// <returns>The default foreground and background Color</returns>
        [Pure]
        public static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) GetDefaultColor()
        {
            ConsoleColor tmpForeground = Console.ForegroundColor;
            ConsoleColor tmpBackground = Console.BackgroundColor;
            Console.ResetColor();
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) returnValue = (Console.ForegroundColor, Console.BackgroundColor);
            Console.ForegroundColor = tmpForeground;
            Console.BackgroundColor = tmpBackground;
            return returnValue;
        }

        /// <summary>
        /// Get the path to a directory in the temporary directory with the specified name
        /// </summary>
        /// <param name="ProgramName">The name of the subdirectory</param>
        /// <returns></returns>
        public static string? TempProgramDirectory(string ProgramName)
        {
            if (ProgramName == null) throw new ArgumentNullException(nameof(ProgramName) + " is null");
            if (ProgramName.Length == 0) throw new ArgumentException(nameof(ProgramName) + " cannot be empty");
            return TempDirectory + Path.DirectorySeparatorChar + ProgramName;
        }
    }
}
