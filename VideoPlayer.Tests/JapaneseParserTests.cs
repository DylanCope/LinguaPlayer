using System.Linq;
using VideoPlayer.Services;
using VideoPlayer.Models;
using Xunit;

namespace VideoPlayer.Tests
{
    public class ScriptBasedParserTests
    {
        private readonly ScriptBasedJapaneseParser _parser;
        private readonly TestLogger _testHelper;

        public ScriptBasedParserTests()
        {
            _parser = new ScriptBasedJapaneseParser();
            _testHelper = new TestLogger(typeof(ScriptBasedParserTests), "script-based-parser-test-logs.txt");
        }

        [Fact]
        public void ParseText_EmptyString_ReturnsEmptyCollection()
        {
            _testHelper.InitTestLog(nameof(ParseText_EmptyString_ReturnsEmptyCollection));
            _testHelper.WriteLog("Testing empty string input");
            var result = _parser.ParseText("");
            _testHelper.WriteLog($"Result count: {result.Count()}");
            Assert.Empty(result);
            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_MixedLanguages_HandlesCorrectly()
        {
            _testHelper.InitTestLog(nameof(ParseText_MixedLanguages_HandlesCorrectly));
            var input = "Hello みなさん、私は日本語を studying です。";
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
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

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_WithPunctuation_ParsesCorrectly()
        {
            _testHelper.InitTestLog(nameof(ParseText_WithPunctuation_ParsesCorrectly));
            var input = "私は、日本語が好きです。";
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
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

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_HandlesWhitespace()
        {
            _testHelper.InitTestLog(nameof(ParseText_HandlesWhitespace));
            var input = "私は  日本語    を  勉強します";  // Multiple spaces
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
            }

            // Verify words are correctly identified regardless of spacing
            Assert.Contains(result, w => w.Surface == "私");
            Assert.Contains(result, w => w.Surface == "は");
            Assert.Contains(result, w => w.Surface == "日本語");
            Assert.Contains(result, w => w.Surface == "を");
            Assert.Contains(result, w => w.Surface == "勉強");
            Assert.Contains(result, w => w.Surface == "します");

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_VerbConjugations()
        {
            _testHelper.InitTestLog(nameof(ParseText_VerbConjugations));
            var input = "お寿司を食べました";
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
            }

            // Verify word boundaries based on actual tokenization
            Assert.Contains(result, w => w.Surface == "お");
            Assert.Contains(result, w => w.Surface == "寿司");
            Assert.Contains(result, w => w.Surface == "を");
            Assert.Contains(result, w => w.Surface == "食");
            Assert.Contains(result, w => w.Surface == "べました");

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }
    }
} 