using Alexicon.API.Domain.PrimaryPorts.GetGameById;
using FluentValidation;

namespace Alexicon.API.Domain.Services.Validators;

public class GetGameByIdRequestValidator : AbstractValidator<GetGameByIdRequest>
{
    public GetGameByIdRequestValidator()
    {
        RuleFor(req => req.GameId)
            .Must(BeGuid)
            .WithMessage("Should be a GUID.");
    }

    private static bool BeGuid(string attempt)
    {
        return Guid.TryParse(attempt, out _);
    }
}