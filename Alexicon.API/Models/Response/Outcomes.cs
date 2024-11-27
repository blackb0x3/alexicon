namespace Alexicon.API.Models.Response;

public static class Outcomes
{
    public const string Ok = "Ok";

    public const string Created = "Created";

    public const string Updated = "Updated";

    public const string EndpointDoesNotExist = "Endpoint Does Not Exist";
    
    public const string InvalidContentType = "InvalidContentType";

    public const string MethodNotAllowed = "MethodNotAllowed";

    public const string ModelBindingFailed = "ModelBindingFailed";

    public const string ValidationFailed = "ValidationFailed";

    public const string Forbidden = "Forbidden";
    
    public const string NotFound = "NotFound";

    public const string UnprocessableRequest = "UnprocessableRequest";
    
    public const string InternalServerError = "InternalServerError";
}