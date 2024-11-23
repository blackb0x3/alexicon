using Alexicon.API.Domain.PrimaryPorts.ApplyMove;
using Alexicon.API.Domain.Representations;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.Domain.Services.Validators;
using MapsterMapper;
using Mediator;
using OneOf;

namespace Alexicon.API.Domain.Adapters;

public class ApplyMoveRequestHandler : IRequestHandler<ApplyMoveRequest, OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation>>
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

    public async ValueTask<OneOf<GameRepresentation, ValidationRepresentation, EntityNotFoundRepresentation>> Handle(ApplyMoveRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}