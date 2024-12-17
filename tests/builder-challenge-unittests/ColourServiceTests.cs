using builder_challenge_application.Services;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using FluentAssertions;
using Moq;

namespace builder_challenge_unittests;

public class ColourServiceTests
{
    [Fact]
    public async Task GetColourDetails_ReturnsColours_WhenSetNameIsValid()
    {
        // Arrange
        var colourRepository = new Mock<IColourRepository>();
        colourRepository.Setup(c => c.GetColours()).ReturnsAsync(new List<Colour>
        {
            new Colour { Name = "Red", Code = 1 },
            new Colour { Name = "Green", Code = 2 },
            new Colour { Name = "Blue", Code = 3 }
        });
        
        var colourService = new ColourService(colourRepository.Object);

        // Act
        var colours = await colourService.GetColourDetails().ConfigureAwait(false);

        // Assert
        colours.Should().NotBeNull();
        colours.Should().HaveCount(3);
    }

    [Fact]
    public async Task GetColourDetails_ThrowsException_WhenSetNameIsInvalid()
    {
        // Arrange
        var colourRepository = new Mock<IColourRepository>();
        colourRepository.Setup(c => c.GetColours()).Throws(new Exception());
        var colourService = new ColourService(colourRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () => await colourService.GetColourDetails());
    }
}
