public class AccountingEmployee 
{
    public Guid EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string PositionName { get; set; } = null!;

    public DateOnly Seniority { get; set; }

    public decimal Salary { get; set; }
}