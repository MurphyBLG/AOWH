public class PositionDTO
{
    public Guid? PositionId { get; init; }

    public string Name { get; init; } = null!;

    public decimal Salary { get; init; }

    public decimal QuarterlyBonus { get; init; }

    public InterfaceAccesses InterfaceAccesses { get; init; } = null!;
}