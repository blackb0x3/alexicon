using Alexicon.API.Domain.Models;
using Alexicon.API.Domain.Services.Mappers;
using Alexicon.API.SecondaryPorts.DTOs;

namespace Alexicon.API.Tests.Mappers;

[TestFixture]
public class A_GameDto_To_GameRepresentation_Mapper
{
    public class Should_Map_Game_To_GameRepresentation
    {
        [Test]
        public void When_Game_Has_Valid_Properties()
        {
            // Arrange
            var gameId = Guid.NewGuid();

            var game = new Game
            {
                Id = gameId,
                Players = new List<GamePlayer>
                {
                    new GamePlayer
                    {
                        Player = new Player { Username = "player1", DisplayName = "Player One" },
                        CurrentRack = new List<char> { 'A', 'B', 'C' }
                    },
                    new GamePlayer
                    {
                        Player = new Player { Username = "player2", DisplayName = "Player Two" },
                        CurrentRack = new List<char> { 'X', 'Y', 'Z' }
                    }
                },
                MovesPlayed = new List<GameMove>
                {
                    new GameMove
                    {
                        Player = new GamePlayer { Player = new Player { Username = "player1" } },
                        LettersUsed = new List<char> { 'A', 'B' },
                        WordsCreated = new List<string> { "AB" },
                        FirstLetterX = 0,
                        FirstLetterY = 0,
                        LastLetterX = 0,
                        LastLetterY = 1,
                        Score = 10
                    }
                }
            };

            var mapper = new GameDtoToGameRepresentationMapper();

            // Act
            var result = mapper.Map(game);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(gameId);

            result.Players.Should().ContainKey("player1");
            result.Players["player1"].Player.Username.Should().Be("player1");
            result.Players["player1"].CurrentRack.Should().BeEquivalentTo(new List<char> { 'A', 'B', 'C' });

            result.State.Board[0, 0].Should().Be((byte)'A');
            result.State.Board[1, 0].Should().Be((byte)'B');
            result.State.MovesPlayed.Should().HaveCount(1);
            result.State.MovesPlayed.First().Score.Should().Be(10);
        }
    }

    public class Should_Handle_Edge_Cases
    {
        [Test]
        public void When_Game_Has_No_Players_Or_Moves()
        {
            // Arrange
            var gameId = Guid.NewGuid();

            var game = new Game
            {
                Id = gameId,
                Players = new List<GamePlayer>(),
                MovesPlayed = new List<GameMove>()
            };

            var mapper = new GameDtoToGameRepresentationMapper();

            // Act
            var result = mapper.Map(game);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(gameId);
            result.Players.Should().BeEmpty();
            result.State.MovesPlayed.Should().BeEmpty();
            result.State.Board.Cast<byte>().Should().AllBeEquivalentTo(0);
            result.State.Bag.Should().BeEquivalentTo(Scrabble.StartingBag);
        }

        [Test]
        public void When_Game_Has_Inconsistent_Rack_And_Bag()
        {
            // Arrange
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Players = new List<GamePlayer>
                {
                    new GamePlayer
                    {
                        Player = new Player { Username = "player1" },
                        CurrentRack = new List<char> { 'A', 'A', 'A', 'B', 'C', 'D', 'E' }
                    }
                },
                MovesPlayed = new List<GameMove>()
            };

            var mapper = new GameDtoToGameRepresentationMapper();

            // Act
            var result = mapper.Map(game);

            // Assert
            result.Should().NotBeNull();

            var expectedNumberOfAs = Scrabble.BagCount['A'] - 3;
            var expectedNumberOfBs = Scrabble.BagCount['B'] - 1;
            var expectedNumberOfCs = Scrabble.BagCount['C'] - 1;
            var expectedNumberOfDs = Scrabble.BagCount['D'] - 1;
            var expectedNumberOfEs = Scrabble.BagCount['E'] - 1;
            result.State.Bag.Where(t => t == 'A').Should().HaveCount(expectedNumberOfAs);
            result.State.Bag.Where(t => t == 'B').Should().HaveCount(expectedNumberOfBs);
            result.State.Bag.Where(t => t == 'C').Should().HaveCount(expectedNumberOfCs);
            result.State.Bag.Where(t => t == 'D').Should().HaveCount(expectedNumberOfDs);
            result.State.Bag.Where(t => t == 'E').Should().HaveCount(expectedNumberOfEs);
        }
    }
}