public class RefreshTokenResponse
{
    public string OldToken { get; init; } = null!;
    public string NewToken { get; init; } = null!;
    public DateTime TokenExpires { get; init; }

    public RefreshTokenResponse(string oldToken, string newToken, DateTime tokenExpires)
    {
        OldToken = oldToken;
        NewToken = newToken;
        TokenExpires = tokenExpires;
    }
}
    