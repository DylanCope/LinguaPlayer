using System;
using System.Collections.Generic;
using System.Linq;
using WanaKanaNet;
using VideoPlayer.Models;

namespace VideoPlayer.Services
{
    public class ScriptBasedJapaneseParser : IJapaneseParser
    {
        public IEnumerable<ParsedWord> ParseText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Enumerable.Empty<ParsedWord>();

            // Strip whitespace and normalize spaces
            text = string.Join(" ", text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries));

            var tokens = WanaKana.Tokenize(text);
            var results = new List<ParsedWord>();

            foreach (var token in tokens)
            {
                var tokenStr = token.Content;
                if (string.IsNullOrEmpty(tokenStr) || string.IsNullOrWhiteSpace(tokenStr))
                {
                    continue;
                }

                results.Add(new ParsedWord
                {
                    Surface = tokenStr,
                    Reading = string.Empty, // We'll handle readings later
                    DictionaryForm = tokenStr,
                    PartOfSpeech = string.Empty // We'll handle POS later
                });
            }

            return results;
        }
    }
} 