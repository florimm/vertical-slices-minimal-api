using Newsletter.Api.Entities;

namespace Newsletter.Api.Database
{
    public interface IDbContext
    {
        void Add(Article article);

        Task SaveChangesAsync();

        Task<List<Article>> GetAllArticles();
    }
    public class DbContext : IDbContext
    {
        List<Article> _articles = new List<Article>();
        public void Add(Article article)
        {
            _articles.Add(article);
        }

        public Task<List<Article>> GetAllArticles()
        {
            return Task.FromResult(_articles);
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
