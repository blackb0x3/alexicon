namespace Alexicon.Helpers;

public class ScrabbleHelper
{
    public static string MapToScrabbleTileNotation(short x, short y)
    {
        var columnLetter = (char)(x + 65);
        var rowNumber = ++y;

        return $"{columnLetter}{rowNumber}";
    }

    public static (short x, short y) MapToArrayIndex(string scrabbleTileNotation)
    {
        var columnLetter = char.ToUpper(scrabbleTileNotation[0]);
        var columnIndex = (short)(columnLetter - 65); // 65 represents letter 'A' in ASCII
        var rowIndex = Convert.ToInt16(scrabbleTileNotation[1..]);

        return (columnIndex, --rowIndex);
    }

    public static bool IsValidTileNotation(string notation)
    {
        var columnLetter = char.ToUpper(notation[0]);

        if (!char.IsAsciiLetterUpper(columnLetter))
        {
            return false;
        }

        return int.TryParse(notation[1..], out _);
    }
    
    public static List<(short, short)> GetLetterPath(short x1, short y1, short x2, short y2)
    {
        if (x1 == x2)
        {
            return GetLetterPath(x1, y1, y2);
        }

        if (y1 == y2)
        {
            return GetLetterPath(y1, x1, x2);
        }

        throw new Exception("Unable to determine letter path from coords.");
    }

    private static List<(short, short)> GetLetterPath(short origin, short start, short end)
    {
        var path = new List<(short, short)>();

        for (var i = start; i <= end; i++)
        {
            path.Add((origin, i));
        }

        return path;
    }
}