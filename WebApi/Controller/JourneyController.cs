using Application.Journeys.GetJourneyByOriginDestination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi.Controller;

[ApiController]
[Route("journeys")]
public class Journeys : ApiController
{
    private readonly ISender _mediator;

    public Journeys(ISender mediator)
    {
        _mediator = mediator?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("{origin}, {destination}")]
    public async Task<IActionResult> GetOrigin(string origin, string destination)
    {
        var journeyResult = await _mediator.Send(new GetJourneyByOriginDestination(origin, destination));

        if (!journeyResult.IsError)
        {
            return Ok(journeyResult.Value);
        }

        return NotFound();
    }
    
}
