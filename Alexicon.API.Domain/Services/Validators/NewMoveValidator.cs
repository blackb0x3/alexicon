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
                result.Score += newWord.WordScore;
            }
        }

        result.FirstLetterX = path[0].Item1;
        result.FirstLetterY = path[0].Item2;
        result.LastLetterX = path[^1].Item1;
        result.LastLetterY = path[^1].Item2;

        return result;

    }

    private List<WordCreated> GetNewWords(List<char> letters, List<(short, short)> path, byte[,] board)
    {
        var wordsCreated = new List<WordCreated>();
        var primaryWord = BuildWordFromPath(path, letters, board);
        wordsCreated.Add(primaryWord);
        var primaryWordIsVertical = path.All(node => node.Item1 == path.First().Item1);

        // Build secondary words for each new tile
        foreach (var (x, y) in primaryWord.NewUsedTiles)
        {
            var secondaryWord = BuildSecondaryWord(x, y, board, primaryWord.NewUsedTiles, primaryWordIsVertical);
            if (secondaryWord != null)
            {
                wordsCreated.Add(secondaryWord);
            }
        }

        return wordsCreated;
    }

    private WordCreated BuildWordFromPath(List<(short, short)> path, List<char> letters, byte[,] board)
    {
        short wordScore = 0;
        var word = new char[path.Count];
        var letterIndex = 0;
        var newUsedTiles = new List<(short, short)>();

        for (var i = 0; i < path.Count; i++)
        {
            var (x, y) = path[i];

            if (board[y, x] != 0) // Existing tile
            {
                word[i] = (char)board[y, x];
            }
            else // New tile placed during this move
            {
                word[i] = letters[letterIndex++];
                newUsedTiles.Add((x, y));
                board[y, x] = (byte)word[i]; // Update the board with the new tile

                // Apply letter multiplier
                if (Scrabble.LetterMultiplierLocations.TryGetValue((x, y), out var mult))
                {
                    wordScore += Convert.ToInt16(Scrabble.LetterScores[word[i]] * mult);
                }
                else
                {
                    wordScore += Scrabble.LetterScores[word[i]];
                }
            }
        }

        // Apply word multipliers for new tiles
        foreach (var tile in newUsedTiles)
        {
            if (Scrabble.WordMultiplierLocations.TryGetValue(tile, out var mult))
            {
                wordScore *= mult;
            }
        }

        // Apply the 50-point bingo bonus for using all tiles
        if (letters.Count == 7)
        {
            wordScore += 50;
        }

        var isValidWord = IsValidWord(new string(word));

        return new WordCreated
        {
            Word = new string(word),
            IsValid = isValidWord,
            Reason = isValidWord ? string.Empty : "InvalidWord",
            WordScore = wordScore,
            NewUsedTiles = newUsedTiles
        };
    }

    private WordCreated? BuildSecondaryWord(short x, short y, byte[,] board, List<(short, short)> newUsedTiles, bool primaryWordIsVertical)
    {
        var secondaryWord = primaryWordIsVertical
            ? BuildWordInDirection(x, y, board, -1, 0, 1, 0, newUsedTiles) // Left and right
            : BuildWordInDirection(x, y, board, 0, -1, 0, 1, newUsedTiles); // Up and down

        // Return the valid word (if any)
        if (secondaryWord != null && secondaryWord.Word.Length > 1)
        {
            return secondaryWord;
        }

        return null;
    }

    private WordCreated? BuildWordInDirection(short startX, short startY, byte[,] board, short dx1, short dy1, short dx2, short dy2, List<(short, short)> newUsedTiles)
    {
        var word = new List<char>();
        short wordScore = 0;
        var hasNewTile = false;

        // Move in the first direction (e.g. left or up)
        var x = startX;
        var y = startY;

        while (x >= 0 && y >= 0 && x < Scrabble.BoardSize && y < Scrabble.BoardSize && board[y, x] != 0)
        {
            word.Insert(0, (char)board[y, x]);
            if (newUsedTiles.Contains((x, y)))
            {
                hasNewTile = true;

                // Apply letter multiplier
                if (Scrabble.LetterMultiplierLocations.TryGetValue((x, y), out var mult))
                {
                    wordScore += Convert.ToInt16(Scrabble.LetterScores[(char)board[y, x]] * mult);
                }
                else
                {
                    wordScore += Scrabble.LetterScores[(char)board[y, x]];
                }
            }
            else
            {
                wordScore += Scrabble.LetterScores[(char)board[y, x]];
            }

            x += dx1;
            y += dy1;
        }

        // Reset to move in the second direction (e.g. right or down)
        x = startX;
        y = startY;

        while (x >= 0 && y >= 0 && x < Scrabble.BoardSize && y < Scrabble.BoardSize && board[y, x] != 0)
        {
            if (!newUsedTiles.Contains((x, y)) || x != startX || y != startY) // Avoid duplicate scoring
            {
                word.Add((char)board[y, x]);
                wordScore += Scrabble.LetterScores[(char)board[y, x]];
            }

            x += dx2;
            y += dy2;
        }

        if (word.Count < 2 || !hasNewTile)
        {
            return null;
        }

        // Apply word multipliers for new tiles
        foreach (var tile in newUsedTiles)
        {
            if (Scrabble.WordMultiplierLocations.TryGetValue(tile, out var mult))
            {
                wordScore *= mult;
            }
        }

        var wordString = new string(word.ToArray());
        var isValid = IsValidWord(wordString);

        return new WordCreated
        {
            Word = wordString,
            IsValid = isValid,
            Reason = isValid ? string.Empty : "InvalidWord",
            WordScore = wordScore
        };
    }

    private bool IsValidWord(string word)
    {
        // TODO
        // add word validation flag to game entity
        // if flag is not set return false
        // otherwise, send ValidateWordQuery and return the result
        return true;
    }

    private static HashSet<(short, short)> GetIntersectableTileIndices(byte[,] board)
    {
        var tileIndices = new HashSet<(short, short)>();

        var hasTiles = false;

        for (short i = 0; i < Scrabble.BoardSize; i++)
        {
            for (short j = 0; j < Scrabble.BoardSize; j++)
            {
                if (board[i, j] != 0)
                {
                    hasTiles = true;

                    // Add adjacent empty tiles with boundary checks
                    if (i + 1 < Scrabble.BoardSize && board[i + 1, j] == 0)
                    {
                        tileIndices.Add(((short)(i + 1), j));
                    }

                    if (i - 1 >= 0 && board[i - 1, j] == 0)
                    {
                        tileIndices.Add(((short)(i - 1), j));
                    }

                    if (j + 1 < Scrabble.BoardSize && board[i, j + 1] == 0)
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