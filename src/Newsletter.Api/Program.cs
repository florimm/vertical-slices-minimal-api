using FluentValidation;
using Newsletter.Api.Database;
using Newsletter.Api.Entities;
using Newsletter.Api.Features.Articles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDbContext, DbContext>();
builder.Services.AddValidatorsFromAssemblyContaining<Article>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

CreateArticleFeature.Register(app);
GetArticleByIdFeature.Register(app);

app.UseHttpsRedirection();

app.Run();

