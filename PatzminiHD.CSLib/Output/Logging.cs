using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PatzminiHD.CSLib.Output
{
    /// <summary>
    /// Contains Methods for logging purposes
    /// </summary>
    public static class Logging
    {
        private static string? _logFilePath;
        /// <summary> Log entries of this and higher levels are logged to the logfile<br/>
        ///           A value of null means that nothing is logged to the logfile</summary>
        public static LoggingLevel? FileLogLevel { get; set; } = null;
        /// <summary> Log entries of this and higher levels are logged to the console<br/>
        ///           A value of null means that nothing is logged to the console</summary>
        public static LoggingLevel? ConsoleLogLevel { get; set; } = null;
        /// <summary> The path of the logfile.
        ///           If set to null, nothing will be logged, regardless of <see cref="FileLogLevel"/> </summary>
        public static string? LogFilePath
        {
            get => _logFilePath;
            set
            {
                try
                {
                    if (value != null && !File.Exists(value))
                        File.Create(value);

                    _logFilePath = value;
                }
                catch(Exception e)
                {
                    _logFilePath = null;
                    throw new Exception("Logfile could not be created. See inner exception for details", e);
                }
            }
        }

        /// <summary> Specifies the logging level </summary>
        public enum LoggingLevel
        {
            /// <summary> Logging data that only serves as information </summary>
            Info = 0,
            /// <summary> Logging data that contains a warning, but no errors </summary>
            Warning = 1,
            /// <summary> Logging data that contains errors </summary>
            ERROR = 2,
        }

        public static void Log(string message, LoggingLevel logLevel)
        {
            var callerMethodName = (new System.Diagnostics.StackTrace()).GetFrame(1)?.GetMethod()?.Name;
            var dateTimeString = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm:ss");

            string logMessage = $"{dateTimeString} [{Enum.GetName(typeof(LoggingLevel), logLevel)}] ({callerMethodName}) {message}";


        }

        private static void LogToFile(string message, LoggingLevel loggingLevel)
        {
            if(LogFilePath == null || FileLogLevel == null || loggingLevel < FileLogLevel)
                return;

            File.AppendAllText(LogFilePath, message + "\n");
        }
        private static void LogToConsole(string message, LoggingLevel loggingLevel)
        {
            if (ConsoleLogLevel == null || loggingLevel < ConsoleLogLevel)
                return;

            switch (loggingLevel)
            {
                case LoggingLevel.Info:
                    System.Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case LoggingLevel.Warning:
                    System.Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LoggingLevel.ERROR:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            System.Console.WriteLine(message);
            System.Console.ResetColor();
        }
    }
}
