public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepo;

    public PositionService(IPositionRepository positionRepo)
    {
        _positionRepo = positionRepo;
    }

    public async Task CreatePosition(CreatePositionRequest request)
    {
        Position position = new Position(
            Guid.NewGuid(),
            request.Name,
            request.Salary,
            request.QuarterlyBonus,
            request.InterfaceAccesses);
        try
        {
            await _positionRepo.AddPosition(position);
        }
        catch(InvalidDataException)
        {
            throw new InvalidDataException($"Должность с названием \"{position.Name}\" уже есть!");
        }
    }

    public async Task<GetPositionResponse> GetPosition(Guid id)
    {
        Position? position = await _positionRepo.GetPositionByIdAsync(id);

        if (position is null)
            throw new NullReferenceException($"Должности с ID:{id} не существует!");

        return new GetPositionResponse(position);
    }

    public IEnumerable<GetAllPositionResponse> GetAllPositions()
    {
        IEnumerable<Position> positions = _positionRepo.GetAllPositions();

        IEnumerable<GetAllPositionResponse> result = positions.Select(pos => new GetAllPositionResponse(pos));

        return result;
    }

    public async Task UpdatePosition(UpdatePositionRequest request)
    {
        Position? position = await _positionRepo.GetPositionByIdAsync(request.PositionId);

        if (position is null)
            throw new NullReferenceException($"Должности с ID:{request.PositionId} не существует!");

        position.Update(
            request.Name,
            request.Salary,
            request.QuarterlyBonus,
            request.InterfaceAccesses);

        try
        {
            await _positionRepo.Save();
        }
        catch(InvalidDataException)
        {
            throw new InvalidDataException($"Должность с названием \"{position.Name}\" уже есть!");
        }
    }
}