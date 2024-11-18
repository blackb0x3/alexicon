namespace Alexicon.API.Domain.PrimaryPorts.CreateGame;

public record NewPlayer(string Username, string DisplayName, List<char> StartingRack);