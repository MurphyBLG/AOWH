using Newtonsoft.Json;

public class AuthorizationResponse
{
    public InterfaceAccesses Accesses { get; init; } = null!;
    public IEnumerable<StockDTO> Stocks { get; init; } = null!;
    public string Token { get; init; } = null!;
    [JsonIgnore]
    public RefreshToken refreshToken { get; init; } = null!;

    public AuthorizationResponse(InterfaceAccesses accesses,
                                 IEnumerable<StockDTO> stocks, 
                                 string token, 
                                 RefreshToken refreshToken)
    {
        Accesses = accesses;
        Stocks = stocks;
        Token = token;
        this.refreshToken = refreshToken;
    }
}