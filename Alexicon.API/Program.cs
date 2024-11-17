using Mediator;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*
 * Example
 * app.MapPost("/test",
        async (
        ) =>
        {
            return Results.Ok(new { foo = "bar" });
        })
    .WithName("test")
    .WithOpenApi();
 */

app.MapPost("/game",
        async (
            HttpRequest req,
            [FromServices] IMediator mediator
        ) =>
        {
            
        })
    .WithName("StartNewGame")
    .WithOpenApi();



await app.RunAsync();
