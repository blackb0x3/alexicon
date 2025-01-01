using Mediator;
using OneOf;

namespace Alexicon.API.SecondaryPorts.Queries.GetWordDefinition;

public record GetWordDefinitionQuery(
    string Word
) : IQuery<WordDefinitionResult>;