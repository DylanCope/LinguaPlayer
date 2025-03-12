using System;
using System.Linq;
using WanaKanaNet;
using WanaKanaNet.Helpers;
using Xunit;
using Xunit.Abstractions;
using System.Diagnostics;
using System.IO;

namespace VideoPlayer.Tests
{
    public class WanaKanaTest
    {
        private readonly string logFile;
        private static bool isFirstTest = true;

        public WanaKanaTest()
        {
            // Get the directory where the test assembly is located
            var assemblyLocation = typeof(WanaKanaTest).Assembly.Location;
            var testDirectory = Path.GetDirectoryName(assemblyLocation);
            logFile = Path.Combine(testDirectory, "test-logs.txt");
        }

        private void WriteLog(string message)
        {
            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(logFile);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.AppendAllText(logFile, message + Environment.NewLine);
            // Also write to console for immediate feedback
            Console.WriteLine(message);
        }

        private void InitTestLog(string testName)
        {
            if (isFirstTest)
            {
                File.WriteAllText(logFile, $"=== Test Run Started ===\n\n");
                isFirstTest = false;
            }
            WriteLog($"\n=== Starting {testName} ===\n");
        }

        [Fact]
        public void TestTokenize()
        {
            InitTestLog("TestTokenize");
            var text = "私は日本語を勉強します";
            var tokens = WanaKana.Tokenize(text);
            WriteLog($"Input text: {text}");
            WriteLog($"Raw tokens: {string.Join("|", tokens.Select(t => $"'{t}'"))}");
            WriteLog($"Token count: {tokens.Length}");

            foreach (var token in tokens)
            {
                WriteLog($"Token type: {token.GetType().FullName}");
                WriteLog($"Token value: {token}");
                WriteLog($"Token properties:");
                foreach (var prop in token.GetType().GetProperties())
                {
                    WriteLog($"  {prop.Name} = {prop.GetValue(token)}");
                }
            }
            WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void TestSimpleHiragana()
        {
            InitTestLog("TestSimpleHiragana");
            var text = "ひらがな";
            var tokens = WanaKana.Tokenize(text);
            WriteLog($"Input text: {text}");
            WriteLog($"Raw tokens: {string.Join("|", tokens.Select(t => $"'{t}'"))}");
            WriteLog($"Token count: {tokens.Length}");

            foreach (var token in tokens)
            {
                WriteLog($"Token type: {token.GetType().FullName}");
                WriteLog($"Token value: {token}");
                WriteLog($"Token properties:");
                foreach (var prop in token.GetType().GetProperties())
                {
                    WriteLog($"  {prop.Name} = {prop.GetValue(token)}");
                }
            }
            WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void TestSimpleKanji()
        {
            InitTestLog("TestSimpleKanji");
            var text = "漢字";
            var tokens = WanaKana.Tokenize(text);
            WriteLog($"Input text: {text}");
            WriteLog($"Raw tokens: {string.Join("|", tokens.Select(t => $"'{t}'"))}");
            WriteLog($"Token count: {tokens.Length}");

            foreach (var token in tokens)
            {
                WriteLog($"Token type: {token.GetType().FullName}");
                WriteLog($"Token value: {token}");
                WriteLog($"Token properties:");
                foreach (var prop in token.GetType().GetProperties())
                {
                    WriteLog($"  {prop.Name} = {prop.GetValue(token)}");
                }
            }
            WriteLog("\n=== Test Complete ===\n");
        }
    }
} 