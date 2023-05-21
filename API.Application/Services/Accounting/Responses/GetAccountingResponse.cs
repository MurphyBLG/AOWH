public class GetAccountingResponse
{
    public Guid EmployeeId { get; set; }
    public int OvertimeDay { get; set; }
    public int OvertimeNight { get; set; }
    public decimal SalaryForShift { get; set; }
    public decimal SalaryForHour { get; set; }
    public decimal Mentoring { get; set; }
    public decimal Seniority { get; set; }
    public decimal Teaching { get; set; }
    public decimal Bonus { get; set; }
    public decimal Vacation { get; set; }
    public decimal Earned { get; set; }
    public decimal Advance { get; set; }
    public decimal Penalties { get; set; }
    public decimal Sends { get; set; }
    public decimal Payment { get; set; }
}