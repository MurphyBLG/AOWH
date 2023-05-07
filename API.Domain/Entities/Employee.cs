using Newtonsoft.Json;


public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public int Password { get; set; }

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

    [JsonIgnore]
    public Guid? PositionId { get; set; }

    public DateOnly? DateOfStartInTheCurrentPosition { get; set; }

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public int PercentageOfSalaryInAdvance { get; set; }

    public string? Link { get; set; }

    public DateOnly? DateOfStartInTheCurrentLink { get; set; }

    public string? Stocks { get; set; }

    public DateOnly? DateOfStartInTheCurrentStock { get; set; }

    public bool ForkliftControl { get; set; }

    public bool RolleyesControl { get; set; }

    [JsonIgnore]
    public virtual ICollection<EmployeeHistory> EmployeeHistories { get; } = new List<EmployeeHistory>();

    [JsonIgnore]
    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();

    [JsonIgnore]
    public virtual ICollection<Mark> Marks { get; } = new List<Mark>();

    [JsonIgnore]
    public virtual ICollection<WorkPlan> WorkPlans { get; } = new List<WorkPlan>();

    [JsonIgnore]
    public virtual ICollection<ShiftInfo> ShiftInfos { get; } = new List<ShiftInfo>();

    [JsonIgnore]
    public virtual ICollection<ShiftHistory> ShiftHistories { get; } = new List<ShiftHistory>();

    [JsonIgnore]
    public virtual ICollection<Accounting> Accountings { get; } = new List<Accounting>();

    public virtual Position? Position { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;
    
    [JsonIgnore]
    public DateTime RefreshTokenExpires { get; set; }  = DateTime.UtcNow;

    public Employee() { }

    public Employee(int password,
                    string name,
                    string surname,
                    string patronymic,
                    DateTime birthday,
                    string passportNumber,
                    string? passportIssuer,
                    DateTime passportIssueDate,
                    DateTime startOfTotalSeniority,
                    DateTime startOfLuchSeniority,
                    DateTime? dateOfTermination,
                    Guid? positionId,
                    decimal salary,
                    decimal quarterlyBonus,
                    int percentageOfSalaryInAdvance,
                    string? link,
                    string? stocks,
                    bool forkliftControl,
                    bool rolleyesControl,
                    DateTime? dateOfStartInTheCurrentPosition,
                    DateTime? dateOfStartInTheCurrentStock)
    {
        EmployeeId = Guid.NewGuid();
        Password = password;
        Name = name;
        Surname = surname;
        Patronymic = patronymic;
        Birthday = DateOnly.FromDateTime(birthday);
        PassportNumber = passportNumber;
        PassportIssuer = passportIssuer;
        PassportIssueDate = DateOnly.FromDateTime(passportIssueDate);
        StartOfTotalSeniority = DateOnly.FromDateTime(startOfTotalSeniority);
        StartOfLuchSeniority = DateOnly.FromDateTime(startOfLuchSeniority);
        DateOfTermination = (dateOfTermination == null) ?
            null : DateOnly.FromDateTime(DateTime.Parse(dateOfTermination.ToString()!));
        PositionId = positionId;
        Salary = salary;
        QuarterlyBonus = quarterlyBonus;
        PercentageOfSalaryInAdvance = percentageOfSalaryInAdvance;
        Link = link;
        DateOfStartInTheCurrentLink = (link == null) ?
            null : DateOnly.FromDateTime(DateTime.UtcNow);
        Stocks = stocks;
        ForkliftControl = forkliftControl;
        RolleyesControl = rolleyesControl;
        DateOfStartInTheCurrentPosition = (dateOfStartInTheCurrentPosition == null) ? 
            DateOnly.FromDateTime(DateTime.UtcNow) : DateOnly.FromDateTime(DateTime.Parse(dateOfStartInTheCurrentPosition.ToString()!)); // refactoring
        DateOfStartInTheCurrentStock = (dateOfStartInTheCurrentStock == null) ?
            null : DateOnly.FromDateTime(DateTime.Parse(dateOfStartInTheCurrentStock.ToString()!));
    }

    public void Update(string name,
                       string surname,
                       int password,
                       string patronymic,
                       DateTime birthday,
                       string passportNumber,
                       string? passportIssuer,
                       DateTime passportIssueDate,
                       DateTime startOfTotalSeniority,
                       DateTime startOfLuchSeniority,
                       DateTime? dateOfTermination,
                       Guid? positionId,
                       string? link,
                       string? stocks,
                       bool forkliftControl,
                       bool rolleyesControl,
                       decimal salary,
                       int percentageOfSalaryInAdvance,
                       decimal quarterlyBonus)
    {
        this.Name = name;
        this.Password = password;
        this.Surname = surname;
        this.Patronymic = patronymic;
        this.Birthday = DateOnly.FromDateTime(birthday);
        this.PassportNumber = passportNumber;
        this.PassportIssuer = passportIssuer;
        this.PassportIssueDate = DateOnly.FromDateTime(passportIssueDate);
        this.StartOfTotalSeniority = DateOnly.FromDateTime(startOfTotalSeniority);
        this.StartOfLuchSeniority = DateOnly.FromDateTime(startOfLuchSeniority);
        this.ForkliftControl = forkliftControl;
        this.RolleyesControl = rolleyesControl;
        this.Salary = salary;
        this.PercentageOfSalaryInAdvance = percentageOfSalaryInAdvance;
        this.QuarterlyBonus = quarterlyBonus;

        if (Stocks != stocks)
        {
            this.DateOfStartInTheCurrentStock = DateOnly.FromDateTime(DateTime.UtcNow);
            this.Stocks = stocks;
        }

        if (Link != link)
        {
            this.DateOfStartInTheCurrentLink = DateOnly.FromDateTime(DateTime.UtcNow);
            this.Link = link;
        }

        if (PositionId != positionId)
        {
            this.DateOfStartInTheCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
            this.PositionId = positionId;
            this.DateOfTermination = null;
        }
    }

    public void Fire(DateTime timeOfTermination, Guid? firedId)
    {
        this.DateOfStartInTheCurrentPosition = DateOnly.FromDateTime(DateTime.UtcNow);
        this.PositionId = firedId;
        this.DateOfTermination = DateOnly.FromDateTime(timeOfTermination);
        this.DateOfStartInTheCurrentStock = null;
        this.Stocks = "[]";
        this.DateOfStartInTheCurrentLink = null;
        this.Link = null;
        this.ForkliftControl = false;
        this.RolleyesControl = false;
    }
}
