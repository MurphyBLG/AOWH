public interface IEmployeeRepository : IRepository
{
    Task<Employee?> GetEmployeeById(Guid id);
    Task<Employee?> GetEmployeeByPassword(int password);
    Task<IEnumerable<AllEmployeesResponse>> GetAllEmployees(IStockRepository stockRepo);
    IEnumerable<Guid> GetAllEmployeesId();
    Task AddEmployeeAsync(Employee employee);
    Task AddEmployeeHistory(EmployeeHistory history);
    Task AddMark(Mark mark);
    Task<Mark?> GetMarkByEmployeeId(Guid id);
    IEnumerable<Mark> GetMarksByStockId(int stockId);
    void DeleteMarks(IEnumerable<Mark> marks);
    void DeleteMark(Mark mark);
}