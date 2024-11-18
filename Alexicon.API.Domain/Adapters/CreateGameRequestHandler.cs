using Alexicon.API.Domain.Models;
using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.Extensions;
using FluentValidation;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.Adapters;

internal class CreateGameRequestHandler : IRequestHandler<CreateGameRequest, OneOf<GameRepresentation, ValidationRepresentation>>
{
    private readonly IValidator<CreateGameRequest> _validator;

    public CreateGameRequestHandler(IValidator<CreateGameRequest> validator)
    {
        _validator = validator;
    }

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation>> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationRepresentation(validationResult);
        }

        var players = CreateGamePlayers(request.Players);
        
        // TODO think about infrastructure
        // need a way of storing in-progress games
        // SQLite?
        // LiteDB?
        // Mongo in Azure?
        // defo doing document storage, not faffing around with SQL / relational DBs for this project
    }

    private List<GamePlayer> CreateGamePlayers(Dictionary<string, NewPlayer> newPlayers)
    {
        var gamePlayers = new List<GamePlayer>();
        var bag = Scrabble.StartingBag.ToList();

        foreach (var newPlayer in newPlayers)
        {
            var gamePlayer = new GamePlayer
            {
                Username = newPlayer.Key,
                DisplayName = newPlayer.Value.DisplayName,
                CurrentRack = newPlayer.Value.StartingRack
            };

            if (!gamePlayer.CurrentRack.Any())
            {
                gamePlayer.CurrentRack = GenerateNewRack(bag);
            }

            gamePlayers.Add(gamePlayer);
        }

        return gamePlayers;
    }

    private List<char> GenerateNewRack(List<char> bag)
    {
        // to ensure randomness
        bag.ShuffleFiveTimes();

        return bag.PopRandomElements(7);
    }
}