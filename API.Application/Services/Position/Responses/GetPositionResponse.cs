using Newtonsoft.Json;

public class GetPositionResponse
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public InterfaceAccesses InterfaceAccesses { get; set; } = null!;

    public GetPositionResponse() { }

    public GetPositionResponse(Position position)
    {
        PositionId = position.PositionId;
        Name = position.Name;
        Salary = position.Salary;
        QuarterlyBonus = position.QuarterlyBonus;
        InterfaceAccesses = JsonConvert.DeserializeObject<InterfaceAccesses>(position.InterfaceAccesses)!;
    }
}