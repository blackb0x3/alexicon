using System.Text.Json;
using Alexicon.API.Domain.PrimaryPorts.ApplyMove;
using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using Alexicon.API.Domain.PrimaryPorts.GetGameById;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.Domain.Services.Converters;
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
    opts.JsonSerializerOptions.Converters.Add(new ByteArrayToJsonArrayConverter());
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

app.MapGet("/game/{gameId}",
        [SwaggerOperation("Retrieve a Scrabble game using its ID.")]
        [SwaggerResponse(StatusCodes.Status200OK, Outcomes.Ok, typeof(GameRepresentation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Outcomes.ValidationFailed, typeof(ValidationRepresentation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Outcomes.NotFound, typeof(EntityNotFoundRepresentation))]
        async (
            HttpRequest httpReq,
            [FromServices] IMediator mediator,
            [FromServices] IMapper mapper,
            [FromRoute] string gameId
        ) =>
        {
            var request = new GetGameByIdRequest(gameId);

            var response = await mediator.Send(request);

            return response.Match<IResult>(
                game => Results.Ok(game),
                invalidRequest => Results.BadRequest(invalidRequest),
                notFound => Results.NotFound(notFound)
            );
        })
    .WithName("GetGameById")
    .WithOpenApi();

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

app.MapPut("/game/{gameId}/move",
        [SwaggerOperation("Add a player's move to a game.")]
        [SwaggerResponse(StatusCodes.Status200OK, Outcomes.Ok, typeof(GameRepresentation))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Outcomes.ValidationFailed, typeof(ValidationRepresentation))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Outcomes.NotFound, typeof(EntityNotFoundRepresentation))]
        async (
            HttpRequest httpReq,
            [FromServices] IMediator mediator,
            [FromServices] IMapper mapper,
            [FromRoute] string gameId,
            [FromBody] ApplyMoveHttpRequest requestModel
        ) =>
        {
            var request = new ApplyMoveRequest(gameId, requestModel.Player, requestModel.LettersUsed, requestModel.Location, requestModel.NewRack);

            var response = await mediator.Send(request);

            return response.Match<IResult>(
                game => Results.Ok(game),
                invalidRequest => Results.BadRequest(invalidRequest),
                gameNotFound => Results.NotFound(gameNotFound)
            );
        })
    .WithName("ApplyMoveToGame")
    .WithOpenApi();

await app.RunAsync();
