public class RegistrationResponse
{
    public Guid EmployeeId { get; init; }

    public int Password { get; init; }

    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public string Patronymic { get; init; } = null!;

    public DateOnly Birthday { get; init; }

    public string PassportNumber { get; init; } = null!;

    public string? PassportIssuer { get; init; }

    public DateOnly PassportIssueDate { get; init; }

    public DateOnly StartOfTotalSeniority { get; init; }

    public DateOnly StartOfLuchSeniority { get; init; }

    public DateOnly? DateOfTermination { get; init; }

    public DateOnly? DateOfStartInTheCurrentPosition { get; init; }

    public decimal Salary { get; init; }

    public decimal QuarterlyBonus { get; init; }

    public int PercentageOfSalaryInAdvance { get; init; }

    public string? Link { get; init; }

    public DateOnly? DateOfStartInTheCurrentLink { get; init; }

    public string? Stocks { get; init; }

    public DateOnly? DateOfStartInTheCurrentStock { get; init; }

    public bool ForkliftControl { get; init; }

    public bool RolleyesControl { get; init; }

    public virtual Position? Position { get; init; }

    public RegistrationResponse() { }
}