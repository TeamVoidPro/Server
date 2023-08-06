namespace Server.Models;

public class AuthResults
{
    public string Token { get; set; } = null!;
    
    public string RefreshToken { get; set; } = null!;
    
    public bool Result { get; set; }
    
    public IEnumerable<string>? Errors { get; set; }
}