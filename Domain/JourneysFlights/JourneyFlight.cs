using Domain.Flights;
using Domain.Journeys;

namespace Domain.JourneysFlights;

public class JourneyFlight
{
    public JourneyFlight(JourneyId journeyId, FlightId flightId)
    {
        JourneyId = journeyId;
        FlightId = flightId;
    }

    public JourneyId JourneyId { get; private set; }
    public FlightId FlightId { get; private set; }

    public Journey? Journey { get; private set; }
    public Flight? Flight { get; private set; }

    public void SetJourney(Journey journey)
    {
        Journey = journey;
    }

    public void SetFlight(Flight flight)
    {
        Flight = flight;
    }
}
