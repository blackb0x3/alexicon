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
        throw new NotImplementedException();
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