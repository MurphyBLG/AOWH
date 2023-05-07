using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AccountingController : ControllerBase
{
    private readonly IAccountingService _accountingService;

    public AccountingController(IAccountingService accountingService)
    {
        _accountingService = accountingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAccounting([FromQuery] int year, [FromQuery] int month, [FromQuery] int stockId)
    {
        List<GetAccountingResponse> response = await _accountingService.GetAccounting(year, month, stockId);

        return Ok(response);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAccountings([FromBody] List<UpdateAccountingRequest> request)
    {
        try
        {
            await _accountingService.UpdateAccountings(User, request);

            return Ok();
        }
        catch(NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(UnauthorizedAccessException ex)
        {
            return Unauthorized(ex);
        }
    }
}