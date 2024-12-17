using System;
using System.Net;
using System.Text;
using System.Text.Json;
using builder_challenge_domain.Entities;
using builder_challenge_integrationtests.Stubs;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Sdk;

namespace builder_challenge_integrationtests;

public class BuildableSetsIntegrationTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    // ensure that the API returns a status code of 200
    [Fact]
    public async Task GetBuildableSetsForUser_ReturnsSuccessStatusCode()
    {
        // Arrange
        var user = "brickfan35";

        // Act
        var response = await _client.GetAsync($"/buildable-sets/{user}");

        // Assert
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // Define JsonSerializer options
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Deserialize the response into a List<Set>
        var buildableSets = JsonSerializer.Deserialize<List<Set>>(responseString, options);

        // Further Assertions
        buildableSets.Should().NotBeNull();
        buildableSets.Should().HaveCount(3);
    }

    [Fact]
    public async Task GivenIncorrectUsername_ReturnsBadRequest()
    {
        // Arrange


        // Act


        // Assert
    }


    [Fact]
    public async Task GetBuildableSetsForUser_WithExactMatchingInventory_ReturnsExpectedSets()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public async Task GetBuildableSetsForUser_WithSurplusInventory_ReturnsExpectedSets()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public async Task GetBuildableSetsForUser_InsufficientPieces_ReturnsEmptyOrNoSet()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public async Task GetBuildableSetsForUser_MissingPiece_ReturnsNoSet()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public async Task GetBuildableSetsForUser_ColorMismatch_ReturnsNoSet()
    {
        // Arrange


        // Act


        // Assert
    }

    [Fact]
    public async Task GetBuildableSetsForUser_EmptyInventory_ReturnsNoSet()
    {
        // Arrange


        // Act


        // Assert
    }
}