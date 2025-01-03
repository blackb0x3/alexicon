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

        var parsedRow = int.TryParse(notation[1..], out var rowIndex);

        if (!parsedRow)
        {
            return false;
        }

        return rowIndex is >= 1 and <= 15;
    }

    public static List<(short, short)> GetLetterPath((string, string) location)
    {
        return GetLetterPath(location.Item1, location.Item2);
    }

    public static List<(short, short)> GetLetterPath(string firstLetterNotation, string lastLetterNotation)
    {
        var firstLetterLocation = MapToArrayIndex(firstLetterNotation);
        var lastLetterLocation = MapToArrayIndex(lastLetterNotation);

        return GetLetterPath(firstLetterLocation.x, firstLetterLocation.y, lastLetterLocation.x, lastLetterLocation.y);
    }

    public static List<(short, short)> GetLetterPath(short x1, short y1, short x2, short y2)
    {
        if (x1 == x2)
        {
            return GetLetterPath(x1, y1, y2, true);
        }

        if (y1 == y2)
        {
            return GetLetterPath(y1, x1, x2, false);
        }

        throw new Exception("Unable to determine letter path from coords.");
    }

    private static List<(short, short)> GetLetterPath(short origin, short start, short end, bool originFirst)
    {
        var path = new List<(short, short)>();

        for (var i = start; i <= end; i++)
        {
            path.Add(originFirst ? (origin, i) : (i, origin));
        }

        return path;
    }
}