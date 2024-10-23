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

    public Task<bool> ExistsAsync(JourneyId id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Journey>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Journey?> GetByIdAsync(JourneyId id) => await _context.Journeys.SingleOrDefaultAsync(journey => journey.Id == id);

    public void Update(Journey journey)
    {
        throw new NotImplementedException();
    }
}

