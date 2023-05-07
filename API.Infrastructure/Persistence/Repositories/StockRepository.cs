using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

public class StockRepository : IStockRepository
{
    private readonly AccountingContext _context;

    public StockRepository(AccountingContext context)
    {
        _context = context;
    }

    public IEnumerable<StockDTO> GetAllStocksNameByEmployee(string? stocks)
    {
        List<int> stocksIds = JsonConvert.DeserializeObject<List<int>>(stocks!)!;
        
        List<StockDTO> stockResult = new List<StockDTO>();
        foreach(int id in stocksIds)
        {
            Stock stock = _context.Stocks.Find(id)!;

            stockResult.Add(new StockDTO
            {
                StockId = id,
                StockName = stock.StockName
            });
        }

        return stockResult;
    }

    public async Task<Stock?> GetStockById(int id)
    {
        Stock? stock = await _context.Stocks.FindAsync(id);

        return stock;
    }

    public async Task AddStock(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public IEnumerable<int> GetAllStocksId()
    {
        IEnumerable<int> ids = _context.Stocks.AsNoTracking().Select(e => e.StockId);

        return ids;
    }
}
