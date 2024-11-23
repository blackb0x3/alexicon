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
}