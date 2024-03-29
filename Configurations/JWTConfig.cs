namespace Server.Configurations;

public class JWTConfig
{
    public string Secret { get; set; } = null!;

    public TimeSpan ExpiryTimeFrame { get; set; }
}