using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ShiftController : ControllerBase
{
    private readonly IShiftService _shiftService;

    public ShiftController(IShiftService shiftService)
    {
        this._shiftService = shiftService;
    }

    [HttpPost]
    [Route("open")]
    public async Task<IActionResult> OpenShift([FromBody] OpenShiftRequest request)
    {
        try
        {
            Guid result = await _shiftService.OpenShift(User, request);
    
            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("close")]
    public async Task<IActionResult> CloseShift([FromBody] CloseShiftRequest request)
    {
        try
        {
            await _shiftService.CloseShift(request);
    
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return Ok(ex.Message);
        }
    }

    [HttpGet]
    [Route("get/{stockId:int}")]
    public async Task<IActionResult> GetCurrentShift(int stockId)
    {
        try
        {
            GetCurrentShiftResponse result = await _shiftService.GetCurrentShift(stockId);
    
            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{shiftId:Guid}")]
    public async Task<IActionResult> UpdateCurrentShift(Guid shiftId, [FromBody] UpdateShiftRequest request)
    {
        try
        {
            await _shiftService.UpdateCurrentShift(shiftId, request);
    
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{shiftInfoId:Guid}")]
    public async Task<IActionResult> UpdateShiftInfo(Guid shiftInfoId, [FromBody] UpdateShiftInfoRequest request)
    {
        try
        {
            await _shiftService.UpdateShiftInfo(shiftInfoId, request);
    
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}