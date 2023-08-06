namespace server.Helpers;

public class IdGenerator
{
    private static readonly Random Random = new Random();
    
    public static string GenerateId(string prefix)
    {
        prefix += "_";
        prefix += GenerateRandomDigits();
        prefix += "_";
        prefix += GenerateRandomDigits();

        return prefix;
    }
    
    private static string GenerateRandomDigits()
    {
        var randomNumber = Random.Next(1000, 9999);
        return randomNumber.ToString();
    }
    
    public static string GenerateRandomId(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz_";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}