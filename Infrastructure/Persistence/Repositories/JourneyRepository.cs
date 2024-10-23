using Domain.Journeys;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<Journey>> GetByOriginDestinationAsync(string origin, string destination)
    {
        return await _context.Journeys
            .Where(j => j.Origin == origin && j.Destination == destination)
            .Include(j => j.JourneyFlights)
                .ThenInclude(jf => jf.Flight)
            .ToListAsync();
    }
}

