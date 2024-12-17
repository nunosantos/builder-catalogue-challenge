using System.Text.Json;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace builder_challenge_infrastructure;

public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public UserRepository(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _baseUrl = configuration.GetSection("ApiSettings:BaseUrl").Value;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var requestUrl = $"{_baseUrl}/users";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var usersResponse = await JsonSerializer.DeserializeAsync<UsersResponse>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return usersResponse.Users;
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch users. Status Code: {response.StatusCode}");
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var requestUrl = $"{_baseUrl}/user/by-username/{Uri.EscapeDataString(username)}";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var user = await JsonSerializer.DeserializeAsync<User>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch user by username. Status Code: {response.StatusCode}");
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var requestUrl = $"{_baseUrl}/user/by-id/{id}";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var user = await JsonSerializer.DeserializeAsync<User>(contentStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return user;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Or throw a custom exception
            }
            else
            {
                throw new HttpRequestException($"Failed to fetch user by ID. Status Code: {response.StatusCode}");
            }
        }

        // Helper class to match the API response structure
        private class UsersResponse
        {
            public List<User> Users { get; set; }
        }
}