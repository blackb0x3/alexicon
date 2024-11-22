using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using FluentValidation;

namespace Alexicon.API.Domain.Services.Validators;

public class CreateGameRequestValidator : AbstractValidator<CreateGameRequest>
{
    public CreateGameRequestValidator()
    {
        RuleForEach(req => req.Players.Values)
            .SetValidator(new PlayerValidator());
    }
}