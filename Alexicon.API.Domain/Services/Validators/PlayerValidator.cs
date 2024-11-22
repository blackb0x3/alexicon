using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using FluentValidation;

namespace Alexicon.API.Domain.Services.Validators;

public class PlayerValidator : AbstractValidator<NewPlayer>
{
    public PlayerValidator()
    {
        RuleFor(np => np.Username)
            .NotEmpty()
            .WithMessage(" is required.");

        RuleFor(np => np.DisplayName)
            .NotEmpty()
            .WithMessage(" is required.");

        RuleForEach(np => np.StartingRack)
            .Must(BeValidScrabbleCharacter)
            .WithMessage(" is not a valid Scrabble tile.");
    }

    private static bool BeValidScrabbleCharacter(char charToValidate)
    {
        var upper = char.ToUpper(charToValidate);

        return char.IsAsciiLetterUpper(upper) || charToValidate == '?';
    }
}