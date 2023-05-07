public class UpdatePositionRequest
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public string InterfaceAccesses { get; set; } = null!;
}