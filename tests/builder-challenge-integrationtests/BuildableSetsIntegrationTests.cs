using System;
using builder_challenge_integrationtests.Stubs;

namespace builder_challenge_integrationtests;

public class BuildableSetsIntegrationTests
{
    [Fact]
    public void GetBuildableSetsForUser_WithExactMatchingInventory_ReturnsExpectedSets()
    {
        // Arrange
        var user = UserStubData.GetDefaultUser().First();
        var set = SetStubData.GetBlueHouseSet().First();
        var sut = new BuildableSets();
        // Act
        var canUserBuildSet = sut.CanUserBuildSet(user, set);
        // Assert
        sut.Should().BeTrue();
    }

    [Fact]
    public void GetBuildableSetsForUser_WithSurplusInventory_ReturnsExpectedSets()
    {
        // Arrange
        
    
        // Act
        
    
        // Assert
        
    }

    [Fact]
    public void GetBuildableSetsForUser_InsufficientPieces_ReturnsEmptyOrNoSet()
    {
        // Arrange
                
    
        // Act
        
    
        // Assert
        
    }

    [Fact]
    public void GetBuildableSetsForUser_MissingPiece_ReturnsNoSet()
    {
        // Arrange
        
    
        // Act
        
    
        // Assert
        
    }

    [Fact]
    public void GetBuildableSetsForUser_ColorMismatch_ReturnsNoSet()
    {
        // Arrange
        
    
        // Act
        
    
        // Assert
        
    }

    [Fact]
    public void GetBuildableSetsForUser_EmptyInventory_ReturnsNoSet()
    {
        // Arrange
        
    
        // Act
        
    
        // Assert
        
    }
}
