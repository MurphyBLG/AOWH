using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;
    private readonly IHttpClientFactory _httpClientFactory;

    public StockController(IStockService stockService, IHttpClientFactory httpClientFactory)
    {
        _stockService = stockService;
        _httpClientFactory = httpClientFactory;
    }

    // Нужно оптимизировать
    [HttpPost]
    public async Task<IActionResult> UpdateStocks()
    {
        try
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetStringAsync("https://b2b.otr-it.ru/api/public/storages");
        
            await _stockService.UpdateStocks(JsonConvert.DeserializeObject<List<StockRequest>>(response));
        
            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPost("{stockId}")]
    public async Task<IActionResult> AddLinkToStock(int stockId, [FromBody] AddLinkRequest request)
    {
        try
        {
            await _stockService.AddLinkToStock(stockId, request);

            return Ok();
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{stockId}")]
    public async Task<IActionResult> GetLinks(int stockId)
    {
        try
        {
            IEnumerable<string> result = await _stockService.GetLinks(stockId);

            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return NotFound(ex.Message);
        }
    }
}