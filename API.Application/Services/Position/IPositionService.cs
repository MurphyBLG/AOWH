public interface IPositionService
{
    Task CreatePosition(CreatePositionRequest request);
    Task<GetPositionResponse> GetPosition(Guid id);
    IEnumerable<GetAllPositionResponse> GetAllPositions();
    Task UpdatePosition(UpdatePositionRequest request);
}