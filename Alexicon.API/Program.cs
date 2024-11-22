using System.Text.Json;
using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.IoC;
using Alexicon.API.Models.Requests;
using Alexicon.API.Models.Response;
using MapsterMapper;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(opts =>
{
    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
ApiInstaller.Install(builder.Services, builder.Configuration);

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
        [SwaggerOperation("Create a new Scrabble game.")]
        [SwaggerResponse(StatusCodes.Status200OK, Outcomes.Ok, typeof(GameRepresentation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Outcomes.ValidationFailed, typeof(ValidationRepresentation))]
        async (
            HttpRequest httpReq,
            [FromServices] IMediator mediator,
            [FromServices] IMapper mapper,
            [FromBody] CreateGameHttpRequest req
        ) =>
        {
            var request = mapper.Map<CreateGameRequest>(req);

            var response = await mediator.Send(request);

            return response.Match<IResult>(
                created => Results.Ok(created),
                invalidRequest => Results.BadRequest(invalidRequest)
            );
        })
    .WithName("StartNewGame")
    .WithOpenApi();



await app.RunAsync();
