using FluentValidation.Results;

namespace Alexicon.API.Domain.Representations;

public class ValidationRepresentation
{
    public ValidationRepresentation(ValidationResult result)
    {
        Errors = new Dictionary<string, ValidationError>();

        foreach (var err in result.Errors)
        {
            Errors.Add(err.PropertyName, new ValidationError(err.PropertyName, err.ErrorCode, err.ErrorMessage, err.AttemptedValue));
        }
    }
    
    public Dictionary<string, ValidationError> Errors { get; set; }
}