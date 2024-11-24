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
    private readonly ApplyMoveRequestValidator _validator;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ApplyMoveRequestHandler(ApplyMoveRequestValidator validator, IMediator mediator, IMapper mapper)
    {
        _validator = validator;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation, InvalidMove>> Handle(ApplyMoveRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

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
    }
}