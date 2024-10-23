namespace Application.Flights.Common;

public record FlightResponse(
    Guid Id,
    string Origin,
    string Destination,
    decimal Price,
    TransportResponse Transport
);

public record TransportResponse(
    string FlightNumber,
    string FlightCarrier
);