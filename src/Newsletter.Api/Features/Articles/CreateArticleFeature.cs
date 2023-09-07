using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Database;

namespace Newsletter.Api.Features.Articles
{
    public static class CreateArticleFeature
    {
        public static void Register(this IEndpointRouteBuilder endpoints) =>
            endpoints.MapPost(
                "api/articles",
                async (
                    [AsParameters] CreateArticleRequest request,
                    [FromServices] IDbContext context,
                    IValidator<CreateArticleRequest> validator
                    ) => await Handle(request, context, validator));

        public static async Task<IResult> Handle(CreateArticleRequest request, IDbContext context, IValidator<CreateArticleRequest> validator)
        {
            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                return Results.BadRequest(new { Name = "CreateArticle.Validation", validation = validation.ToString() });
            }

            var article = new Entities.Article
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Content = request.Content,
                Tags = request.Tags,
                CreaedOnUtc = DateTime.UtcNow
            };
            context.Add(article);
            await context.SaveChangesAsync();
            return Results.Ok(article.Id);
        }

        public record CreateArticleRequest(string Title, string Content, List<string> Tags);

        public class CreateArticleRequestValidation : AbstractValidator<CreateArticleRequest>
        {
            public CreateArticleRequestValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Content).NotEmpty();
            }
        }

    }
}
