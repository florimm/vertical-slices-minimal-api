using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newsletter.Api.Database;

namespace Newsletter.Api.Features.Articles
{
    public static class GetArticleByIdFeature
    {
        public static void Register(this IEndpointRouteBuilder endpoints) =>
       endpoints.MapGet(
                "api/articles/{id}",
                async(
                    [AsParameters] GetArticleByIdQuery request,
                    [FromServices] IDbContext context
                    ) => await Handle(request, context));

        public static async Task<IResult> Handle(GetArticleByIdQuery request, IDbContext context)
        {
            var result = (await context.GetAllArticles()).Where(t => t.Id == request.Id).FirstOrDefault();
            if (result == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result);

        }

        public record GetArticleByIdQuery([FromRoute] Guid Id);
    }
}
