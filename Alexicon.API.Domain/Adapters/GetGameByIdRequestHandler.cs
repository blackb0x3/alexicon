using Alexicon.API.Domain.PrimaryPorts.GetGameById;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.Domain.Services.Validators;
using Alexicon.API.SecondaryPorts.Queries.GetGameById;
using MapsterMapper;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.Adapters;

public class GetGameByIdRequestHandler : IRequestHandler<GetGameByIdRequest, OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation>>
{
    private readonly GetGameByIdRequestValidator _validator;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetGameByIdRequestHandler(GetGameByIdRequestValidator validator, IMediator mediator, IMapper mapper)
    {
        _validator = validator;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation>> Handle(GetGameByIdRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            return new ValidationRepresentation(validationResult);
        }

        var parsedId = Guid.Parse(request.GameId);
        var result = await _mediator.Send(new GetGameByIdQuery(parsedId), cancellationToken);

        if (result is null)
        {
            return new EntityNotFoundRepresentation("Unable to find game with requested ID.", request.GameId);
        }

        return _mapper.Map<GameRepresentation>(result);
    }
}