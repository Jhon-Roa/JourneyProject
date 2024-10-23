using Application.Journeys.Common;
using Domain.Journeys; 
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.Journeys.GetJourneyByOriginDestination;

internal sealed class GetJourneyByOriginDestinationHandler : IRequestHandler<GetJourneyByOriginDestination, ErrorOr<List<JourneyResponse>>>
{
    private readonly IJourneyRepository _journeyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetJourneyByOriginDestinationHandler(IJourneyRepository journeyRepository, IUnitOfWork unitOfWork)
    {
        _journeyRepository = journeyRepository ?? throw new ArgumentNullException(nameof(journeyRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<List<JourneyResponse>>> Handle(GetJourneyByOriginDestination request, CancellationToken cancellationToken)
    {
        var journeys = await _journeyRepository.GetByOriginDestinationAsync(request.Origin, request.Destination);

        if (!journeys.Any())
        {
            return Error.NotFound("Journey not found");
        }

        var journeyResponses = journeys.Select(j => new JourneyResponse(
            Id: j.Id,
            Origin: j.Origin,
            Destination: j.Destination,
            Price: (decimal)j.Price,
            Flights: j.JourneyFlights.Select(jf => jf.Flight)
                                      .Where(f => f != null)
                                      .ToList()
        )).ToList();

        return journeyResponses;
    }
}
