using builder_challenge_application.Services;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using FluentAssertions;
using Moq;

namespace builder_challenge_unittests;

public class SetServiceTests
{
    [Fact]
    public async Task GetSetDetails_ShouldReturnAllColours_WhenSetNameIsValid()
    {
        // Arrange
        var setId = Guid.NewGuid();
        var setRepository = new Mock<ISetRepository>();
        setRepository
            .Setup(s => s.GetSetByIdAsync(setId))
            .ReturnsAsync(new Set
            {
                Name = "setName",
                Id = setId,
                Pieces = new List<Piece>(),
                SetNumber = "12345",
                TotalPieces = 100
            });

        var setService = new SetService(setRepository.Object);

        // Act
        var result = await setService.GetSetDetails(setId).ConfigureAwait(false);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(setId);
    }


    [Fact]
    public async Task GetSetDetails_ShouldThrowException_WhenSetNotFound()
    {
        // Arrange
        var setName = "InValidSet";
        var setRepository = new Mock<ISetRepository>();
        setRepository.Setup(s => s.GetSetByIdAsync(It.IsAny<Guid>())).Throws(new Exception("Set not found"));
        var setService = new SetService(setRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await setService.GetSetDetails(It.IsAny<Guid>()));
    }
}