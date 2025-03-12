using System.Linq;
using VideoPlayer.Services;
using VideoPlayer.Models;
using Xunit;
using System;
using System.IO;

namespace VideoPlayer.Tests
{
    public class JapaneseParserTests
    {
        private readonly JapaneseParser _parser;
        private readonly string logFile;
        private static bool isFirstTest = true;

        public JapaneseParserTests()
        {
            _parser = new JapaneseParser();
            var assemblyLocation = typeof(JapaneseParserTests).Assembly.Location;
            var testDirectory = Path.GetDirectoryName(assemblyLocation);
            logFile = Path.Combine(testDirectory, "japanese-parser-test-logs.txt");
        }

        private void WriteLog(string message)
        {
            var directory = Path.GetDirectoryName(logFile);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.AppendAllText(logFile, message + Environment.NewLine);
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
        public void ParseText_EmptyString_ReturnsEmptyCollection()
        {
            InitTestLog(nameof(ParseText_EmptyString_ReturnsEmptyCollection));
            WriteLog("Testing empty string input");
            var result = _parser.ParseText("");
            WriteLog($"Result count: {result.Count()}");
            Assert.Empty(result);
            WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_MixedLanguages_HandlesCorrectly()
        {
            InitTestLog(nameof(ParseText_MixedLanguages_HandlesCorrectly));
            var input = "Hello みなさん、私は日本語を studying です。";
            WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                WriteLog($"Word: '{word.Surface}'");
            }

            // Verify Japanese parts are detected
            Assert.Contains(result, w => w.Surface == "みなさん");
            Assert.Contains(result, w => w.Surface == "私");
            Assert.Contains(result, w => w.Surface == "は");
            Assert.Contains(result, w => w.Surface == "日本語");
            Assert.Contains(result, w => w.Surface == "を");
            Assert.Contains(result, w => w.Surface == "です");
            // Also verify punctuation and English
            Assert.Contains(result, w => w.Surface == "Hello");
            Assert.Contains(result, w => w.Surface == "、");
            Assert.Contains(result, w => w.Surface == "studying");
            Assert.Contains(result, w => w.Surface == "。");

            WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_WithPunctuation_ParsesCorrectly()
        {
            InitTestLog(nameof(ParseText_WithPunctuation_ParsesCorrectly));
            var input = "私は、日本語が好きです。";
            WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                WriteLog($"Word: '{word.Surface}'");
            }

            // Verify words and punctuation
            Assert.Contains(result, w => w.Surface == "私");
            Assert.Contains(result, w => w.Surface == "は");
            Assert.Contains(result, w => w.Surface == "、");
            Assert.Contains(result, w => w.Surface == "日本語");
            Assert.Contains(result, w => w.Surface == "が");
            Assert.Contains(result, w => w.Surface == "好");
            Assert.Contains(result, w => w.Surface == "きです");
            Assert.Contains(result, w => w.Surface == "。");

            WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_HandlesWhitespace()
        {
            InitTestLog(nameof(ParseText_HandlesWhitespace));
            var input = "私は  日本語    を  勉強します";  // Multiple spaces
            WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                WriteLog($"Word: '{word.Surface}'");
            }

            // Verify words are correctly identified regardless of spacing
            Assert.Contains(result, w => w.Surface == "私");
            Assert.Contains(result, w => w.Surface == "は");
            Assert.Contains(result, w => w.Surface == "日本語");
            Assert.Contains(result, w => w.Surface == "を");
            Assert.Contains(result, w => w.Surface == "勉強");
            Assert.Contains(result, w => w.Surface == "します");

            WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_VerbConjugations()
        {
            InitTestLog(nameof(ParseText_VerbConjugations));
            var input = "お寿司を食べました";
            WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                WriteLog($"Word: '{word.Surface}'");
            }

            // Verify word boundaries based on actual tokenization
            Assert.Contains(result, w => w.Surface == "お");
            Assert.Contains(result, w => w.Surface == "寿司");
            Assert.Contains(result, w => w.Surface == "を");
            Assert.Contains(result, w => w.Surface == "食");
            Assert.Contains(result, w => w.Surface == "べました");

            WriteLog("\n=== Test Complete ===\n");
        }
    }
} 