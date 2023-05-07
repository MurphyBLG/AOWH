public class GetAttendanceResponse
{
    public AttendaceEmployee Employee { get; init; } = null!;
    public List<ShiftInfoDTO> Shifts { get; init; } = null!;

    public GetAttendanceResponse(AttendaceEmployee employee, List<ShiftInfoDTO> shifts)
    {
        Employee = employee;
        Shifts = shifts;
    }
}