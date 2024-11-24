using Alexicon.API.Domain.PrimaryPorts.ApplyMove;
using Alexicon.Helpers;
using FluentValidation;

namespace Alexicon.API.Domain.Services.Validators;

public class ApplyMoveRequestValidator : AbstractValidator<ApplyMoveRequest>
{
    public ApplyMoveRequestValidator()
    {
        RuleFor(req => req.GameId)
            .Must(BeGuid)
            .WithMessage("Should be a GUID.");

        RuleFor(req => req.Player)
            .NotEmpty()
            .WithMessage("Is required.");

        RuleFor(req => req.LettersUsed)
            .Must(l => ContainCorrectNumberOfTiles(l, 1, 7))
            .WithMessage("Should contain 7 or fewer letters.");

        RuleFor(req => req.Location)
            .Must(UseValidScrabbleTileNotations)
            .WithMessage("Should use Scrabble tile notation, e.g. A1, F6, H12, O15 etc.");

        RuleFor(req => req.NewRack)
            .Must(l => BeEmptyOrContainCorrectNumberOfTiles(l, 7))
            .WithMessage("Should be empty or contain exactly 7 tiles.");

        RuleForEach(req => req.LettersUsed)
            .Must(BeValidScrabbleCharacter)
            .WithMessage("Should be letter A - Z or `?` for blanks.");
        
        RuleForEach(req => req.NewRack)
            .Must(BeValidScrabbleCharacter)
            .WithMessage("Should be letter A - Z or `?` for blanks.");
    }

    private bool ContainCorrectNumberOfTiles(List<char> chars, int min, int maxInclusive)
    {
        return chars.Count >= min && chars.Count <= maxInclusive;
    }

    private bool UseValidScrabbleTileNotations((string, string) locationToCheck)
    {
        return ScrabbleHelper.IsValidTileNotation(locationToCheck.Item1) && ScrabbleHelper.IsValidTileNotation(locationToCheck.Item2);
    }

    private bool BeEmptyOrContainCorrectNumberOfTiles(List<char> chars, int expectedLength)
    {
        return chars.Count == 0 || ContainCorrectNumberOfTiles(chars, expectedLength, expectedLength);
    }

    private static bool BeValidScrabbleCharacter(char charToValidate)
    {
        var upper = char.ToUpper(charToValidate);

        return char.IsAsciiLetterUpper(upper) || charToValidate == '?';
    }

    private static bool BeGuid(string attempt)
    {
        return Guid.TryParse(attempt, out _);
    }
}