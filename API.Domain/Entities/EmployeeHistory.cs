public partial class EmployeeHistory
{
    public Guid? EmployeeHistoryId { get; set; }

    public Guid EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string PassportNumber { get; set; } = null!;

    public string? PassportIssuer { get; set; }

    public DateOnly PassportIssueDate { get; set; }

    public DateOnly StartOfTotalSeniority { get; set; }

    public DateOnly StartOfLuchSeniority { get; set; }

    public DateOnly? DateOfTermination { get; set; }

    public Guid? PositionId { get; set; }

    public DateOnly? StartDateOfWorkInCurrentPosition { get; set; }

    public DateOnly? EndDateOfWorkInCurrentPosition { get; set; }

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }

    public string? Link { get; set; }

    public DateOnly? StartDateOfWorkInCurrentLink { get; set; }

    public DateOnly? EndDateOfWorkInCurrentLink { get; set; }

    public string? Stocks { get; set; }

    public DateOnly? StartDateOfWorkIncurrentStock { get; set; }

    public DateOnly? EndDateOfWorkInCurrentStock { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    public DateTime DateOfCreation { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Position? Position { get; set; }

    public EmployeeHistory() { }

    public EmployeeHistory(Employee currentEmployee,
                           Guid? positionId,
                           string? stocks,
                           string? link)
    {
        EmployeeHistoryId = Guid.NewGuid();
        EmployeeId = currentEmployee.EmployeeId;
        Name = currentEmployee.Name;
        Surname = currentEmployee.Surname;
        Patronymic = currentEmployee.Patronymic;
        Birthday = currentEmployee.Birthday;
        PassportNumber = currentEmployee.PassportNumber;
        PassportIssuer = currentEmployee.PassportIssuer;
        PassportIssueDate = currentEmployee.PassportIssueDate;
        StartOfTotalSeniority = currentEmployee.StartOfTotalSeniority;
        StartOfLuchSeniority = currentEmployee.StartOfLuchSeniority;
        DateOfTermination = currentEmployee.DateOfTermination;
        PositionId = currentEmployee.PositionId;
        StartDateOfWorkInCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition;
        Salary = currentEmployee.Salary;
        QuarterlyBonus = currentEmployee.QuarterlyBonus;
        PercentageOfSalaryInAdvance = currentEmployee.PercentageOfSalaryInAdvance;
        Link = currentEmployee.Link;
        StartDateOfWorkInCurrentLink = currentEmployee.DateOfStartInTheCurrentLink;
        Stocks = currentEmployee.Stocks;
        StartDateOfWorkIncurrentStock = currentEmployee.DateOfStartInTheCurrentStock;
        ForkliftControl = currentEmployee.ForkliftControl;
        RolleyesControl = currentEmployee.RolleyesControl;
        DateOfCreation = DateTime.UtcNow;

        if (currentEmployee.Stocks != stocks)
            EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
        
        if (currentEmployee.Link != link)
            EndDateOfWorkInCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);

        if (currentEmployee.PositionId != positionId)
            EndDateOfWorkInCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public EmployeeHistory(Employee currentEmployee)
    {
        EmployeeHistoryId = Guid.NewGuid();
        EmployeeId = currentEmployee.EmployeeId;
        Name = currentEmployee.Name;
        Surname = currentEmployee.Surname;
        Patronymic = currentEmployee.Patronymic;
        Birthday = currentEmployee.Birthday;
        PassportNumber = currentEmployee.PassportNumber;
        PassportIssuer = currentEmployee.PassportIssuer;
        PassportIssueDate = currentEmployee.PassportIssueDate;
        StartOfTotalSeniority = currentEmployee.StartOfTotalSeniority;
        StartOfLuchSeniority = currentEmployee.StartOfLuchSeniority;
        DateOfTermination = currentEmployee.DateOfTermination;
        PositionId = currentEmployee.PositionId;
        StartDateOfWorkInCurrentPosition = currentEmployee.DateOfStartInTheCurrentPosition;
        Salary = currentEmployee.Salary;
        QuarterlyBonus = currentEmployee.QuarterlyBonus;
        PercentageOfSalaryInAdvance = currentEmployee.PercentageOfSalaryInAdvance;
        Link = currentEmployee.Link;
        StartDateOfWorkInCurrentLink = currentEmployee.DateOfStartInTheCurrentLink;
        Stocks = currentEmployee.Stocks;
        StartDateOfWorkIncurrentStock = currentEmployee.DateOfStartInTheCurrentStock;
        ForkliftControl = currentEmployee.ForkliftControl;
        RolleyesControl = currentEmployee.RolleyesControl;
        DateOfCreation = DateTime.UtcNow;
        EndDateOfWorkInCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
        EndDateOfWorkInCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);
        EndDateOfWorkInCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}
