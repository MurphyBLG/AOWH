public class ShiftInfo 
{
    public Guid ShiftInfoId { get; set; }

    public Guid ShiftHistoryId { get; set; }

    public Guid EmployeeId { get; set; }

    public DateTime DateAndTimeOfArrival { get; set; }

    public string DayOrNight { get; set; } = null!;

    public int NumberOfHoursWorked { get; set; }

    public decimal? Penalty { get; set; }

    public string? PenaltyComment { get; set; }

    public decimal? Send { get; set; }

    public string? SendComment { get; set; }

    public virtual ShiftHistory? ShiftHistory { get; set; }

    public virtual Employee? Employee { get; set; }

    public ShiftInfo() { }

    public ShiftInfo(Guid shiftHistoryId,
                     Guid employeeId,
                     DateTime dateAndTimeOfArrival,
                     string dayOrNight,
                     int numberOfHoursWorked)
    {
        this.ShiftInfoId = new Guid();
        this.ShiftHistoryId = shiftHistoryId;
        this.EmployeeId = employeeId;
        this.DateAndTimeOfArrival = dateAndTimeOfArrival;
        this.DayOrNight = dayOrNight;
        this.NumberOfHoursWorked = numberOfHoursWorked;
    }

    public void Update(decimal? penalty,
                       string? penaltyComment,
                       decimal? send,
                       string? sendComment)
    {
        this.Penalty = penalty;
        this.PenaltyComment = penaltyComment;
        this.Send = send;
        this.SendComment = sendComment;
    }
}