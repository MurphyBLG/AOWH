public class GetWorkPlanResponse
{
    public Guid WorkPlanId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public Guid EmployeeId { get; set; }

    public int NumberOfDayShifts { get; set; }

    public int NumberOfHoursPerDayShift { get; set; }

    public int NumberOfNightShifts { get; set; }

    public int NumberOfHoursPerNightShift { get; set; }
}