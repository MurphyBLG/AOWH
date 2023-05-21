using Newtonsoft.Json;

public class WorkPlan
{
    public Guid WorkPlanId { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public Guid EmployeeId { get; set; }
    public int NumberOfDayShifts { get; set; }
    public int NumberOfHoursPerDayShift { get; set; }
    public int NumberOfNightShifts { get; set; }
    public int NumberOfHoursPerNightShift { get; set; }
    [JsonIgnore]
    public Employee? Employee { get; set; }
    [JsonIgnore]
    public virtual ICollection<Accounting> Accountings { get; } = new List<Accounting>();
}