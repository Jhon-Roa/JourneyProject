using Application.Flights.Common;
using Domain.Flights;
using ErrorOr;
using MediatR;

namespace Application.Flights.GetAll;

internal sealed class GetAllFlightQueryHandler : IRequestHandler<GetAllFlightQuery, ErrorOr<IReadOnlyList<FlightResponse>>>
{
    private readonly IFlightRepository _flightRepository;

    public GetAllFlightQueryHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<FlightResponse>>> Handle(GetAllFlightQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Flight> flights = await _flightRepository.GetAll();

        return flights.Select(flight => new FlightResponse(
            flight.Id.Value,
            flight.Origin,
            flight.Destination,
            (decimal)flight.Price,
            new TransportResponse(
                flight.Transport.FlightNumber,
                flight.Transport.FlightCarrier
            )
        )).ToList();
    }
}