using System.Collections.Generic;
using NMeCab.Specialized;
using VideoPlayer.Models;

namespace VideoPlayer.Services
{
    public class MeCabJapaneseParser : IJapaneseParser
    {

        public IEnumerable<ParsedWord> ParseText(string text)
        {
            var results = new List<ParsedWord>();
            using (var tagger = MeCabIpaDicTagger.Create())
            {
                var nodes = tagger.Parse(text);
                foreach (var node in nodes) 
                {
                    // Skip BOS/EOS nodes
                    if (node.Feature == "BOS/EOS")
                        continue;

                    results.Add(new ParsedWord
                    {
                        Surface = node.Surface,
                        DictionaryForm = string.Empty, // Add later
                        PartOfSpeech = node.PartsOfSpeech,  // Part of speech is the 1st feature
                        Reading = node.Reading // We'll handle readings later
                    });
                }
            }

            return results;
        }
    }
} 