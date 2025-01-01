using Alexicon.API.Domain.PrimaryPorts.ApplyMove;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.Domain.Services.Validators;
using Alexicon.API.SecondaryPorts.Commands.ApplyGameMove;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.API.SecondaryPorts.Queries.GetGameById;
using Alexicon.API.SecondaryPorts.Queries.GetPlayerByUsername;
using Alexicon.Extensions;
using MapsterMapper;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.Adapters;

public class ApplyMoveRequestHandler : IRequestHandler<ApplyMoveRequest, OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, PlayerNotInGame, InvalidMove>>
{
    private readonly ApplyMoveRequestValidator _requestValidator;
    private readonly IMediator _mediator;
    private readonly INewMoveValidator _newMoveValidator;
    private readonly IMapper _mapper;

    public ApplyMoveRequestHandler(ApplyMoveRequestValidator requestValidator, IMediator mediator, INewMoveValidator newMoveValidator, IMapper mapper)
    {
        _requestValidator = requestValidator;
        _mediator = mediator;
        _newMoveValidator = newMoveValidator;
        _mapper = mapper;
    }

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, PlayerNotInGame, InvalidMove>> Handle(ApplyMoveRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _requestValidator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationRepresentation(validationResult);
        }

        var gameGuid = Guid.Parse(request.GameId);
        var game = await _mediator.Send(new GetGameByIdQuery(gameGuid), cancellationToken);

        if (game is null)
        {
            return new EntityNotFoundRepresentation("Unable to find game with requested ID.", request.GameId);
        }

        var player = await _mediator.Send(new GetPlayerByUsernameQuery(request.Player), cancellationToken);

        if (player is null)
        {
            return new EntityNotFoundRepresentation("Unable to find player with requested username.", request.Player);
        }

        var (playerIsInGame, matchingPlayer) = PlayerIsInGame(game, player);

        if (!playerIsInGame)
        {
            return new PlayerNotInGame();
        }

        var gameRep = _mapper.Map<GameRepresentation>(game);

        var moveValidationResult = await _newMoveValidator.ValidateMove(request, gameRep, cancellationToken);

        if (!moveValidationResult.IsValid)
        {
            return new InvalidMove
            {
                WordsCreated = moveValidationResult.WordsCreated,
                AttemptedLetters = request.LettersUsed
            };
        }
        
        UpdateCurrentRack(matchingPlayer!, gameRep, request.LettersUsed, request.NewRack);

        var gameMove = new GameMove
        {
            Player = matchingPlayer!,
            FirstLetterX = moveValidationResult.FirstLetterX,
            FirstLetterY = moveValidationResult.FirstLetterY,
            LastLetterX = moveValidationResult.LastLetterX,
            LastLetterY = moveValidationResult.LastLetterY,
            Score = moveValidationResult.Score,
            LettersUsed = request.LettersUsed,
            WordsCreated = moveValidationResult.WordsCreated.Select(wc => wc.Word).ToList()
        };

        var applyMoveCommand = new ApplyGameMoveCommand(gameGuid, gameMove);

        var applyMoveResult = await _mediator.Send(applyMoveCommand, cancellationToken);

        if (applyMoveResult.IsOfType<ApplyMoveFailure>())
        {
            throw new Exception($"Failed to apply move to game: {applyMoveResult.AsT1.Reason}");
        }

        var updatedGame = await _mediator.Send(new GetGameByIdQuery(gameGuid), cancellationToken);

        return _mapper.Map<GameRepresentation>(updatedGame!);
    }

    private static void UpdateCurrentRack(GamePlayer gamePlayer, GameRepresentation gameRep, IEnumerable<char> lettersUsed, List<char> newRack)
    {
        if (!newRack.Any())
        {
            var unusedLetters = gamePlayer.CurrentRack.Except(lettersUsed).ToList();
            newRack.AddRange(unusedLetters);
            var lettersToGet = 7 - unusedLetters.Count;
            var newLetters = gameRep.State.Bag.PopRandomElements(lettersToGet);
            newRack.AddRange(newLetters);
        }

        gamePlayer.CurrentRack = newRack;
    }

    private static (bool, GamePlayer? matchingPlayer) PlayerIsInGame(Game game, Player player)
    {
        var matchingPlayer = game.Players.SingleOrDefault(gp => string.Equals(gp.Player.Username, player.Username, StringComparison.OrdinalIgnoreCase));

        return (matchingPlayer is not null, matchingPlayer);
    }
}