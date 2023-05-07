using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LogInController : ControllerBase
{
    private readonly ILogInService _loginService;

    public LogInController(ILogInService loginService)
    {
        _loginService = loginService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> AuthorizeEmployee([FromBody] AuthorizationRequest request)
    {
        AuthorizationResponse response = await _loginService.AuthorizeEmployee(request);
        
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = response.refreshToken.Expires
        };
        Response.Cookies.Append("refreshToken", response.refreshToken.Token, cookieOptions);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string token)
    {
        try
        {
            string? refreshToken = Request.Cookies["refreshToken"];

            RefreshTokenResponse? result = await _loginService.RefreshToken(token);
        
            if (result is null)
                return NotFound("Employee is not found");

            if (result.OldToken != refreshToken)
                return Unauthorized("Invalid refresh token");

            if (result.TokenExpires < DateTime.Now)
                return Unauthorized("Token expired");

            return Ok(result.NewToken);
        }
        catch (NullReferenceException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}