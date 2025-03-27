using System.ComponentModel.Design;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Formats.Tar;

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
                    if(value == null)
                    {
                        _logFilePath = null;
                        return;
                    }
                    var directoryName = Path.GetDirectoryName(value);
                    var fileName = DateTime.UtcNow.ToString("yyMMdd-HHmmss") + '_' + Path.GetFileName(value);

                    if (directoryName != null && !Directory.Exists(directoryName))
                        Directory.CreateDirectory(directoryName);

                    if (Path.GetFileName(value) == null)
                    {
                        throw new ArgumentException("The path must contain a filename", nameof(value));
                    }
                    string fullFileName;

                    if (directoryName != null)
                        fullFileName = Path.Combine(directoryName, fileName);
                    else
                        fullFileName = fileName;

                    _logFilePath = fullFileName;
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
            /// <summary> Logging data that is very verbose </summary>
            Verbose = 0,
            /// <summary> Logging data that only serves as information </summary>
            Info = 1,
            /// <summary> Logging data that contains a warning, but no errors </summary>
            Warning = 2,
            /// <summary> Logging data that contains errors </summary>
            ERROR = 3,
            /// <summary> Logging data that should always be logged </summary>
            Mandatory = 99,
        }

        /// <summary>
        /// Initialise the logging.<br/>
        /// It is recommended that you also call the CullLogs Method<br/>
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
            CompressPreviousLogs();
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
        /// Compress Previous logs using gzip
        /// </summary>
        private static void CompressPreviousLogs()
        {
            if (LogFilePath == null)
                return;

            var logFiles = GetExistingLogFiles();
            if (logFiles.Count == 0)
                return;

            List<string> compressedFiles = new();

            foreach (var logFile in logFiles)
            {
                try
                {
                    if (logFile.EndsWith(".gz"))
                        continue;

                    using FileStream logFileStream = File.Open(logFile, FileMode.Open);
                    using FileStream compressedLogFileStream = File.Create(logFile + ".gz");
                    using var compressor = new GZipStream(compressedLogFileStream, CompressionMode.Compress);
                    logFileStream.CopyTo(compressor);

                    compressedFiles.Add(logFile);
                }
                catch
                {
                    continue;
                }
            }

            foreach (var logFile in compressedFiles)
            {
                File.Delete(logFile);
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
        /// Log a message with <see cref="LoggingLevel.Verbose"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        public static void LogVerbose(string message, string subsystem = "", [CallerMemberName] string callerName = "")
        {
            LogInternal(message, LoggingLevel.Verbose, subsystem, callerName);
        }

        /// <summary>
        /// Log a message with <see cref="LoggingLevel.Info"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        /// <param name="callerName">Optional: The name of the calling function. Should be filled in automatically</param>
        public static void LogInfo(string message, string subsystem = "", [CallerMemberName] string callerName = "")
        {
            LogInternal(message, LoggingLevel.Info, subsystem, callerName);
        }

        /// <summary>
        /// Log a message with <see cref="LoggingLevel.Warning"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        /// <param name="callerName">Optional: The name of the calling function. Should be filled in automatically</param>
        public static void LogWarning(string message, string subsystem = "", [CallerMemberName] string callerName = "")
        {
            LogInternal(message, LoggingLevel.Warning, subsystem, callerName);
        }

        /// <summary>
        /// Log a message with <see cref="LoggingLevel.ERROR"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        /// <param name="callerName">Optional: The name of the calling function. Should be filled in automatically</param>
        public static void LogError(string message, string subsystem = "", [CallerMemberName] string callerName = "")
        {
            LogInternal(message, LoggingLevel.ERROR, subsystem, callerName);
        }

        /// <summary>
        /// Log a message with <see cref="LoggingLevel.Mandatory"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        /// <param name="callerName">Optional: The name of the calling function. Should be filled in automatically</param>
        public static void LogMandatory(string message, string subsystem = "", [CallerMemberName] string callerName = "")
        {
            LogInternal(message, LoggingLevel.Mandatory, subsystem, callerName);
        }

        /// <summary>
        /// Add a message to the log, with a specified loglevel
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="logLevel">The loglevel</param>
        /// <param name="subsystem">Optional: The name of a subsystem where the log originated from,
        ///                         for easier tracing</param>
        /// <param name="callerName">Optional: The name of the calling function. Should be filled in automatically</param>
        public static void Log(string message, LoggingLevel logLevel, string subsystem = "", [CallerMemberName] string callerName = "")
        {
            LogInternal(message, logLevel, subsystem, callerName);
        }

        private static void LogInternal(string message, LoggingLevel logLevel, string subsystem, string callerName)
        {
            //Improve performence when no logging is needed
            if (ConsoleLogLevel == null && FileLogLevel == null)
                return;

            var dateTimeString = DateTime.UtcNow.ToString("yyyy.MM.dd HH:mm:ss");

            string logMessage = $"{dateTimeString} [{Enum.GetName(typeof(LoggingLevel), logLevel)}] ({(subsystem != "" ? $"{subsystem}:" : "")}{callerName}) {message}";

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
                case LoggingLevel.Verbose:
                case LoggingLevel.Mandatory:
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

        /// <summary>
        /// Get a list of all existing log files, including the current logfile and all compressed logfiles
        /// </summary>
        /// <returns></returns>
        public static List<string> GetExistingLogFiles()
        {
            if(LogFilePath == null)
                return new();

            var logDir = Path.GetDirectoryName(LogFilePath);

            //Match strings like "250211-102415_" where each digit can be exchanged for any other digit
            Regex regex = new("^[0-9]{6}-[0-9]{6}_");

            if (logDir == null)
                return new();

            var logFiles = Directory.GetFiles(logDir).Where(f => regex.IsMatch(Path.GetFileName(f))).ToList();
            logFiles.Sort();
            return logFiles;
        }
        /// <summary>
        /// Compress all previous logfiles to a tar.gz archive
        /// </summary>
        /// <param name="tarFileName">The name of the tar file<br/>(Date and time of the newest )</param>
        /// <returns>The name of the created tar file</returns>
        public static string? CompressToTarGz(string tarFileName)
        {
            if (LogFilePath == null)
                return null;
            var logFiles = GetExistingLogFiles().Where(f => !f.Contains(".tar")).ToList();
            logFiles.Sort();
            if (logFiles.Count == 0)
                return null;

            var logDir = Path.GetDirectoryName(LogFilePath);
            if(logDir == null)
                return null;

            //Create a temporary directory to store the logfiles
            var tmpDir = Path.Combine(logDir, "tmp");
            Directory.CreateDirectory(tmpDir);

            //Copy the logfiles to the temporary directory
            foreach (var logFile in logFiles)
            {
                //Preseve the current logfile
                if(logFile != LogFilePath)
                    File.Move(logFile, Path.Combine(tmpDir, Path.GetFileName(logFile)));
                else
                    File.Copy(logFile, Path.Combine(tmpDir, Path.GetFileName(logFile)));
            }

            //Create the tar.gz file
            var tarFilePath = Path.Combine(logDir, Path.GetFileName(logFiles[0]).Substring(0, 13) + "_" + Path.GetFileName(LogFilePath).Substring(0, 13)) + $"_{tarFileName}.tar";
            
            TarFile.CreateFromDirectory(tmpDir, tarFilePath, false);

            using FileStream tarFileStream = File.Open(tarFilePath, FileMode.Open);
            using FileStream compressedTarFileStream = File.Create(tarFilePath + ".gz");
            using var compressor = new GZipStream(compressedTarFileStream, CompressionMode.Compress);
            tarFileStream.CopyTo(compressor);

            var compressedTarFilePath = compressedTarFileStream.Name;

            tarFileStream.Dispose();

            Directory.Delete(tmpDir, true);
            File.Delete(tarFilePath);

            return compressedTarFilePath;
        }
    }
}
