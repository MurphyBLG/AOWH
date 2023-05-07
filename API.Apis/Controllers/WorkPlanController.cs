using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkPlanController : ControllerBase
{
    private readonly IWorkPlanService _workPlanService;

    public WorkPlanController(IWorkPlanService workPlanService)
    {
        _workPlanService = workPlanService;
    }

    [HttpPost]
    public async Task<IActionResult> AddWorkPlan([FromQuery] Guid employeeId, [FromQuery] int year, [FromQuery] int month,
                                                 [FromBody] AddWorkPlanRequest request)
    {
        try
        {
            await _workPlanService.AddWorkPlan(employeeId, year, month, request);

            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkPlanForEmployee([FromQuery] Guid employeeId, [FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            GetWorkPlanResponse result = await _workPlanService.GetWorkPlan( employeeId, year, month);

            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}