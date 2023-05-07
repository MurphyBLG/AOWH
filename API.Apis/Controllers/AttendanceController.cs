using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AttendanceController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public AttendanceController(IEmployeeService employeeService)
    {
        _employeeService = employeeService; 
    }

    [HttpGet]
    public IActionResult GetAttendance([FromBody] GetAttendanceRequest request)
    {
        List<GetAttendanceResponse> response = _employeeService.GetAttendance(request);

        return Ok(response);
    }
}