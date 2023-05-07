using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegistrationRequest request)
    {
        try
        {
            RegistrationResponse? result = await _employeeService.RegisterEmployee(request);

            return Ok(result);
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

    [HttpGet("{EmployeeId}")]
    public async Task<IActionResult> GetEmployee(string employeeId)
    {
        try
        {
            GetEmployeeResponse result = await _employeeService.GetEmployee(new Guid(employeeId));

            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEmployees()
    {
        IEnumerable<AllEmployeesResponse> result = await _employeeService.GetAllEmployee();

        return Ok(result);
    }

    [HttpPut("{EmployeeId}")]
    public async Task<IActionResult> UpdateEmployee(string employeeId, [FromBody] UpdateEmployeeRequest request)
    {
        try
        {
            UpdateEmployeeResponse result = await _employeeService.UpdateEmployee(new Guid(employeeId), request);

            return Ok(result);
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

    [HttpDelete("{EmployeeId}")]
    public async Task<IActionResult> FireEmployee(string employeeId, [FromBody] FireEmployeeRequest request)
    {
        try
        {
            await _employeeService.FireEmployee(new Guid(employeeId), request);

            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}