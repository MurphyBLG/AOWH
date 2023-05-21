using Microsoft.EntityFrameworkCore;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AccountingContext _context;

    public EmployeeRepository(AccountingContext context)
    {
        _context = context;
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidDataException();
        }
    }

    public async Task<IEnumerable<AllEmployeesResponse>> GetAllEmployees(IStockRepository stockRepo)
    {
        var employees = await _context.Employees.AsNoTracking()
            .Select(e => new
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Surname = e.Surname,
                Patronymic = e.Patronymic,
                Stocks = e.Stocks,
                Link = e.Link
            }).ToListAsync();

        var employeesResponse = employees
            .Select(e => new AllEmployeesResponse
            {
                EmployeeId = e.EmployeeId,
                Name = e.Name,
                Surname = e.Surname,
                Patronymic = e.Patronymic,
                Stocks = stockRepo.GetAllStocksNameByEmployee(e.Stocks),
                Link = e.Link
            });

        return employeesResponse;
    }

    public async Task<Employee?> GetEmployeeById(Guid id)
    {
        Employee? employee = await _context.Employees.FindAsync(id);

        if (employee != null)
            await _context.Entry(employee).Reference(e => e.Position).LoadAsync();

        return employee;
    }

    public async Task<Employee?> GetEmployeeByPassword(int password)
    {
        Employee? employee = await _context.Employees.SingleOrDefaultAsync(e => e.Password == password);

        if (employee == null)
        {
            return null;
        }

        _context.Entry(employee).Reference(e => e.Position).Load();

        return employee;
    }

    public async Task AddEmployeeHistory(EmployeeHistory history)
    {   
        try
        {
            await _context.EmployeeHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidDataException();
        }
    }

    public async Task AddMark(Mark mark)
    {
        await _context.Marks.AddAsync(mark);
        await _context.SaveChangesAsync();
    }

    public async Task<Mark?> GetMarkByEmployeeId(Guid id)
    {
        Mark? mark = await _context.Marks.SingleOrDefaultAsync(m => m.EmployeeId == id);

        return mark;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Mark> GetMarksByStockId(int stockId)
    {
        IEnumerable<Mark> marks = _context.Marks.Where(m => m.StockId == stockId)
                                                .Include(m => m.Employee)
                                                .ToList();

        return marks;
    }

    public void DeleteMarks(IEnumerable<Mark> marks)
    {
        _context.Marks.RemoveRange(marks);
    }

    public void DeleteMark(Mark mark)
    {
        _context.Marks.Remove(mark);
    }

    public IEnumerable<Guid> GetAllEmployeesId()
    {
        IEnumerable<Guid> ids = _context.Employees.AsNoTracking().Select(e => e.EmployeeId);

        return ids;
    }
}
