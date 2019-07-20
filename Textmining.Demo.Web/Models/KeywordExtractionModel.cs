namespace Textmining.Demo.Web.Models
{
    public class KeywordExtractionModel
    {
        public string Text { get; set; }

        public int MinWordLength { get; set; }

        public int MaxWordCount { get; set; }

        public int MinKeywordFrequency { get; set; }

        public int ResultKeywordCount { get; set; }

        public string Method { get; set; }
    }
}
