using Microsoft.EntityFrameworkCore;

public class PositionRepository : IPositionRepository
{
    private readonly AccountingContext _context;

    public PositionRepository(AccountingContext context)
    {
        _context = context;
    }

    public async Task AddPosition(Position position)
    {
        try
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateException)
        {
            throw new InvalidDataException();
        }
        
    }
 
    public async Task<Position?> GetPositionByIdAsync(Guid? id)
    {
        Position? position = await _context.Positions.FindAsync(id);

        if (position is null)
            throw new NullReferenceException("This position isn't exist!");

        return position;
    }

    public IEnumerable<Position> GetAllPositions()
    {
        IEnumerable<Position>? positions = _context.Positions.AsNoTracking().ToList();

        return positions;
    }

    public async Task<Guid?> GetPositionIdByName(string positionName)
    {
        Position position = await _context.Positions.AsNoTracking()
                                                    .SingleAsync(p => p.Name == positionName);
        
        return position.PositionId;
    }

    public async Task Save()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateException)
        {
            throw new InvalidDataException();
        }
    }
}