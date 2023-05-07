public class GetEmployeeResponse
{
    public Guid EmployeeId { get; init; }

    public string Name { get; init; } = null!;

    public int Password { get; init; }

    public string Surname { get; init; } = null!;

    public string Patronymic { get; init; } = null!;

    public DateOnly Birthday { get; init; }

    public string PassportNumber { get; init; } = null!;

    public string? PassportIssuer { get; init; }

    public DateOnly PassportIssueDate { get; init; } 

    public DateOnly StartOfTotalSeniority { get; init; } 

    public DateOnly StartOfLuchSeniority { get; init; }

    public DateOnly? DateOfTermination { get; init; }

    public PositionDTO Position { get; init; } = null!;

    public string? Link { get; init; }

    public IEnumerable<StockDTO>? Stocks { get; init; }

    public bool ForkliftControl { get; init; }

    public bool RolleyesControl { get; init; }

    public decimal Salary { get; init; }

    public decimal PercentageOfSalaryInAdvance { get; init; }

    public DateOnly? DateOfStartInTheCurrentPosition { get; init; }

    public DateOnly? DateOfStartInTheCurrentStock { get; init; }

    public DateOnly? DateOfStartInTheCurrentLink { get; init; }

    public GetEmployeeResponse() {}

    public GetEmployeeResponse(Employee employee, PositionDTO position, IEnumerable<StockDTO>? stocks)
    {
        Password = employee.Password;
        EmployeeId = employee.EmployeeId;
        Name = employee.Name;
        Surname = employee.Surname;
        Patronymic = employee.Patronymic;
        Birthday = employee.Birthday;
        PassportNumber = employee.PassportNumber;
        PassportIssuer = employee.PassportIssuer;
        PassportIssueDate = employee.PassportIssueDate;
        StartOfTotalSeniority = employee.StartOfTotalSeniority;
        StartOfLuchSeniority = employee.StartOfLuchSeniority;
        DateOfTermination = employee.DateOfTermination;
        Position = position;
        Link = employee.Link;
        Stocks = stocks;
        ForkliftControl = employee.ForkliftControl;
        RolleyesControl = employee.RolleyesControl;
        Salary = employee.Salary;
        PercentageOfSalaryInAdvance = employee.PercentageOfSalaryInAdvance;
        DateOfStartInTheCurrentPosition = employee.DateOfStartInTheCurrentPosition;
        DateOfStartInTheCurrentStock = employee.DateOfStartInTheCurrentStock;
        DateOfStartInTheCurrentLink = employee.DateOfStartInTheCurrentLink;
    }
}