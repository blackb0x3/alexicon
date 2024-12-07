using Alexicon.API.Domain.Models;
using Alexicon.API.Domain.PrimaryPorts.ApplyMove;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.Helpers;
using MapsterMapper;

namespace Alexicon.API.Domain.Services.Validators;

public interface INewMoveValidator
{
    MoveValidationResult ValidateMove(ApplyMoveRequest request, GameRepresentation gameRep);
}

public class NewMoveValidator : INewMoveValidator
{
    public MoveValidationResult ValidateMove(ApplyMoveRequest request, GameRepresentation gameRep)
    {
        var result = new MoveValidationResult();
        var intersectableTileLocations = GetIntersectableTileIndices(gameRep.State.Board);
        var path = ScrabbleHelper.GetLetterPath(request.Location);

        if (!path.Intersect(intersectableTileLocations).Any())
        {
            result.WordsCreated.Add(new WordCreated
            {
                Word = string.Join(string.Empty, request.LettersUsed),
                IsValid = false,
                Reason = "InvalidLocation"
            });
        }
        else
        {
            var wordsCreated = GetNewWords(request.LettersUsed, path, gameRep.State.Board);

            foreach (var newWord in wordsCreated)
            {
                result.WordsCreated.Add(newWord);
            }
        }

        return result;

    }

    private List<WordCreated> GetNewWords(List<char> letters, List<(short, short)> path, byte[,] board)
    {
        var wordsCreated = new List<WordCreated>();
        var primaryWord = BuildWordFromPath(path, letters, board);
        wordsCreated.Add(primaryWord);

        // Check for secondary words formed by letters in orthogonal directions
        foreach (var (x, y) in path)
        {
            var secondaryWord = BuildSecondaryWord(x, y, board);
            if (secondaryWord != null)
            {
                wordsCreated.Add(secondaryWord);
            }
        }

        return wordsCreated;
    }

    private WordCreated BuildWordFromPath(List<(short, short)> path, List<char> letters, byte[,] board)
    {
        var word = new char[path.Count];
        var letterIndex = 0;

        for (var i = 0; i < path.Count; i++)
        {
            var (x, y) = path[i];

            // when space is already being occupied
            if (board[y, x] != 0)
            {
                word[i] = (char)board[y, x];
            }

            // when space is empty
            else
            {
                word[i] = letters[letterIndex++];
            }
        }

        return new WordCreated
        {
            Word = new string(word),
            IsValid = IsValidWord(new string(word)), // Assume ValidateWord is implemented elsewhere
            Reason = IsValidWord(new string(word)) ? null : "InvalidWord"
        };
    }

    private WordCreated? BuildSecondaryWord(short x, short y, byte[,] board)
    {
        var horizontalWord = BuildWordInDirection(x, y, board, 0, -1, 0, 1); // Left and right
        var verticalWord = BuildWordInDirection(x, y, board, -1, 0, 1, 0); // Up and down

        if (horizontalWord != null && horizontalWord.Word.Length > 1)
        {
            return horizontalWord;
        }

        if (verticalWord != null && verticalWord.Word.Length > 1)
        {
            return verticalWord;
        }

        return null;
    }

    private WordCreated? BuildWordInDirection(short startX, short startY, byte[,] board, short dx1, short dy1, short dx2, short dy2)
    {
        var word = new List<char>();
        var (x, y) = (startX, startY);

        // Move in the first direction
        while (x >= 0 && y >= 0 && x < board.GetLength(0) && y < board.GetLength(1) && board[x, y] != 0)
        {
            word.Insert(0, (char)board[x, y]);
            x += dx1;
            y += dy1;
        }

        // Reset to the original position and move in the second direction
        x = startX;
        y = startY;

        while (x >= 0 && y >= 0 && x < board.GetLength(0) && y < board.GetLength(1) && board[x, y] != 0)
        {
            if ((x != startX || y != startY)) // Avoid adding the center tile twice
            {
                word.Add((char)board[x, y]);
            }

            x += dx2;
            y += dy2;
        }

        if (word.Count > 1)
        {
            var wordString = new string(word.ToArray());
            return new WordCreated
            {
                Word = wordString,
                IsValid = IsValidWord(wordString),
                Reason = IsValidWord(wordString) ? string.Empty : "InvalidWord"
            };
        }

        return null;
    }

    private bool IsValidWord(string word)
    {
        // TODO Implement the dictionary-based validation here
        return true;
    }

    private static HashSet<(short, short)> GetIntersectableTileIndices(byte[,] board)
    {
        var tileIndices = new HashSet<(short, short)>();
        var boardSize = board.GetLength(0); // Assume the Scrabble board is square

        var hasTiles = false;

        for (short i = 0; i < boardSize; i++)
        {
            for (short j = 0; j < boardSize; j++)
            {
                if (board[i, j] != 0)
                {
                    hasTiles = true;

                    // Add adjacent empty tiles with boundary checks
                    if (i + 1 < boardSize && board[i + 1, j] == 0)
                    {
                        tileIndices.Add(((short)(i + 1), j));
                    }

                    if (i - 1 >= 0 && board[i - 1, j] == 0)
                    {
                        tileIndices.Add(((short)(i - 1), j));
                    }

                    if (j + 1 < boardSize && board[i, j + 1] == 0)
                    {
                        tileIndices.Add((i, (short)(j + 1)));
                    }

                    if (j - 1 >= 0 && board[i, j - 1] == 0)
                    {
                        tileIndices.Add((i, (short)(j - 1)));
                    }
                }
            }
        }

        // If no tiles exist on the board, add the center tile
        if (!hasTiles)
        {
            tileIndices.Add((7, 7)); // Center of a 15x15 board
        }

        return tileIndices;
    }
}