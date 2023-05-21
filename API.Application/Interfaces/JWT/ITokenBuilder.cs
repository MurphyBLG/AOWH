using System.Security.Claims;
using Microsoft.Extensions.Configuration;

public interface ITokenBuilder
{
    public string BuildToken(Guid employeeId);
    public RefreshToken GenerateRefreshToken();
    public ClaimsPrincipal? GetPrincipalForExpiredToken(string token, IConfiguration config);

}