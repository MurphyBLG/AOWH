using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class LogInService : ILogInService
{
    private readonly IConfiguration _config;
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IStockRepository _stockRepo;
    private readonly IPositionRepository _positionRepo;
    private readonly ITokenBuilder _tokenBuilder;

    public LogInService(
        IConfiguration config,
        IEmployeeRepository employeeRepo,
        ITokenBuilder tokenBuilder,
        IStockRepository stockRepo,
        IPositionRepository positionRepo)
    {
        _config = config;
        _employeeRepo = employeeRepo;
        _tokenBuilder = tokenBuilder;
        _stockRepo = stockRepo;
        _positionRepo = positionRepo;
    }

    public async Task<AuthorizationResponse> AuthorizeEmployee(AuthorizationRequest request)
    {
        Employee? currentEmployee = await _employeeRepo.GetEmployeeByPassword(request.Password);

        if (currentEmployee == null)
            throw new Exception("Authentication error: There is no such user");

        string token = _tokenBuilder.BuildToken(currentEmployee.EmployeeId); 

        if (currentEmployee.PositionId == await _positionRepo.GetPositionIdByName("Грузчик")) 
        {
            await _employeeRepo.AddMark(new Mark
            {
                MarkId = new Guid(),
                EmployeeId = currentEmployee.EmployeeId,
                StockId = JsonConvert.DeserializeObject<List<int>>(currentEmployee.Stocks!)![0],
                MarkDate = DateTime.UtcNow
            });
        }

        RefreshToken refreshToken = _tokenBuilder.GenerateRefreshToken();
        currentEmployee.RefreshToken = refreshToken.Token;
        currentEmployee.RefreshTokenExpires = refreshToken.Expires;
        

        IEnumerable<StockDTO> stocks = _stockRepo.GetAllStocksNameByEmployee(currentEmployee.Stocks);

        InterfaceAccesses accesses = System.Text.Json.JsonSerializer.Deserialize<InterfaceAccesses>(currentEmployee.Position!.InterfaceAccesses)!;
        return new AuthorizationResponse(
            JsonConvert.DeserializeObject<InterfaceAccesses>(currentEmployee.Position!.InterfaceAccesses)!,
            stocks,
            token,
            refreshToken);
    }

    public async Task<RefreshTokenResponse?> RefreshToken(string oldToken)
    {
        var oldClaims = _tokenBuilder.GetPrincipalFroExpiredToken(oldToken, _config);
        Guid currentEmployeeId = new(oldClaims!.FindFirstValue("EmployeeId")!);
        Employee? currentEmployee = await _employeeRepo.GetEmployeeById(currentEmployeeId);
        
        if (currentEmployee is null)
            throw new NullReferenceException("Token not found");
    
        string newToken = _tokenBuilder.BuildToken(currentEmployee.EmployeeId);

        return new RefreshTokenResponse(
            currentEmployee.RefreshToken,
            newToken,
            currentEmployee.RefreshTokenExpires
        );
    }
}