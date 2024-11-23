namespace Alexicon.API.Domain.Services.Mappers;

public interface IAlexiconMapper<in TSource, out TDestination>
{
    TDestination Map(TSource src);
}