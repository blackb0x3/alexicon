using Alexicon.API.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Alexicon.API.Infrastructure.Services;

public interface IWordDefinitionService
{
    Task<WordDefinition> GetWordDefinition(string word, CancellationToken cancellationToken);
}

public class WordDefinitionService : IWordDefinitionService
{
    private static readonly MemoryCacheEntryOptions _cacheEntryOptions = new MemoryCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromDays(1),
        Priority = CacheItemPriority.Low
    };

    private readonly ITheWordFinderClient _theWordFinderClient;
    private readonly IMemoryCache _memoryCache;

    public WordDefinitionService(ITheWordFinderClient theWordFinderClient, IMemoryCache memoryCache)
    {
        _theWordFinderClient = theWordFinderClient;
        _memoryCache = memoryCache;
    }

    public async Task<WordDefinition> GetWordDefinition(string word, CancellationToken cancellationToken)
    {
        var key = word.ToUpper();
        var cachedDefinition = _memoryCache.Get<WordDefinition>(key);

        if (cachedDefinition is not null)
        {
            return cachedDefinition;
        }

        Models.TheWordFinder.WordDefinition result = await _theWordFinderClient.GetWordDefinition(word, cancellationToken);

        _memoryCache.Set(key, result, _cacheEntryOptions);

        return new WordDefinition
        {
            Definitions = result.Definitions,
            IsScrabbleWord = result.IsScrabbleUkWord // restricted to UK definitions currently, may change to include US definitions if requested
        };
    }
}