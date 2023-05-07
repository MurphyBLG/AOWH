using System.Security.Claims;
using Microsoft.Extensions.Configuration;

public interface ITokenBuilder
{
    public string BuildToken(Guid employeeId);
    public RefreshToken GenerateRefreshToken();
    public ClaimsPrincipal? GetPrincipalFroExpiredToken(string token, IConfiguration config);

}