using Alexicon.API.Domain.Models;
using Alexicon.API.Domain.Representations.Games;
using Alexicon.API.SecondaryPorts.DTOs;
using Alexicon.Helpers;
using LanguageExt;

namespace Alexicon.API.Domain.Services.Mappers;

public class GameDtoToGameRepresentationMapper : IAlexiconMapper<Game, GameRepresentation>
{
    public GameRepresentation Map(Game src)
    {
        var rep = new GameRepresentation
        {
            State = MapGameState(src.MovesPlayed),
            Players = MapGamePlayers(src)
        };

        return rep;
    }

    private static GameStateRepresentation MapGameState(IReadOnlyCollection<GameMove> movesPlayed)
    {
        var gameState = new GameStateRepresentation
        {
            MovesPlayed = new List<GameMoveRepresentation>(),
            Board = new byte[15, 15],
            Bag = Scrabble.StartingBag.ToList()
        };

        foreach (var move in movesPlayed)
        {
            MapGameMove(move, gameState);
        }

        return gameState;
    }

    private static void MapGameMove(GameMove move, GameStateRepresentation gameState)
    {
        var firstTileNotation = ScrabbleHelper.MapToScrabbleTileNotation(move.FirstLetterX, move.FirstLetterY);
        var lastTileNotation = ScrabbleHelper.MapToScrabbleTileNotation(move.LastLetterX, move.LastLetterY);

        var rep = new GameMoveRepresentation
        {
            PlayerUsername = move.Player.Player.Username,
            LettersUsed = move.LettersUsed.ToList(),
            WordsCreated = move.WordsCreated.ToList(),
            Location = new Tuple<string, string>(firstTileNotation, lastTileNotation),
            Score = move.Score
        };

        gameState.MovesPlayed.Add(rep);

        UpdateGameBoard(move, gameState);

        RemoveUsedTiles(move, gameState);
    }

    private static void UpdateGameBoard(GameMove move, GameStateRepresentation gameState)
    {
        var letterPath = GetLetterPath(move.FirstLetterX, move.FirstLetterY, move.LastLetterX, move.LastLetterY);
        var lettersArray = move.LettersUsed.ToArray();
        var letterPointer = -1;

        foreach (var currentSlot in letterPath)
        {
            if (gameState.Board[currentSlot.Item2, currentSlot.Item1] == 0)
            {
                gameState.Board[currentSlot.Item2, currentSlot.Item1] = (byte)lettersArray[++letterPointer];
            }
        }
    }

    private static void RemoveUsedTiles(GameMove move, GameStateRepresentation gameState)
    {
        foreach (var letter in move.LettersUsed)
        {
            gameState.Bag.Remove(letter);
        }
    }

    private static Dictionary<string,GamePlayerRepresentation> MapGamePlayers(Game game)
    {
        var dict = new Dictionary<string, GamePlayerRepresentation>();

        foreach (var gamePlayer in game.Players)
        {
            var playerRep = new GamePlayerRepresentation
            {
                Player = new PlayerRepresentation
                {
                    Username = gamePlayer.Player.Username,
                    DisplayName = gamePlayer.Player.DisplayName
                },
                CurrentRack = gamePlayer.CurrentRack.ToList(),
                CurrentScore = 0
            };

            foreach (var move in game.MovesPlayed.Where(gm => string.Equals(gm.Player.Player.Username, gamePlayer.Player.Username, StringComparison.OrdinalIgnoreCase)))
            {
                playerRep.CurrentScore += move.Score;
            }

            dict.Add(gamePlayer.Player.Username, playerRep);
        }

        return dict;
    }

    private static List<Tuple<short, short>> GetLetterPath(short x1, short y1, short x2, short y2)
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

    private static List<Tuple<short, short>> GetLetterPath(short origin, short start, short end)
    {
        var path = new List<Tuple<short, short>>();

        for (var i = start; i <= end; i++)
        {
            path.Add(new Tuple<short, short>(origin, i));
        }

        return path;
    }
}