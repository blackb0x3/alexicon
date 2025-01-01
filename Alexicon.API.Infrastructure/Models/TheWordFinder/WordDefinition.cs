namespace Alexicon.API.Infrastructure.Models.TheWordFinder;

public class WordDefinition
{
    public List<string> Definitions { get; set; }

    public bool IsWordsWithFriendsWord { get; set; }

    public bool IsScrabbleUsWord { get; set; }

    public bool IsScrabbleUkWord { get; set; }

    /// <remarks>
    /// Equivalent to SOWPODS in the UK
    /// </remarks>
    public bool IsEnglishInternationalWord { get; set; }
    
    public bool IsScrabbleGlobalWord { get; set; }

    public bool IsInEnable1Dictionary { get; set; }
}