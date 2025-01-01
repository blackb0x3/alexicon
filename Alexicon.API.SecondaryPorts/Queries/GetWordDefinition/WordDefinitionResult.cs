namespace Alexicon.API.SecondaryPorts.Queries.GetWordDefinition;

public class WordDefinitionResult
{
    public bool IsScrabbleWord { get; set; }

    public List<string> Definitions { get; set; }
}