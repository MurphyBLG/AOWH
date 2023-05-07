public interface IAccountingRepository : IRepository
{
    Task<Accounting?> GetAccountingByEmployeeIdMonthYear(Guid employeeId, int month, int year);
    Task AddAccounting(Accounting accounting);
    Task AddAccouningHistory(AccountingHistory accountingHistory);
}