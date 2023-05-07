public class GetCurrentShiftResponse
{
    public Guid ShiftId { get; set; }

    public string DayOrNight { get; set; } = null!;

    public List<EmployeeDTO> Employees { get; set; } = new ();

    public GetCurrentShiftResponse(Guid shiftId,
                                   string dayOrNight,
                                   List<EmployeeDTO> employees)
    {
        ShiftId = shiftId;
        DayOrNight = dayOrNight;
        Employees = employees;
    }
}