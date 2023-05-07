public class ShiftHistory
{
    public Guid ShiftHistoryId { get; set; }

    public int StockId { get; set; }

    public Guid EmployeeWhoPostedTheShiftId { get; set; }

    public string DayOrNight { get; set; } = null!;

    public DateTime? OpeningDateAndTime { get; set; }

    public string Employees { get; set; } = null!;

    public DateTime? ClosingDateAndTime { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Stock? Stock { get; set; }

    public virtual Employee? EmployeeWhoPostedTheShift { get; set; }

    public virtual ICollection<ShiftInfo> ShiftInfos { get; } = new List<ShiftInfo>();

    public ShiftHistory() { }

    public ShiftHistory(Guid shiftId,
                        int stockId,
                        Guid employeeWhoPostedTheShiftId,
                        string dayOrNight,
                        DateTime? openingDateAndTime,
                        string employees,
                        DateTime? closingDateAndTime,
                        DateTime? lastUpdate)
    {
        ShiftHistoryId = shiftId;
        StockId = stockId;
        EmployeeWhoPostedTheShiftId = employeeWhoPostedTheShiftId;
        DayOrNight = dayOrNight;
        OpeningDateAndTime = openingDateAndTime;
        Employees = employees;
        ClosingDateAndTime = closingDateAndTime;
        LastUpdate = lastUpdate;
    }
}