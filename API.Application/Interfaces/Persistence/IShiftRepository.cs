public interface IShiftRepository : IRepository
{
    Task<Shift?> GetShiftByIdAsync(Guid id);
    Shift? GetShiftByStock(int stockId);
    Task<ShiftInfo?> GetShiftInfoById(Guid id);
    Task AddShift(Shift shift);
    Task AddShiftHistory(Shift shift, ShiftHistory shiftHistory);
    Task AddShiftInfos(List<ShiftInfo> shiftInfos);
    ShiftSummary GetShiftSummary(Guid employeeId, int month, int year);
    IEnumerable<AccountingEmployee> GetEmployeesByShiftInfo(int stockId, int month, int year);
    IEnumerable<AttendaceEmployee> GetFullEmployeesByShiftInfo(int stockId, int month, int year);
    IEnumerable<ShiftInfoDTO> GetShiftsByMonth(int stockId, int month, int year);
}