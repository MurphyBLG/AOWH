using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PositionController : ControllerBase
{
    private readonly IPositionService _positionService;

    public PositionController(IPositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePosition([FromBody] CreatePositionRequest request)
    {
        try
        {
            await _positionService.CreatePosition(request);

            return Ok();
        }
        catch(InvalidDataException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet("{PositionId}")]
    public async Task<IActionResult> GetPosition(string positionId)
    {
        try
        {
            GetPositionResponse result = await _positionService.GetPosition(new Guid(positionId));

            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet]
    public IActionResult GetAllPositions()
    {
        IEnumerable<GetAllPositionResponse> result = _positionService.GetAllPositions();

        return Ok(result);
    }

    [HttpPut] 
    public async Task<IActionResult> UpdatePosition([FromBody] UpdatePositionRequest request)
    {
        try
        {
            await _positionService.UpdatePosition(request);

            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(InvalidDataException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
