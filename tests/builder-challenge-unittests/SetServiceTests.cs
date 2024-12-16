using builder_challenge_application.Services;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using FluentAssertions;
using Moq;

namespace builder_challenge_unittests;

public class SetServiceTests
{
    [Fact]
    public void GetSetDetails_ShouldReturnAllColours_WhenSetNameIsValid()
    {
        // Arrange
        var setName = "castaway";
        var setRepository = new Mock<ISetRepository>();
        setRepository
            .Setup(s => s.GetSetDetails(setName))
            .Returns(new Set
            {
                Name = setName,
                Id = Guid.Empty,
                Pieces = new List<Piece>(),
                SetNumber = "12345",
                TotalPieces = 100
            });

        var setService = new SetService(setRepository.Object);

        // Act
        var result = setService.GetSetDetails(setName);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(setName);
    }


    [Fact]
    public void GetSetDetails_ShouldThrowException_WhenSetNotFound()
    {
        // Arrange
        var setName = "InValidSet";
        var setRepository = new Mock<ISetRepository>();
        setRepository.Setup(s => s.GetSetDetails(setName)).Throws(new Exception("Set not found"));
        var setService = new SetService(setRepository.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => setService.GetSetDetails(setName));
    }
}