using System.Linq;
using VideoPlayer.Services;
using VideoPlayer.Models;
using Xunit;
using System;
using System.IO;

namespace VideoPlayer.Tests
{
    public class MeCabJapaneseParserTests
    {
        private readonly MeCabJapaneseParser _parser;
        private readonly TestLogger _testHelper;

        public MeCabJapaneseParserTests()
        {
            _parser = new MeCabJapaneseParser();
            _testHelper = new TestLogger(typeof(MeCabJapaneseParserTests), "mecab-parser-test-logs.txt");
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
        public void ParseText_BasicVerb_IdentifiesPartsOfSpeech()
        {
            _testHelper.InitTestLog(nameof(ParseText_BasicVerb_IdentifiesPartsOfSpeech));
            var input = "食べる";
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
                _testHelper.WriteLog($"Part of Speech: '{word.PartOfSpeech}'");
                _testHelper.WriteLog($"Reading: '{word.Reading}'");
            }

            var verb = result.Single();
            Assert.Equal("食べる", verb.Surface);
            Assert.Equal("動詞", verb.PartOfSpeech);
            Assert.NotEmpty(verb.Reading);

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_VerbConjugation_IdentifiesBaseForm()
        {
            _testHelper.InitTestLog(nameof(ParseText_VerbConjugation_IdentifiesBaseForm));
            var input = "食べました";
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
                _testHelper.WriteLog($"Part of Speech: '{word.PartOfSpeech}'");
                _testHelper.WriteLog($"Dictionary Form: '{word.DictionaryForm}'");
                _testHelper.WriteLog($"Reading: '{word.Reading}'");
            }

            // Should identify the verb stem and auxiliary endings
            Assert.True(result.Count >= 2); // At least verb stem + ました
            var verbStem = result.First();
            Assert.Equal("食べ", verbStem.Surface);
            Assert.Equal("動詞", verbStem.PartOfSpeech);
            // Assert.Equal("食べる", verbStem.DictionaryForm);  // not implemented yet

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }

        [Fact]
        public void ParseText_ParticlesAndPunctuation_IdentifiesCorrectly()
        {
            _testHelper.InitTestLog(nameof(ParseText_ParticlesAndPunctuation_IdentifiesCorrectly));
            var input = "私は、本を読みます。";
            _testHelper.WriteLog($"Input text: {input}");
            var result = _parser.ParseText(input).ToList();
            
            _testHelper.WriteLog($"Result count: {result.Count}");
            foreach (var word in result)
            {
                _testHelper.WriteLog($"Word: '{word.Surface}'");
                _testHelper.WriteLog($"Part of Speech: '{word.PartOfSpeech}'");
                _testHelper.WriteLog($"Reading: '{word.Reading}'");
            }

            // Check particles
            Assert.Contains(result, w => w.Surface == "は" && w.PartOfSpeech == "助詞");
            Assert.Contains(result, w => w.Surface == "を" && w.PartOfSpeech == "助詞");
            
            // Check punctuation
            Assert.Contains(result, w => w.Surface == "、");
            Assert.Contains(result, w => w.Surface == "。");

            // Check nouns and verbs
            Assert.Contains(result, w => w.Surface == "私" && w.PartOfSpeech == "名詞");
            Assert.Contains(result, w => w.Surface == "本" && w.PartOfSpeech == "名詞");
            Assert.Contains(result, w => w.Surface == "読み" && w.PartOfSpeech == "動詞");

            _testHelper.WriteLog("\n=== Test Complete ===\n");
        }
    }
} 