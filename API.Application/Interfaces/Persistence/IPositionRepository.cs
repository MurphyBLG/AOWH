public interface IPositionRepository : IRepository
{
    Task<Guid?> GetPositionIdByName(string position);
    Task<Position?> GetPositionByIdAsync(Guid? id);
    IEnumerable<Position> GetAllPositions();
    Task AddPosition(Position position);
}