namespace SuperMovie.Util;

using BCrypt.Net;

public class Hasher
{
    public string GetHash(string text)
        => BCrypt.HashPassword(text);

    public bool TextIsHash(string text, string hash)
        => BCrypt.Verify(text, hash);
}