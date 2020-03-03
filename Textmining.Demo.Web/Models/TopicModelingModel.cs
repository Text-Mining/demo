namespace Textmining.Demo.Web.Models
{
    public class TopicModelingModel
    {
        public string Text { get; set; }

        public string[] Documents { get; set; }

        public int TopicsCount { get; set; }

        public int WordsPerTopicCount { get; set; }

        public string Method { get; set; }
    }
}
