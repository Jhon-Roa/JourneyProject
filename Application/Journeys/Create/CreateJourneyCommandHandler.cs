using Domain.Journeys;
using Domain.Primitives;
using MediatR;

namespace Application.Journeys.Create;

internal sealed class CreateJourneyCommandHandler : IRequestHandler<CreateJourneyCommand, Unit>
{
    private readonly IJourneyRepository _journeyRepository;
    private readonly IUnitOfWork _iUnitOfWork;

    public CreateJourneyCommandHandler(IJourneyRepository journeyRepository, IUnitOfWork iUnitOfWork)
    {
        _journeyRepository = journeyRepository ?? throw new ArgumentNullException(nameof(journeyRepository));
        _iUnitOfWork = iUnitOfWork ?? throw new ArgumentNullException(nameof(iUnitOfWork));;
    }

    public async Task<Unit> Handle(CreateJourneyCommand command, CancellationToken cancellationToken)
    {
        Journey journey = new Journey(
            new JourneyId(Guid.NewGuid()),
            command.Origin,
            command.Destination,
            command.Price
        );

        _journeyRepository.Add(journey);

        await _iUnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
