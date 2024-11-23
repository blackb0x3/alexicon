using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using FluentValidation;
using OneOf.Types;

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

        RuleFor(np => np.StartingRack)
            .Must(ContainCorrectNumberOfTiles);

        RuleForEach(np => np.StartingRack)
            .Must(BeValidScrabbleCharacter)
            .WithMessage(" is not a valid Scrabble tile.");
    }

    private static bool ContainCorrectNumberOfTiles(List<char> rack)
    {
        // All Scrabble games start with 7 tiles on each player's rack.
        // If no letters are present, this is also considered valid as
        // we will generate a new rack in the API.
        return rack.Count is 0 or 7;
    }

    private static bool BeValidScrabbleCharacter(char charToValidate)
    {
        var upper = char.ToUpper(charToValidate);

        return char.IsAsciiLetterUpper(upper) || charToValidate == '?';
    }
}