public class AccountingRepository : IAccountingRepository
{
    private readonly AccountingContext _context;

    public AccountingRepository(AccountingContext context)
    {
        _context = context;
    }

    public async Task AddAccouningHistory(AccountingHistory accountingHistory)
    {
        await _context.AccountingHistories.AddAsync(accountingHistory);

        await _context.SaveChangesAsync();
    }

    public async Task AddAccounting(Accounting accounting)
    {
        await _context.Accountings.AddAsync(accounting);

        await _context.SaveChangesAsync();
    }

    public async Task<Accounting?> GetAccountingByEmployeeIdMonthYear(Guid employeeId, int month, int year)
    {
        Accounting? accounting = await _context.Accountings.FindAsync(employeeId, month, year);

        return accounting;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
