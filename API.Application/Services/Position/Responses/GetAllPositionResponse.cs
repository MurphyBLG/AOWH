public class GetAllPositionResponse
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public GetAllPositionResponse() { }

    public GetAllPositionResponse(Position position)
    {
        PositionId = position.PositionId;
        Name = position.Name;
    }
}