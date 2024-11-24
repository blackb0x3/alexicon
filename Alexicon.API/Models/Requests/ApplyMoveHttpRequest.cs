namespace Alexicon.API.Models.Requests;

public class ApplyMoveHttpRequest
{
    public string Player { get; set; }

    public List<char> LettersUsed { get; set; }
    
    public (string, string) Location { get; set; }

    public List<char> NewRack { get; set; }
}