using System.Text.Json;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace builder_challenge_infrastructure;

public class SetRepository(HttpClient httpClient, IConfiguration configuration) : ISetRepository
{
    private readonly string _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;

        public async Task<List<Set>> GetAllSetsAsync()
        {
            var requestUrl = $"{_baseUrl}/sets";
            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var setsResponse = await JsonSerializer.DeserializeAsync<SetsResponse>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return setsResponse.Sets;
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch sets. Status Code: {response.StatusCode}");
            }
        }

        public async Task<Set> GetSetByNameAsync(string name)
        {
            var requestUrl = $"{_baseUrl}/by-name/{Uri.EscapeDataString(name)}";
            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var set = await JsonSerializer.DeserializeAsync<Set>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return set;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Or throw a custom exception
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch set by name. Status Code: {response.StatusCode}");
            }
        }

        public async Task<Set> GetSetByIdAsync(Guid id)
        {
            var requestUrl = $"{_baseUrl}/set/by-id/{id}";
            var response = await httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var set = await JsonSerializer.DeserializeAsync<Set>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return set;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Or throw a custom exception
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch set by ID. Status Code: {response.StatusCode}");
            }
        }

        // Helper class to match the API response structure
        private class SetsResponse
        {
            public List<Set> Sets { get; set; }
        }
}