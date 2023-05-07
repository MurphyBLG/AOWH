public interface ILogInService
{
    Task<AuthorizationResponse> AuthorizeEmployee(AuthorizationRequest request);
    Task<RefreshTokenResponse?> RefreshToken(string token);
    
}