using builder_challenge_application.Interfaces;
using builder_challenge_application.Services;
using builder_challenge_domain.Entities;
using FluentAssertions;
using Moq;

namespace builder_challenge_unittests;

public class BuildableSetServiceTests
{
    [Fact]
    public void CanBuildSet_ExactInventory_ReturnsTrue()
    {
        // Arrange
        var set = new Set
        {
            Id = Guid.NewGuid(),
            Name = "Test Set",
            SetNumber = "12345",
            Pieces = new List<Piece>
            {
                new()
                {
                    Part = new Part { DesignID = "3001", Material = 3, PartType = "rigid" },
                    Quantity = 5
                }
            }
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "test-user",
            Location = "TestLocation",
            BrickCount = 100,
            Collection = new List<UserCollectionPiece>
            {
                new()
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new() { Color = "3", Count = 5 } 
                    }
                }
            }
        };

        var setServiceMock = new Mock<ISetService>();
        var service = new BuildableSetService(setServiceMock.Object);
        // Act

        var canBuild = service.CanUserBuildSet(user, set);

        // Assert
        canBuild.Should().BeTrue();
    }

    [Fact]
    public async Task CanBuildSet_SurplusInventory_ReturnsTrue()
    {
        // Arrange
        var set = new Set
        {
            Id = Guid.NewGuid(),
            Name = "Surplus Set",
            SetNumber = "67890",
            Pieces = new List<Piece>
            {
                new()
                {
                    Part = new Part { DesignID = "3001", Material = 2, PartType = "rigid" },
                    Quantity = 4
                }
            }
        };

        var user = new User
        {
            Collection = new List<UserCollectionPiece>
            {
                new()
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new() { Color = "2", Count = 10 } 
                    }
                }
            }
        };

        var setServiceMock = new Mock<ISetService>();
        var service = new BuildableSetService(setServiceMock.Object);

        // Act
        var canBuild = service.CanUserBuildSet(user, set);

        // Assert
        canBuild.Should().BeTrue();
    }

    [Fact]
    public async Task CanBuildSet_InsufficientQuantity_ReturnsFalse()
    {
        // Arrange
        var set = new Set
        {
            Pieces = new List<Piece>
            {
                new()
                {
                    Part = new Part { DesignID = "3005", Material = 5, PartType = "rigid" },
                    Quantity = 10
                }
            }
        };

        var user = new User
        {
            Collection = new List<UserCollectionPiece>
            {
                new()
                {
                    PieceId = "3005",
                    Variants = new List<PieceVariant>
                    {
                        new() { Color = "5", Count = 9 } 
                    }
                }
            }
        };

        var setServiceMock = new Mock<ISetService>();
        var service = new BuildableSetService(setServiceMock.Object);

        // Act
        var canBuild = service.CanUserBuildSet(user, set);

        // Assert
        canBuild.Should().BeFalse();
    }

    [Fact]
    public async Task CanBuildSet_MissingPiece_ReturnsFalse()
    {
        // Arrange
        var set = new Set
        {
            Pieces = new List<Piece>
            {
                new()
                {
                    Part = new Part { DesignID = "9999", Material = 4, PartType = "rigid" },
                    Quantity = 2
                }
            }
        };

        var user = new User
        {
            Collection = new List<UserCollectionPiece>
            {
                // No piece with ID 9999
                new()
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new() { Color = "4", Count = 10 }
                    }
                }
            }
        };

        var setServiceMock = new Mock<ISetService>();
        var service = new BuildableSetService(setServiceMock.Object);

        // Act
        var canBuild = service.CanUserBuildSet(user, set);

        // Assert
        canBuild.Should().BeFalse();
    }

    [Fact]
    public async Task CanBuildSet_ColorMismatch_ReturnsFalse()
    {
        // Arrange
        var set = new Set
        {
            Pieces = new List<Piece>
            {
                new()
                {
                    Part = new Part { DesignID = "3001", Material = 3, PartType = "rigid" },
                    Quantity = 5
                }
            }
        };

        var user = new User
        {
            Collection = new List<UserCollectionPiece>
            {
                new()
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new() { Color = "2", Count = 10 }
                    }
                }
            }
        };

        var setServiceMock = new Mock<ISetService>();
        var service = new BuildableSetService(setServiceMock.Object);

        // Act
        var canBuild = service.CanUserBuildSet(user, set);

        // Assert
        canBuild.Should().BeFalse();
    }
}