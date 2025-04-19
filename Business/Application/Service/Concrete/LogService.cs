using Application.Enum;
using Application.Service.Abstract;
using System.Diagnostics;
using System.IO.Compression;

namespace Application.Service.Concrete
{
    public class LogService : ILogService
    {
        #region Properties
        private string _logPath;                                        // Log path
        private static readonly SemaphoreSlim _semaphore = new(1, 1);   // Lock object for thread safety
        #endregion
        #region Constructors
        public LogService()
        {
            _logPath = EnsureLogPathExists();
            EnsureLogFileExists("Error");
            EnsureLogFileExists("Information");
        }
        #endregion
        #region Methods
        public async Task WriteLog(LogLevel level, string message)
        {
            // Ensure thread safety
            await _semaphore.WaitAsync();
            try
            {
                // Prepare log environment
                PrepareLogEnvironment(level.ToString());

                // Determine log entry format based on log level
                var logEntry = $"{DateTime.Now:HH:mm:ss} [{level}] - {message}";
                var path = GetLogFilePath(level);

                // Write log entry to file
                await File.AppendAllTextAsync(path, logEntry + Environment.NewLine);

                // Logging according to level
                if (level == LogLevel.Debug || level == LogLevel.Trace)
                {
                    Debug.WriteLine(logEntry);
                }
                if (level == LogLevel.Warning || level == LogLevel.Error || level == LogLevel.Critical)
                {
                    Trace.WriteLine(logEntry);
                }
            }
            finally
            {
                _semaphore.Release(); // Release the semaphore
            }
        }

        /// <summary>
        /// Prepares the environment to safely write logs:
        /// - Ensures the log directory exists
        /// - Ensures the log file exists
        /// - Rotates large or outdated log files to OldLogs
        /// - Archives OldLogs folder to zip if any files exist
        /// </summary>
        /// <param name="logType"></param>
        private void PrepareLogEnvironment(string logType)
        {
            EnsureLogPathExists();           // 1️⃣
            EnsureLogFileExists(logType);    // 2️⃣
            RotateLogFile(logType);          // 3️⃣
            ArchiveOldLogsToZip();           // 4️⃣
        }

        private void RotateLogFile(string logType)
        {
            // Check if log file is too large or old
            string fileName = GetLogFileName(logType);
            string logFilePath = GetLogFilePath(logType);
            string oldLogsPath = Path.Combine(_logPath, "OldLogs");

            // Max size of log file
            const long maxSize = 10 * 1024 * 1024; // 10 MB

            // Check if log file exists
            if (!File.Exists(logFilePath))
                return;

            // Check if log file is too large or old
            var fileInfo = new FileInfo(logFilePath);

            // Check if log file is too large or old
            bool isTooLarge = fileInfo.Length >= maxSize;
            bool isOld = fileInfo.LastWriteTime.Date < DateTime.Today;

            // If log file is too large or old, move it to old logs folder
            if (isTooLarge || isOld)
            {
                // Create old logs folder if not exists
                Directory.CreateDirectory(oldLogsPath);

                // Move log file to old logs folder
                string newFileName = Path.GetFileNameWithoutExtension(fileName) + $"-{DateTime.Now:HHmmss}" + ".txt";
                string destinationPath = Path.Combine(oldLogsPath, newFileName);

                // Move log file
                File.Move(logFilePath, destinationPath);
                using (File.Create(logFilePath)) { }
            }
        }

        private void ArchiveOldLogsToZip()
        {
            // Check if old logs folder exists
            string oldLogsPath = Path.Combine(_logPath, "OldLogs");
            string archivePath = Path.Combine(_logPath, "ArchivedLogs");
            Directory.CreateDirectory(archivePath);

            // Check if any old log exists
            var oldLogFiles = Directory.GetFiles(oldLogsPath, "*.txt");
            if (oldLogFiles.Length == 0)
                return;

            // Create zip file
            string zipFileName = $"ArchivedLogs-{DateTime.Today:dd.MM.yyyy}-{DateTime.Now:HHmmss}.zip";
            string zipFilePath = Path.Combine(archivePath, zipFileName);

            // Archive old logs
            using (var archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (var file in oldLogFiles)
                {
                    // Add file to archive
                    archive.CreateEntryFromFile(file, Path.GetFileName(file));

                    // Delete file
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// Generates log file depending on log type
        /// </summary>
        /// <param name="logType"></param>
        private void EnsureLogFileExists(string logType)
        {
            // Get path
            var path = GetLogFilePath(logType);

            // Create file if not exists
            if (!File.Exists(path))
            {
                // Create file
                File.Create(path).Dispose();
            }
        }

        /// <summary>
        /// Generates log folder and it's path
        /// </summary>
        /// <returns></returns>
        private string EnsureLogPathExists()
        {
            // Get path
            var path = Path.Combine(AppContext.BaseDirectory, "Log");

            // Create directory if not exists
            Directory.CreateDirectory(path);

            // Return path
            return path;
        }

        /// <summary>
        /// Generates log file name depending on log type
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        private string GetLogFileName(string logType) => $"{DateTime.Today:dd.MM.yyyy}-{logType}-Log.txt";

        /// <summary>
        /// Generates log file name depending on log type
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        private string GetLogFileName(LogLevel level) => $"{DateTime.Today:dd.MM.yyyy}-{level}-Log.txt";

        /// <summary>
        /// Generates log file path depending on log type
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        private string GetLogFilePath(string logType) => Path.Combine(_logPath, GetLogFileName(logType));

        /// <summary>
        /// Generates log file path depending on log type
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        private string GetLogFilePath(LogLevel level) => Path.Combine(_logPath, GetLogFileName(level));
        #endregion
    }
}