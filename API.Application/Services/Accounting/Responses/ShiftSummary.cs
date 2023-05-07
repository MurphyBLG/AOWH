public class ShiftSummary
{
    public int CountOfDayHours { get; set; }

    public int CountOfNightHours { get; set; }

    public decimal Penalties { get; set; }

    public decimal Sends { get; set; }

    public ShiftSummary() { }
    public ShiftSummary(int countOfDayHours,
                        int countOfNightHours,
                        decimal penalties, 
                        decimal sends)
    {
        CountOfDayHours = countOfDayHours;
        CountOfNightHours = countOfNightHours;
        Penalties = penalties;
        Sends = sends;
    }
}