public class UpdateAccountingRequest
{
    public Guid EmployeeId { get; set; }

    public int Year { get; set; }

    public int Month { get; set; }

    public decimal Mentoring { get; set; }

    public decimal Teaching { get; set; }

    public decimal Bonus { get; set; }

    public decimal Vacation { get; set; }

    public decimal Advance { get; set; }
}