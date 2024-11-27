using Alexicon.API.Domain.PrimaryPorts.ApplyMove;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.Domain.Services.Validators;
using Alexicon.API.SecondaryPorts.Queries.GetGameById;
using MapsterMapper;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.Adapters;

public class ApplyMoveRequestHandler : IRequestHandler<ApplyMoveRequest, OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, InvalidMove>>
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

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, InvalidMove>> Handle(ApplyMoveRequest request, CancellationToken cancellationToken)
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

        var moveValidationResult = _newMoveValidator.ValidateMove(request, game);

        if (!moveValidationResult.IsValid)
        {
            return new InvalidMove
            {
                WordsCreated = moveValidationResult.WordsCreated,
                AttemptedLetters = request.LettersUsed
            };
        }
    }
}