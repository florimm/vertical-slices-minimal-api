namespace Newsletter.Api.Entities
{
    public class Article
    {
        public Guid Id { get; internal set; }
        public string Title { get; internal set; }
        public string Content { get; internal set; }
        public List<string> Tags { get; internal set; }
        public DateTime CreaedOnUtc { get; internal set; }
    }
}
