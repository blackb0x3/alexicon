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
            .WithMessage("Is required.");

        RuleFor(np => np.DisplayName)
            .NotEmpty()
            .WithMessage("Is required.");

        // All Scrabble games start with 7 tiles on each player's rack. If no letters are present, this is also
        // considered valid as we will generate a new rack in the API.
        RuleFor(np => np.StartingRack)
            .Must(rack => BeEmptyOrContainCorrectNumberOfTiles(rack, 7))
            .WithMessage("Should be empty or contain exactly 7 tiles.");

        RuleForEach(np => np.StartingRack)
            .Must(BeValidScrabbleCharacter)
            .WithMessage("Should be letter A - Z or `?` for blanks.");
    }

    private bool BeEmptyOrContainCorrectNumberOfTiles(List<char> chars, int expectedLength)
    {
        return chars.Count == 0 || chars.Count == expectedLength;
    }

    private static bool BeValidScrabbleCharacter(char charToValidate)
    {
        var upper = char.ToUpper(charToValidate);

        return char.IsAsciiLetterUpper(upper) || charToValidate == '?';
    }
}