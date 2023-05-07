public interface IEmployeeService
{
    Task<RegistrationResponse> RegisterEmployee(RegistrationRequest request);
    Task<GetEmployeeResponse> GetEmployee(Guid id);
    Task<IEnumerable<AllEmployeesResponse>> GetAllEmployee();
    Task<UpdateEmployeeResponse> UpdateEmployee(Guid id, UpdateEmployeeRequest request);
    Task FireEmployee(Guid id, FireEmployeeRequest request);
    List<GetAttendanceResponse> GetAttendance(GetAttendanceRequest request); 
    bool CheckStocks(List<int> stocks);
}