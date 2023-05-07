using Newtonsoft.Json;

public class Accounting
{
    public Guid EmployeeId { get; set; }

    [JsonIgnore]
    public int Month { get; set; }

    [JsonIgnore]
    public int Year { get; set; }

    [JsonIgnore]
    public Guid WorkPlanId { get; set; }

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

    [JsonIgnore]
    public Employee Employee { get; set; } = null!;

    [JsonIgnore]
    public WorkPlan WorkPlan { get; set; } = null!;

    public Accounting() { }

    public Accounting(Guid employeeId,
                      int month,
                      int year,
                      Guid workPlanId,
                      decimal advance)
    {
        EmployeeId = employeeId;
        Month = month;
        Year = year;
        WorkPlanId = workPlanId;
        Advance = advance;
    }

    public void Update(decimal mentoring,
                       decimal teaching,
                       decimal bonus,
                       decimal vacation,
                       decimal advance)
    {
        this.Mentoring = mentoring;
        this.Teaching = teaching;
        this.Bonus = bonus;
        this.Vacation = vacation;
        this.Advance = advance;
    }

    public void Calculate(WorkPlan workPlan,
                          decimal salary,
                          DateOnly seniority,
                          int dayHours,
                          int nightHours,
                          decimal penalties,
                          decimal sends)
    {
        int hoursOvertimeDay = Math.Max(0, dayHours - (workPlan.NumberOfDayShifts * workPlan.NumberOfHoursPerDayShift));
        int hoursOvertimeNight = Math.Max(0, nightHours - (workPlan.NumberOfNightShifts * workPlan.NumberOfHoursPerNightShift));
        decimal salaryForShift = salary / workPlan.NumberOfDayShifts;
        decimal salaryForHour = salaryForShift / workPlan.NumberOfHoursPerDayShift;
        decimal salaryForOvertime = salaryForHour * 1.5m;

        this.OvertimeDay = hoursOvertimeDay;
        this.OvertimeNight = hoursOvertimeNight;
        this.SalaryForShift = salaryForShift;
        this.SalaryForHour = salaryForHour;
        this.Seniority = ((workPlan.Year * 12 + workPlan.Month - (seniority.Year * 12 + seniority.Month)) / 6) * 500;
        this.Earned = (dayHours + nightHours) * salaryForHour + (hoursOvertimeDay + hoursOvertimeNight) * salaryForOvertime + this.Bonus + this.Mentoring + this.Teaching + this.Seniority + this.Vacation;
        this.Penalties = penalties;
        this.Sends = sends;
        this.Payment = this.Earned - (this.Advance + this.Penalties + this.Sends);
    }
}