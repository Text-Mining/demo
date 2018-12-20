namespace Textmining.Demo.Web.Models
{
    public class OpinionSummarizerModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public string Benefit { get; set; }
        public string Weakness { get; set; }
        public int CategoryId { get; set; }
        public bool SpellCorrection { get; set; }
        public bool Stemming { get; set; }
        public bool SplitCompoundSentence { get; set; }
    }
}