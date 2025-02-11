using System.Text.RegularExpressions;

namespace PatzminiHD.CSLib.Output
{
    /// <summary>
    /// Contains Methods for logging purposes<br/>
    /// Defaults: Log everything to the console, nothing to a file
    /// </summary>
    public static class Logging
    {
        private static string? _logFilePath = null;
        /// <summary> Log entries of this and higher levels are logged to the logfile<br/>
        ///           A value of null means that nothing is logged to the logfile</summary>
        public static LoggingLevel? FileLogLevel { get; set; } = null;
        /// <summary> Log entries of this and higher levels are logged to the console<br/>
        ///           A value of null means that nothing is logged to the console</summary>
        public static LoggingLevel? ConsoleLogLevel { get; set; } = LoggingLevel.Info;
        /// <summary> The path of the logfile.
        ///           If set to null, nothing will be logged, regardless of <see cref="FileLogLevel"/> </summary>
        public static string? LogFilePath
        {
            get => _logFilePath;
            set
            {
                try
                {
                    var fileName = DateTime.UtcNow.ToString("yyMMdd-HHmmss") + '_' + value;
                    if (value != null && !File.Exists(value))
                        File.Create(value);

                    _logFilePath = fileName;
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

        /// <summary>
        /// Initialise the logging.<br/>
        /// It is recommended that you also call the CullLogs Methods<br/>
        /// (<see cref="CullLogs(int)"/> or <see cref="CullLogs(DateTime)"/>)
        /// </summary>
        /// <param name="consoleLogLevel">The lowest level of log that will be logged to the console</param>
        /// <param name="fileLogLevel">The lowest level of log that will be logged to the log file<br/>
        ///                            (Is only logged when <see cref="LogFilePath"/> is also valid)</param>
        /// <param name="logFilePath">The path of the log file<br/>
        ///                           (Logs are only written to the file if <see cref="FileLogLevel"/> is also set)</param>
        public static void Init(LoggingLevel? consoleLogLevel, LoggingLevel? fileLogLevel, string? logFilePath)
        {
            ConsoleLogLevel = consoleLogLevel;
            FileLogLevel = fileLogLevel;
            LogFilePath = logFilePath;
        }


        /// <summary>
        /// Cull all logs that are older than <paramref name="oldestLogToKeep"/>
        /// </summary>
        /// <param name="oldestLogToKeep">The oldest log to keep</param>
        public static void CullLogs(DateTime oldestLogToKeep)
        {
            if (LogFilePath == null)
                return;

            foreach (var logFile in GetExistingLogFiles())
            {
                try
                {
                    var fileTime = DateTime.ParseExact(Path.GetFileName(logFile).Substring(0, 13), "yyMMdd-HHmmss", null);
                    if (fileTime < oldestLogToKeep)
                        File.Delete(logFile);
                }
                catch
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Cull all logs, keeping only the latest <paramref name="numOfLogsToKeep"/>
        /// </summary>
        /// <param name="numOfLogsToKeep">How many logs to keep</param>
        public static void CullLogs(int numOfLogsToKeep)
        {
            foreach (var logFile in GetExistingLogFiles().SkipLast(numOfLogsToKeep))
            {
                File.Delete(logFile);
            }
        }


        /// <summary>
        /// Log a message with <see cref="LoggingLevel.Info"/>
        /// </summary>
        /// <param name="message">The message to log</param
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        public static void LogInfo(string message, string subsystem = "")
        {
            Log(message, LoggingLevel.Info, subsystem);
        }

        /// <summary>
        /// Log a message with <see cref="LoggingLevel.Warning"/>
        /// </summary>
        /// <param name="message">The message to log</param
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        public static void LogWarning(string message, string subsystem = "")
        {
            Log(message, LoggingLevel.Warning, subsystem);
        }

        /// <summary>
        /// Log a message with <see cref="LoggingLevel.ERROR"/>
        /// </summary>
        /// <param name="message">The message to log</param
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        public static void LogError(string message, string subsystem = "")
        {
            Log(message, LoggingLevel.ERROR, subsystem);
        }

        /// <summary>
        /// Add a message to the log, with a specified loglevel
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="logLevel">The loglevel</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        public static void Log(string message, LoggingLevel logLevel, string subsystem = "")
        {
            //Improve performence when no logging is needed
            if (ConsoleLogLevel == null && FileLogLevel == null)
                return;

            var callerMethodName = (new System.Diagnostics.StackTrace()).GetFrame(1)?.GetMethod()?.Name;
            var dateTimeString = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm:ss");

            string logMessage = $"{dateTimeString} [{Enum.GetName(typeof(LoggingLevel), logLevel)}] ({subsystem}:{callerMethodName}) {message}";

            LogToConsole(logMessage, logLevel);
            LogToFile(logMessage, logLevel);
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

        private static List<string> GetExistingLogFiles()
        {
            if(LogFilePath == null)
                return new();

            var logDir = new DirectoryInfo(LogFilePath).FullName;

            //Match strings like "250211-102415" where each digit can be exchanged for any other digit
            Regex regex = new("^[0-9]{6}-[0-9]{6}_");

            var logFiles = Directory.GetFiles(logDir).Where(f => regex.IsMatch(Path.GetFileName(f))).ToList();
            logFiles.Sort();
            return logFiles;
        }
    }
}
