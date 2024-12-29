namespace Alexicon.API.Domain.Models;

public static class Scrabble
{
    public static readonly Dictionary<char, int> BagCount = new Dictionary<char, int>
    {
        { 'A', 9 },
        { 'B', 2 },
        { 'C', 2 },
        { 'D', 4 },
        { 'E', 12 },
        { 'F', 2 },
        { 'G', 3 },
        { 'H', 2 },
        { 'I', 9 },
        { 'J', 1 },
        { 'K', 1 },
        { 'L', 4 },
        { 'M', 2 },
        { 'N', 6 },
        { 'O', 8 },
        { 'P', 2 },
        { 'Q', 1 },
        { 'R', 6 },
        { 'S', 4 },
        { 'T', 6 },
        { 'U', 4 },
        { 'V', 2 },
        { 'W', 2 },
        { 'X', 1 },
        { 'Y', 2 },
        { 'Z', 1 },
        { '?', 2 }
    };

    public static IEnumerable<char> StartingBag
    {
        get
        {
            foreach (var entry in BagCount)
            {
                for (var i = 0; i < entry.Value; i++)
                {
                    yield return entry.Key;
                }
            }
        }
    }

    public static Dictionary<char, short> LetterScores = new Dictionary<char, short>
    {
        {'A', 1},
        {'B', 3},
        {'C', 3},
        {'D', 2},
        {'E', 1},
        {'F', 4},
        {'G', 2},
        {'H', 4},
        {'I', 1},
        {'J', 8},
        {'K', 5},
        {'L', 1},
        {'M', 3},
        {'N', 1},
        {'O', 1},
        {'P', 3},
        {'Q', 10},
        {'R', 1},
        {'S', 1},
        {'T', 1},
        {'U', 1},
        {'V', 4},
        {'W', 4},
        {'X', 8},
        {'Y', 4},
        {'Z', 10},
    };

    public static Dictionary<(short, short), short> LetterMultiplierLocations = new Dictionary<(short, short), short>
    {
        // DOUBLE LETTER
        { (3, 0), 2 },
        { (11, 0), 2 },
        { (6, 2), 2 },
        { (8, 2), 2 },
        { (0, 3), 2 },
        { (7, 3), 2 },
        { (14, 3), 2 },
        { (2, 6), 2 },
        { (6, 6), 2 },
        { (8, 6), 2 },
        { (12, 6), 2 },
        { (3, 7), 2 },
        { (11, 7), 2 },
        { (2, 8), 2 },
        { (6, 8), 2 },
        { (8, 8), 2 },
        { (12, 8), 2 },
        { (0, 11), 2 },
        { (7, 11), 2 },
        { (14, 11), 2 },
        { (6, 12), 2 },
        { (8, 12), 2 },
        { (3, 14), 2 },
        { (11, 14), 2 },
        
        // TRIPLE LETTER
        { (5, 1), 3 },
        { (9, 1), 3 },
        { (1, 5), 3 },
        { (5, 5), 3 },
        { (9, 5), 3 },
        { (13, 5), 3 },
        { (1, 9), 3 },
        { (5, 9), 3 },
        { (9, 9), 3 },
        { (13, 9), 3 },
        { (5, 13), 3 },
        { (9, 13), 3 }
    };

    public static Dictionary<(short, short), short> WordMultiplierLocations = new Dictionary<(short, short), short>
    {
        // DOUBLE WORD
        { (1, 1), 2 },
        { (2, 2), 2 },
        { (3, 3), 2 },
        { (4, 4), 2 },
        { (13, 1), 2 },
        { (12, 2), 2 },
        { (11, 3), 2 },
        { (10, 4), 2 },
        { (1, 13), 2 },
        { (2, 12), 2 },
        { (3, 11), 2 },
        { (4, 10), 2 },
        { (13, 13), 2 },
        { (12, 12), 2 },
        { (11, 11), 2 },
        { (10, 10), 2 },
        // centre tile is a double word too
        { (7, 7), 2 },
        
        // TRIPLE WORD
        { (0, 0), 3 },
        { (7, 0), 3 },
        { (14, 0), 3 },
        { (0, 7), 3 },
        { (14, 7), 3 },
        { (0, 14), 3 },
        { (7, 14), 3 },
        { (14, 14), 3 }
    };

    public const short BoardSize = 15;
}