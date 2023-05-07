public class UpdateEmployeeRequest
{
    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public int Password { get; init; }

    public string Patronymic { get; init; } = null!;

    public DateTime Birthday { get; init; }

    public string PassportNumber { get; init; } = null!;

    public string? PassportIssuer { get; init; }

    public DateTime PassportIssueDate { get; init; }

    public DateTime StartOfTotalSeniority { get; init; }

    public DateTime StartOfLuchSeniority { get; init; }

    public DateTime? DateOfTermination { get; init; }

    public Guid? PositionId { get; init; }

    public string? Link { get; init; }

    public string? Stocks { get; init; }

    public bool ForkliftControl { get; init; }

    public bool RolleyesControl { get; init; }

    public decimal Salary { get; init; }

    public int PercentageOfSalaryInAdvance { get; init; }
    
    public decimal QuarterlyBonus { get; init; }

    public DateTime? DateOfStartInTheCurrentPosition { get; init; }

    public DateTime? DateOfStartInTheCurrentStock { get; init; }

    public DateTime? DateOfStartInTheCurrentLink { get; init; }
}
