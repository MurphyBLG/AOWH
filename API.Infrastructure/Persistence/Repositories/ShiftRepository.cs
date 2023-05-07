using Microsoft.EntityFrameworkCore;

public class ShiftRepository : IShiftRepository
{
    private readonly AccountingContext _context;

    public ShiftRepository(AccountingContext context)
    {
        _context = context;
    }

    public async Task AddShift(Shift shift)
    {
        await _context.Shifts.AddAsync(shift);

        await _context.SaveChangesAsync();
    }

    public async Task AddShiftHistory(Shift shift, ShiftHistory shiftHistory)
    {
        await _context.ShiftHistories.AddAsync(shiftHistory);

        _context.Shifts.Remove(shift);

        await _context.SaveChangesAsync();
    }

    public async Task AddShiftInfos(List<ShiftInfo> shiftInfos)
    {
        await _context.ShiftInfos.AddRangeAsync(shiftInfos);

        await _context.SaveChangesAsync();
    }

    public async Task<Shift?> GetShiftByIdAsync(Guid id)
    {
        Shift? shift = await _context.Shifts.FindAsync(id);

        return shift;
    }

    public Shift? GetShiftByStock(int stockId)
    {
        Shift? shift = _context.Shifts.AsNoTracking().Where(s => s.StockId == stockId).FirstOrDefault();

        return shift;
    }

    public async Task<ShiftInfo?> GetShiftInfoById(Guid id)
    {
        ShiftInfo? shiftInfo = await _context.ShiftInfos.FindAsync(id);

        return shiftInfo;
    }

    public ShiftSummary GetShiftSummary(Guid employeeId, int month, int year)
    {
        IEnumerable<ShiftInfo> result = _context.ShiftInfos
            .Where(s => s.DateAndTimeOfArrival!.Month == month
                && s.DateAndTimeOfArrival!.Year == year
                && s.EmployeeId == employeeId).ToList();

        int countOfDayHours = 0, countOfNightHours = 0;
        decimal penalties = 0, sends = 0;
        foreach (var record in result)
        {
            if (record.DayOrNight == "День")
                countOfDayHours += record.NumberOfHoursWorked;
            else
                countOfNightHours += record.NumberOfHoursWorked;

            if (record.Penalty is not null)
                penalties += (decimal)record.Penalty;

            if (record.Send is not null)
                sends += (decimal)record.Send;
        }

        return new ShiftSummary(
            countOfDayHours,
            countOfNightHours,
            penalties,
            sends);
    }

    public IEnumerable<AccountingEmployee> GetEmployeesByShiftInfo(int stockId, int month, int year)
    {
        IEnumerable<AccountingEmployee> result = _context.ShiftInfos
            .Include(s => s.ShiftHistory)
            .AsNoTracking()
            .Where(s => s.DateAndTimeOfArrival!.Month == month)
            .Where(s => s.DateAndTimeOfArrival!.Year == year)
            .Include(s => s.Employee)
                .ThenInclude(e => e!.Position)
                    .Where(s => s.Employee!.Stocks == $"[{stockId}]")
                    .Select(shiftInfo => new AccountingEmployee
                    {
                        EmployeeId = shiftInfo.EmployeeId,
                        FullName = $"{shiftInfo.Employee!.Surname} {shiftInfo.Employee.Name} {shiftInfo.Employee.Patronymic}",
                        PositionName = shiftInfo.Employee.Position!.Name,
                        Salary = shiftInfo.Employee.Salary,
                        Seniority = shiftInfo.Employee.StartOfLuchSeniority
                    }).Distinct();

        return result.ToList();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public IEnumerable<AttendaceEmployee> GetFullEmployeesByShiftInfo(int stockId, int month, int year)
    {
        IEnumerable<AttendaceEmployee> result = _context.ShiftInfos
            // .Include(s => s.ShiftHistory)
            .Where(s => s.DateAndTimeOfArrival!.Month == month && s.DateAndTimeOfArrival!.Year == year)
            .Include(s => s.Employee)
                .ThenInclude(e => e!.Position)
                    .Where(s => s.Employee!.Stocks == $"[{stockId}]")
                    .Select(shiftInfo => new AttendaceEmployee
                    {
                        EmployeeId = shiftInfo.EmployeeId,
                        FullName = $"{shiftInfo.Employee!.Surname} {shiftInfo.Employee.Name} {shiftInfo.Employee.Patronymic}",
                        PositionName = shiftInfo.Employee.Position!.Name
                    }).Distinct();

        return result.ToList();
    }

    public IEnumerable<ShiftInfoDTO> GetShiftsByMonth(int stockId, int month, int year)
    {
        IEnumerable<ShiftInfoDTO> result = _context.ShiftInfos
            .Where(s => s.DateAndTimeOfArrival.Month == month && s.DateAndTimeOfArrival.Year == year)
            .AsNoTracking()
            .Include(s => s.ShiftHistory)
            .Select(shift => new ShiftInfoDTO
            {
                Editable = shift.ShiftHistory!.StockId == stockId,
                ShiftId = shift.ShiftInfoId,
                EmployeeId = shift.EmployeeId,
                Day = shift.DateAndTimeOfArrival.Day,
                DayOrNight = shift.DayOrNight,
                WorkedHours = shift.NumberOfHoursWorked,
                Penalty = shift.Penalty,
                PenaltyComment = shift.PenaltyComment,
                Send = shift.Send,
                SendComment = shift.SendComment
            }).ToList();

        return result.ToList();
    }
}
