using System.Collections.Generic;
using VideoPlayer.Models;

namespace VideoPlayer.Services
{
    public interface IJapaneseParser
    {
        /// <summary>
        /// Parses Japanese text and returns a list of parsed words with their properties
        /// </summary>
        /// <param name="text">The Japanese text to parse</param>
        /// <returns>A list of parsed words with their properties</returns>
        IEnumerable<ParsedWord> ParseText(string text);
    }
} 