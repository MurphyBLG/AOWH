using Newtonsoft.Json;

public class StockService : IStockService
{
    private readonly IStockRepository _stockRepo;

    public StockService(IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
    }

    public async Task UpdateStocks(List<StockRequest>? stocks)
    {
        if (stocks is null)
            throw new NullReferenceException("Не возможно получить список складов!");

        List<Stock> stocksToAdd = new();
        foreach (StockRequest stockFromAPI in stocks)
        {
            Stock? stock = await _stockRepo.GetStockById(stockFromAPI.Id);

            if (stock is null)
            {
                await _stockRepo.AddStock(new Stock
                {
                    StockId = stockFromAPI.Id,
                    StockName = stockFromAPI.FullTitle,
                    Links = "[]"
                });

                continue;
            }

            stock.StockName = stockFromAPI.FullTitle;
        }

        await _stockRepo.Save();
    }

    public async Task AddLinkToStock(int id, AddLinkRequest request)
    {
        Stock? stock = await _stockRepo.GetStockById(id);

        if (stock is null)
            throw new NullReferenceException($"Склада ID:{id} не существует!");
        

        SortedSet<string>? links = JsonConvert.DeserializeObject<SortedSet<string>>(stock.Links);

        if (links is null)
            throw new NullReferenceException("У данного склада нет списка звеньев! / Ошибка десериализации");
        

        if (links.Contains(request.Name))
            throw new Exception("Это звено уже существует!");
        

        links.Add(request.Name);

        stock.Links = JsonConvert.SerializeObject(links);

        await _stockRepo.Save();
    }

    public async Task<IEnumerable<string>> GetLinks(int id)
    {
        Stock? stock = await _stockRepo.GetStockById(id);

        if (stock is null)
            throw new NullReferenceException($"Склада ID:{id} не существует!");

        IEnumerable<string> links = JsonConvert.DeserializeObject<IEnumerable<string>>(stock.Links!)!; 

        return links;
    }
}