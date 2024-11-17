namespace Alexicon.API.Domain.Representations;

public class ValidationError
{
    public ValidationError(string propertyName, string errorCode, string errorMessage, object attemptedValue)
    {
        Property = propertyName;
        Code = errorCode;
        Description = errorMessage;
        ProvidedValue = attemptedValue;
    }

    public string Property { get; set; }
    
    public string Code { get; set; }
    
    public string Description { get; set; }
    
    public object ProvidedValue { get; set; }
}