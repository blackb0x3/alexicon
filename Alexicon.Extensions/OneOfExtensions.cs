using OneOf;

namespace Alexicon.Extensions;

public static class OneOfExtensions
{
    public static bool IsOfType<T>(this IOneOf oneOf)
    {
        return oneOf.Value is T;
    }
}