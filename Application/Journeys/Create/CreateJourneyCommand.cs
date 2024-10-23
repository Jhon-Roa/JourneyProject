using MediatR;

namespace Application.Journeys.Create;

public record CreateJourneyCommand(
    string Origin,
    string Destination,
    double price
) : IRequest<Unit>;