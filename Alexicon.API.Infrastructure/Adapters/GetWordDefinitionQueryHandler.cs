using Alexicon.API.Infrastructure.Services;
using Alexicon.API.SecondaryPorts.Queries.GetWordDefinition;
using Mediator;

namespace Alexicon.API.Infrastructure.Adapters;

public class GetWordDefinitionQueryHandler : IQueryHandler<GetWordDefinitionQuery, WordDefinitionResult>
{
    private readonly IWordDefinitionService _wordDefinitionService;

    public GetWordDefinitionQueryHandler(IWordDefinitionService wordDefinitionService)
    {
        _wordDefinitionService = wordDefinitionService;
    }

    public async ValueTask<WordDefinitionResult> Handle(GetWordDefinitionQuery query, CancellationToken cancellationToken)
    {
        var wordDefinition = await _wordDefinitionService.GetWordDefinition(query.Word, cancellationToken);

        return new WordDefinitionResult
        {
            IsScrabbleWord = wordDefinition.IsScrabbleWord,
            Definitions = wordDefinition.Definitions
        };
    }
}