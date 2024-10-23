using Domain.Flights;
using Domain.Journeys;
using Domain.JourneysFlights;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class JourneyFlightsRepository : IJourneyFlightRepository
{
    private readonly ApplicationDbContext _context;

    public JourneyFlightsRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(JourneyFlight journeyFlight)
    {
        _context.JourneyFlights.Add(journeyFlight);
    }
    public async Task<List<Flight?>> GetFlightsByJourneyId(JourneyId journeyId)
{
    var journeyFlights = await _context.JourneyFlights
        .Where(jf => jf.JourneyId == journeyId)
        .Include(jf => jf.Flight)
        .ToListAsync();

    return journeyFlights.Select(jf => jf.Flight)
                         .Where(f => f != null) 
                         .ToList();
}

}
