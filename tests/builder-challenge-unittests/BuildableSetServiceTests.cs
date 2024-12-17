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
                new Piece
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
                new UserCollectionPiece
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new PieceVariant { Color = 3, Count = 5 } // exact match
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
    public void CanBuildSet_SurplusInventory_ReturnsTrue()
    {
        // Arrange
        var set = new Set
        {
            Id = Guid.NewGuid(),
            Name = "Surplus Set",
            SetNumber = "67890",
            Pieces = new List<Piece>
            {
                new Piece
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
                new UserCollectionPiece
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new PieceVariant { Color = 2, Count = 10 } // more than needed
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
    public void CanBuildSet_InsufficientQuantity_ReturnsFalse()
    {
        // Arrange
        var set = new Set
        {
            Pieces = new List<Piece>
            {
                new Piece
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
                new UserCollectionPiece
                {
                    PieceId = "3005",
                    Variants = new List<PieceVariant>
                    {
                        new PieceVariant { Color = 5, Count = 9 } // one short
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
    public void CanBuildSet_MissingPiece_ReturnsFalse()
    {
        // Arrange
        var set = new Set
        {
            Pieces = new List<Piece>
            {
                new Piece
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
                new UserCollectionPiece
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new PieceVariant { Color = 4, Count = 10 }
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
    public void CanBuildSet_ColorMismatch_ReturnsFalse()
    {
        // Arrange
        var set = new Set
        {
            Pieces = new List<Piece>
            {
                new Piece
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
                // User has the piece but in a different color (material)
                new UserCollectionPiece
                {
                    PieceId = "3001",
                    Variants = new List<PieceVariant>
                    {
                        new PieceVariant { Color = 2, Count = 10 }
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
    public void CanBuildMultipleSets_ReturnsTrueForAllApplicableSets()
    {
        // Arrange
            var set1 = new Set
            {
                Id = Guid.NewGuid(),
                Name = "Set1",
                Pieces = new List<Piece>
                {
                    new Piece { Part = new Part { DesignID = "3001", Material = 2, PartType = "rigid" }, Quantity = 4 }
                }
            };

            var set2 = new Set
            {
                Id = Guid.NewGuid(),
                Name = "Set2",
                Pieces = new List<Piece>
                {
                    new Piece { Part = new Part { DesignID = "3005", Material = 3, PartType = "rigid" }, Quantity = 3 }
                }
            };

            var set3 = new Set
            {
                Id = Guid.NewGuid(),
                Name = "Set3",
                Pieces = new List<Piece>
                {
                    new Piece { Part = new Part { DesignID = "3001", Material = 2, PartType = "rigid" }, Quantity = 4 },
                    new Piece { Part = new Part { DesignID = "3005", Material = 3, PartType = "rigid" }, Quantity = 3 },
                    new Piece { Part = new Part { DesignID = "9999", Material = 1, PartType = "rigid" }, Quantity = 1 } // not available
                }
            };

            var user = new User
            {
                Collection = new List<UserCollectionPiece>
                {
                    new UserCollectionPiece
                    {
                        PieceId = "3001",
                        Variants = new List<PieceVariant>
                        {
                            new PieceVariant { Color = 2, Count = 10 }
                        }
                    },
                    new UserCollectionPiece
                    {
                        PieceId = "3005",
                        Variants = new List<PieceVariant>
                        {
                            new PieceVariant { Color = 3, Count = 5 }
                        }
                    }
                }
            };

            var sets = new List<Set> { set1, set2, set3 };

            var setServiceMock = new Mock<ISetService>();
            setServiceMock.Setup(s => s.GetAllSets()).Returns(sets);

            var service = new BuildableSetService(setServiceMock.Object);

            // Act
            var buildableSets = service.GetBuildableSetsForUser(user);

            // Assert
            buildableSets.Should().Contain(set1);
            buildableSets.Should().Contain(set2);
            buildableSets.Should().NotContain(set3);
    }
}