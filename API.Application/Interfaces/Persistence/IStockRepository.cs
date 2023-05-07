public interface IStockRepository : IRepository
{
    Task<Stock?> GetStockById(int id);
    IEnumerable<StockDTO> GetAllStocksNameByEmployee(string? stocks);
    IEnumerable<int> GetAllStocksId();
    Task AddStock(Stock stock);
}