using Alexicon.API.Domain.Models;
using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.Domain.Services.Validators;
using Alexicon.API.SecondaryPorts.Commands.CreateGame;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.Extensions;
using MapsterMapper;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.Adapters;

public class CreateGameRequestHandler : IRequestHandler<CreateGameRequest, OneOf<GameRepresentation, ValidationRepresentation>>
{
    private readonly CreateGameRequestValidator _validator;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateGameRequestHandler(CreateGameRequestValidator validator, IMediator mediator, IMapper mapper)
    {
        _validator = validator;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation>> Handle(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationRepresentation(validationResult);
        }

        var players = CreateGamePlayers(request.Players);

        var game = new Game
        {
            Players = players
        };

        var saveResult = await _mediator.Send(new CreateGameCommand(game), cancellationToken);

        if (saveResult.IsOfType<GameNotSaved>())
        {
            throw new Exception($"Failed to create new game: {saveResult.AsT1.Reason}");
        }

        game.Id = saveResult.AsT0.Game.Id;

        return _mapper.Map<GameRepresentation>(game);
    }

    private List<GamePlayer> CreateGamePlayers(List<NewPlayer> newPlayers)
    {
        var gamePlayers = new List<GamePlayer>();
        var bag = Scrabble.StartingBag.ToList();

        foreach (var newPlayer in newPlayers)
        {
            var gamePlayer = new GamePlayer
            {
                CurrentRack = newPlayer.StartingRack,
                Player = new Player
                {
                    Username = newPlayer.Username,
                    DisplayName = newPlayer.DisplayName
                }
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

        // every game starts with 7 letters in each player's rack
        return bag.PopRandomElements(7);
    }
}