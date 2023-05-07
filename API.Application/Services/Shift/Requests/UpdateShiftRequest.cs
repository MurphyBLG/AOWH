public class UpdateShiftRequest
{
    public List<string> Employees { get; set; } = new();

    public string DayOrNight { get; set; } = null!;
}