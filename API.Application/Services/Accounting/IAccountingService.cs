using System.Security.Claims;

public interface IAccountingService
{
    Task<List<GetAccountingResponse>> GetAccounting(int year, int month, int stockId);
    Task UpdateAccountings(ClaimsPrincipal user, List<UpdateAccountingRequest> request);
}