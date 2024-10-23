using Domain.Flights;
using Domain.Journeys; 

namespace Application.Journeys.Common;
    public record JourneyResponse(
        JourneyId Id, 
        string Origin,
        string Destination,
        decimal Price,
        List<Flight?> Flights 
    );
