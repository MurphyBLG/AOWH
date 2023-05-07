public class Shift
{
    public Guid ShiftId { get; set; }

    public int StockId { get; set; }

    public Guid EmployeeWhoPostedTheShiftId { get; set; }

    public string DayOrNight { get; set; } = null!;

    public DateTime? OpeningDateAndTime { get; set; }

    public string Employees { get; set; } = null!;

    public DateTime? ClosingDateAndTime { get; set; }

    public DateTime? LastUpdate { get; set; }


    public virtual Stock? Stock { get; set; }

    public virtual Employee? EmployeeWhoPostedTheShift { get; set; }

    public Shift() { }

    public Shift(int stockId,
                 Guid employeeWhoPostedTheShiftId,
                 string dayOrNight,
                 string employees)
    {
        ShiftId = new Guid();
        StockId = stockId;
        EmployeeWhoPostedTheShiftId = employeeWhoPostedTheShiftId;
        DayOrNight = dayOrNight;
        OpeningDateAndTime = DateTime.UtcNow;
        Employees = employees;
        // ClosingDateAndTime = closingDateAndTime; ????
        LastUpdate = DateTime.UtcNow;
    }

    public void Update(string employees,
                       string dayOrNight)
    {
        this.Employees = employees;
        this.DayOrNight = dayOrNight;
        this.LastUpdate = DateTime.UtcNow;
    }

    public void Close()
    {
        ClosingDateAndTime = DateTime.UtcNow;
        LastUpdate = DateTime.UtcNow;
    }
}