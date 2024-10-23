using Domain.JourneysFlights;
using Domain.Primitives;
using Domain.ValueObjects;
using MediatR;

namespace Application.JourneysFlights.Create;

internal sealed class CreateJourneyFlightCommandHandler : IRequestHandler<CreateJourneyFlightCommand, Unit>
{
    private readonly IJourneyFlightRepository _journeyFlightRepository;
    private readonly IUnitOfWork _iUnitOfWork;

    public CreateJourneyFlightCommandHandler(IJourneyFlightRepository journeyFlightRepository, IUnitOfWork iUnitOfWork)
    {
        _journeyFlightRepository = journeyFlightRepository ?? throw new ArgumentNullException(nameof(journeyFlightRepository));
        _iUnitOfWork = iUnitOfWork ?? throw new ArgumentNullException(nameof(iUnitOfWork));;
    }

    public async Task<Unit> Handle(CreateJourneyFlightCommand command, CancellationToken cancellationToken)
    {

        JourneyFlight journeyFlight = new(
            command.JourneyId,
            command.FlightId
        );

        _journeyFlightRepository.Add(journeyFlight);

        await _iUnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
