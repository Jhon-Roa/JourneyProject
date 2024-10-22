using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public partial record Transport
{
    private const string pattern = @"^[0-9]";

    public string FlightCarrier { get; init; }
    public string FlightNumber { get; init; }

    private Transport(string flightCarrier, string flightNumber)
    {
        FlightCarrier = flightCarrier;
        FlightNumber = flightNumber;
    }

    public static Transport? Create(string flightCarrier, string flightNumber)
    {
        if (string.IsNullOrEmpty(flightCarrier) || string.IsNullOrEmpty(flightNumber) || !FLightNumberRegex().IsMatch(flightNumber))
        {
            return null;
        }

        return new Transport(flightCarrier, flightNumber);
    }

    [GeneratedRegex(pattern)]
    private static partial Regex FLightNumberRegex();
}