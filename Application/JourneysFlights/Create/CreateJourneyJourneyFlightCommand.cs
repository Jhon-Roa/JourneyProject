using Domain.Flights;
using Domain.Journeys;
using MediatR;

namespace Application.JourneysFlights.Create;

public record CreateJourneyFlightCommand(
    FlightId FlightId,
    JourneyId JourneyId
) : IRequest<Unit>;