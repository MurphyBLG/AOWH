public class CloseShiftRequest
{
    public Guid ShiftId { get; set; }

    public Dictionary<string, int> WorkedHours { get; set; } = new();
}