using Domain.Journeys;

namespace Infrastructure.Persistence.Repositories;

public class JourneyRepository : IJourneyRepository
{
    private readonly ApplicationDbContext _context;

    public JourneyRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async void Add(Journey journey) => await _context.Journeys.AddAsync(journey);

    public void Delete(Journey journey)
    {
        throw new NotImplementedException();
    }

    public Task<List<Journey>> GetByOriginDestinationAsync(string origin, string destination)
    {
        throw new NotImplementedException();
    }
}

