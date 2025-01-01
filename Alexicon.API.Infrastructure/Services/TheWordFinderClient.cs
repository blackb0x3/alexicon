using Alexicon.API.Infrastructure.Models.TheWordFinder;

namespace Alexicon.API.Infrastructure.Services;

public interface ITheWordFinderClient
{
    Task<WordDefinition> GetWordDefinition(string word, CancellationToken cancellationToken);
}

public class TheWordFinderClient : ITheWordFinderClient
{
    public Task<WordDefinition> GetWordDefinition(string word, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}