using System.Text.Json;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace builder_challenge_infrastructure;

public class ColourRepository(HttpClient httpClient, IConfiguration configuration) : IColourRepository
{
    private readonly string _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;

    public async Task<IEnumerable<Colour>> GetColours()
    {
        var requestUrl = $"{_baseUrl}/colours";
        var response = await httpClient.GetAsync(requestUrl);

        try
        {
            if (!response.IsSuccessStatusCode) throw new Exception("Failed to retrieve colours");

            var contentStream = await response.Content.ReadAsStreamAsync();
            var coloursResponse = await JsonSerializer.DeserializeAsync<ColoursResponse>(contentStream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return coloursResponse.Colours;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private class ColoursResponse
    {
        public List<Colour> Colours { get; set; }
        public string Disclaimer { get; set; }
    }
}