using Domain.ValueObjects;
using MediatR;

namespace Application.Flights.Create;

public record CreateFlightCommand(
    Transport Transport,
    string Origin,
    string Destination,
    double Price,
    string FlightCarrier,
    string FlightNumber
) : IRequest<Unit>;