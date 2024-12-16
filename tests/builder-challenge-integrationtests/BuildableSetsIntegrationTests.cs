using System;
using System.Net;
using System.Text;
using System.Text.Json;
using builder_challenge_integrationtests.Stubs;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Sdk;

namespace builder_challenge_integrationtests;

public class BuildableSetsIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public BuildableSetsIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    // ensure that the API returns a status code of 200
    [Fact]
    public async Task GetBuildableSetsForUser_ReturnsSuccessStatusCode()
    {
        // Arrange
        var user = UserStubData.GetDefaultUser().First();
        
        // Act
        var response = await _client.GetAsync($"/buildable-sets/{user.Username}");
        
        // Assert
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task GivenIncorrectUsername_ReturnsBadRequest()
    {
        // Arrange
        var user = " ";
        
        // Act
        var response = await _client.GetAsync($"/buildable-sets/{user}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
        
    
    [Fact]
    public async Task GetBuildableSetsForUser_WithExactMatchingInventory_ReturnsExpectedSets()
    {
        // Arrange
        var user = UserStubData.GetDefaultUser().First();
        var set = SetStubData.GetStubData().First();
        //var sut = new BuildableSets();
        
        // var stringContent = new StringContent(JsonSerializer.Serialize(createOrderItemsCommand), Encoding.UTF8, "application/json");
        
        // Act
        var response = await _client.GetAsync($"/users");
        
        // Assert
        response.EnsureSuccessStatusCode();
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
