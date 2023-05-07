public class StockRequest
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string FullTitle { get; set; } = null!;

    public int MaxAcceptedWeight { get; set; }

    public int MaxAcceptedShippingWeight { get; set; }

    public int MaxAcceptedSize { get; set; }

    public int MaxAcceptedShippingSize { get; set; }
    
    public int MaxAcceptedHeight { get; set; }

    public int MaxAcceptedShippingHeight { get; set; }
}