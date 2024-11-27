using System.Net;
using System.Text.Json;

namespace Alexicon.API.Extensions;

public static class MoreResults
{
    /// <remarks>
    /// The default Results.Forbid() method doesn't return JSON data in the standard format compared to the other
    /// factory methods in results.
    /// </remarks>
    public static IResult Forbidden(object response)
    {
        return Results.Json(response, JsonSerializerOptions.Default, contentType: "application/json", statusCode: (int) HttpStatusCode.Forbidden);
    }
}