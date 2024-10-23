using System.Text.Json;
using Application.Flights.Services.Models;

namespace Application.Flights.Services;

public class FlightApiService
{
    private readonly HttpClient _httpClient;

    public FlightApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FlightDto>?> GetFlightsAsync()
    {
        var response = await _httpClient.GetAsync("https://bitecingcom.ipage.com/testapi/avanzado.js");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<FlightDto>>(json);
    }
}
