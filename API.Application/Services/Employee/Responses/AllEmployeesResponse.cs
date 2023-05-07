public class AllEmployeesResponse
{
    public Guid EmployeeId { get; init; }

    public string Name { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public string Patronymic { get; init; } = null!;

    public IEnumerable<StockDTO>? Stocks { get; init; }

    public string? Link { get; init; }
}