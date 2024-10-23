using Application.Data;
using Application.Journeys.Common;
using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Journeys.GetJourneyByOriginDestination;

public class GetJourneyByOriginDestinationHandler : IRequestHandler<GetJourneyByOriginDestination, ErrorOr<List<JourneyResponse>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetJourneyByOriginDestinationHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<List<JourneyResponse>>> Handle(GetJourneyByOriginDestination request, CancellationToken cancellationToken)
    {
        var journeys = await _dbContext.Journeys
            .Where(j => j.Origin == request.Origin && j.Destination == request.Destination)
            .Include(j => j.JourneyFlights)
                .ThenInclude(jf => jf.Flight)
            .ToListAsync(cancellationToken);

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
