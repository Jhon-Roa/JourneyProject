using Domain.Flights;
using Domain.Journeys;
using Domain.JourneysFlights;

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
        throw new NotImplementedException();
    }

    public Task<List<Flight>> GetFlightsByJourneyId(JourneyId journeyId)
    {
        throw new NotImplementedException();
    }
}