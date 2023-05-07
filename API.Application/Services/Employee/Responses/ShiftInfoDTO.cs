public class ShiftInfoDTO
{
    public bool Editable { get; init; }

    public Guid ShiftId { get; init; }

    public Guid EmployeeId { get; init; }

    public int Day { get; init; }  

    public string DayOrNight { get; init; } = null!;

    public int WorkedHours { get; init; }

    public decimal? Penalty { get; init; }

    public string? PenaltyComment { get; init; }

    public decimal? Send { get; init; }

    public string? SendComment { get; init; }
}