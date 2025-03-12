namespace VideoPlayer.Models
{
    public class ParsedWord
    {
        public string Surface { get; set; } = string.Empty;
        public string Reading { get; set; } = string.Empty;
        public string DictionaryForm { get; set; } = string.Empty;
        public string PartOfSpeech { get; set; } = string.Empty;

        // Additional properties that might be useful for dictionary lookup
        public string? Pronunciation { get; set; }
        public bool IsKnown { get; set; } = true;
    }
} 