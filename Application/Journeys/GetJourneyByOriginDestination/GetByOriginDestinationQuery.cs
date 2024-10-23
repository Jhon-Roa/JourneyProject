using Application.Journeys.Common;
using ErrorOr;
using MediatR;

namespace Application.Journeys.GetJourneyByOriginDestination;

public record GetJourneyByOriginDestination(string Origin, string Destination) : IRequest<ErrorOr<List<JourneyResponse>>>; 

