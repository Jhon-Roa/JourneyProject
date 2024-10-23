using Domain.Flights;
using Domain.Journeys;

namespace Domain.JourneysFlights;

public interface IJourneyFlightRepository
{
    void Add(JourneyFlight journeyFlight);

    Task<List<Flight>> GetFlightsByJourneyId(JourneyId journeyId);
}