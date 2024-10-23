using Domain.Flights;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace Application.Flights.Create;

internal sealed class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, ErrorOr<Unit>>
{
    private readonly IFlightRepository _flightRepository;
    private readonly IUnitOfWork _iUnitOfWork;

    public CreateFlightCommandHandler(IFlightRepository flightRepository, IUnitOfWork iUnitOfWork)
    {
        _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
        _iUnitOfWork = iUnitOfWork ?? throw new ArgumentNullException(nameof(iUnitOfWork));;
    }

    public async Task<ErrorOr<Unit>> Handle(CreateFlightCommand command, CancellationToken cancellationToken)
    {
        if(Transport.Create(command.FlightCarrier, command.FlightNumber) is not Transport transport)
        {
            return Error.Validation("Flight.Transport", "Transport has no valid format");
        }

        Flight flight = new(
            new FlightId(Guid.NewGuid()),
            command.Transport,
            command.Origin,
            command.Destination,
            command.Price
        );

        _flightRepository.Add(flight);

        await _iUnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
