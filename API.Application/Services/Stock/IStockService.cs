public interface IStockService
{
    Task UpdateStocks(List<StockRequest>? stocks);
    Task AddLinkToStock(int id, AddLinkRequest request);
    Task<IEnumerable<string>> GetLinks(int id);
}