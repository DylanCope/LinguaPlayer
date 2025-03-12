using System;
using System.IO;

namespace VideoPlayer.Tests
{
    public class TestLogger
    {
        private readonly string _logFile;
        private static bool _isFirstTest = true;

        public TestLogger(Type testClass, string logFileName)
        {
            if (testClass == null) throw new ArgumentNullException(nameof(testClass));
            if (string.IsNullOrEmpty(logFileName)) throw new ArgumentNullException(nameof(logFileName));

            var assemblyLocation = testClass.Assembly.Location;
            var testDirectory = Path.GetDirectoryName(assemblyLocation) 
                ?? throw new InvalidOperationException("Could not determine test directory");
            var testLogsDirectory = Path.Combine(testDirectory, "test-logs");
            _logFile = Path.Combine(testLogsDirectory, logFileName);
        }

        public void WriteLog(string message)
        {
            if (string.IsNullOrEmpty(_logFile)) throw new InvalidOperationException("Log file path not initialized");
            
            var directory = Path.GetDirectoryName(_logFile) 
                ?? throw new InvalidOperationException("Could not determine log directory");
            
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.AppendAllText(_logFile, message + Environment.NewLine);
            Console.WriteLine(message);
        }

        public void InitTestLog(string testName)
        {
            if (string.IsNullOrEmpty(testName)) throw new ArgumentNullException(nameof(testName));
            
            if (_isFirstTest)
            {
                var directory = Path.GetDirectoryName(_logFile) 
                    ?? throw new InvalidOperationException("Could not determine log directory");
                
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(_logFile, $"=== Test Run Started ===\n\n");
                _isFirstTest = false;
            }
            WriteLog($"\n=== Starting {testName} ===\n");
        }
    }
} 