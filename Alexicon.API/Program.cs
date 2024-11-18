using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using Alexicon.API.Models.Requests;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

app.MapPost("/game",
        [SwaggerOperation("")]
        async (
            HttpRequest httpReq,
            [FromServices] IMediator mediator,
            [FromServices] IMapper mapper,
            [FromBody] CreateGameHttpRequest req
        ) =>
        {
            var request = mapper.Map<CreateGameRequest>(req);

            var response = await mediator.Send(request);
        })
    .WithName("StartNewGame")
    .WithOpenApi();



await app.RunAsync();
