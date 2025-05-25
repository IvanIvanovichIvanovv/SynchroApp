using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchroApp.Classes
{
    /// <summary>
    /// Class used to log messages to console and log file.
    /// </summary>
    public static class Logger
    {
        #region Public Properties

        /// <summary>
        /// Path to a log file.
        /// </summary>
        public static string logFile { get; private set; }

        #endregion Public Properties

        #region Private Methods

        /// <summary>
        /// Log a message to a specified log file.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="filePath">Path to log file.</param>
        private static void LogToFile(string message, string filePath)
        {
            filePath = CreateLogFileIfNotPresent(filePath);
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(message);
            }
        }

        /// <summary>
        /// Creates a log file if one does not exist.
        /// </summary>
        /// <param name="filePath">Log file path.</param>
        /// <returns></returns>
        private static string CreateLogFileIfNotPresent(string filePath)
        {
            if (!File.Exists(filePath))
            {
                try
                {
                    File.Create(filePath).Close();
                }
                catch (DirectoryNotFoundException)
                {
                    if (!Path.Exists(filePath))
                    {
                        Console.WriteLine($"[ERROR] Cannot create log file. Path does not exist.");
                        System.Environment.Exit(2);
                    }
                    filePath += "log.txt";
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Close();
                        Console.WriteLine($"[INFO] Creating log file: {filePath}");
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"[ERROR] Access denied: {filePath}");
                    System.Environment.Exit(4);
                }
            }
            return filePath;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Log a message about file being removed.
        /// </summary>
        /// <param name="filePath">Removed file path.</param>
        public static void LogFileRemoved(string filePath)
        {
            string message = $"[FILE][REMOVE] File removed: {filePath}";

            LogMessage(message);
        }

        /// <summary>
        /// Log a message about file being added.
        /// </summary>
        /// <param name="filePath">Path to a added file.</param>
        public static void LogFileAdded(string filePath)
        {
            string message = $"[FILE][ADD] New file added: {filePath}";
            LogMessage(message);
        }

        /// <summary>
        /// Log a message about directory being created.
        /// </summary>
        /// <param name="dirPath">Path to created directory.</param>
        public static void LogDirectoryAdded(string dirPath)
        {
            string message = $"[DIRECTORY][ADD] New directory added: {dirPath}";
            LogMessage(message);
        }

        /// <summary>
        /// Log a message about directory being removed.
        /// </summary>
        /// <param name="dirPath">Path to removed directory.</param>
        public static void LogDirectoryRemoved(string dirPath)
        {
            string message = $"[DIRECTORY][REMOVE] Directory removed: {dirPath}";
            LogMessage(message);
        }

        /// <summary>
        /// Log a message to a specified log file and a console.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="filepath">Path to a log file.</param>
        public static void LogMessage(string message, string filepath)
        {
            LogToFile(message, filepath);
            Console.WriteLine(message);
        }

        /// <summary>
        /// Log a message to log file and a console.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void LogMessage(string message)
        {
            LogMessage(message, logFile);
        }

        /// <summary>
        /// Log user selected data about file paths and synchronization time.
        /// </summary>
        /// <param name="sourceData">Selected source data folder.</param>
        /// <param name="replicaData">Selected replica data folder.</param>
        /// <param name="synchroPeriod">Selected synchronization time.</param>
        /// <param name="logFilePath">Selected log file.</param>
        public static void LogUserData(FilesData sourceData, FilesData replicaData, TimeSpan synchroPeriod, string logFilePath)
        {
            LogMessage($"Your Source Folder path: {sourceData.SourceFolder}");
            LogMessage($"Your Replica Folder path: {replicaData.SourceFolder}", logFilePath);
            LogMessage($"Your Log File path: {logFilePath}", logFilePath);
            LogMessage($"Your synchronization Time span: {synchroPeriod}", logFilePath);
        }

        /// <summary>
        /// Log user selected data about file paths and synchronization time.
        /// </summary>
        /// <param name="sourceData">Selected source data folder.</param>
        /// <param name="replicaData">Selected replica data folder.</param>
        /// <param name="synchroPeriod">Selected synchronization time.</param>
        public static void LogUserData(FilesData sourceData, FilesData replicaData, TimeSpan synchroPeriod)
        {
            LogMessage($"Your Source Folder path: {sourceData.SourceFolder}");
            LogMessage($"Your Replica Folder path: {replicaData.SourceFolder}");
            LogMessage($"Your Log File path: {logFile}");
            LogMessage($"Your synchronization Time span: {synchroPeriod}");
        }

        public static void SetLogFile(string logFilePath)
        {
            logFile = CreateLogFileIfNotPresent(logFilePath);
        }

        #endregion Public Methods
    }
}