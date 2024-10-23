using Domain.Journeys;
using Domain.Primitives;
using MediatR;

namespace Application.Journeys.Create;

internal sealed class CreateJourneyCommandHandler : IRequestHandler<CreateJourneyCommand, Unit>
{
    private readonly IJourneyRepository _journeyRepository;
    private readonly IUnitOfWork _iUnitOfWork;

    public CreateJourneyCommandHandler(IJourneyRepository customerRepository, IUnitOfWork iUnitOfWork)
    {
        _journeyRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _iUnitOfWork = iUnitOfWork ?? throw new ArgumentNullException(nameof(iUnitOfWork));;
    }

    public async Task<Unit> Handle(CreateJourneyCommand command, CancellationToken cancellationToken)
    {
        Journey journey = new Journey(
            new JourneyId(Guid.NewGuid()),
            command.Origin,
            command.Destination,
            command.price
        );

        _journeyRepository.Add(journey);

        await _iUnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
