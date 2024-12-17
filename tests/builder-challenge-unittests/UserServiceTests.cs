using builder_challenge_application.Services;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;
using FluentAssertions;
using Moq;

namespace builder_challenge_unittests;

public class UserServiceTests
{
    [Fact]
    public async Task GetUsers_ShouldReturnListOfUsers()
    {
        // Act
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(new List<User>
        {
            new User { Username = "user1" },
            new User { Username = "user2" }
        });
        var userService = new UserService(mockUserRepository.Object);
        
        var users = await userService.GetAllUsersAsync();

        // Assert
        users.Should().NotBeNull();
        users.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetUserDetails_WithValidUsername_ShouldReturnUser()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(x => x.GetUserByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new User
        {
            Username = "user1",
            Location = "Location1",
            BrickCount = 10
        });
        var userService = new UserService(mockUserRepository.Object);
        var username = "user1";

        // Act
        var user = await userService.GetUserByUsernameAsync(username);

        // Assert
        user.Should().NotBeNull();
        user.Username.Should().Be(username);
        user.Location.Should().Be("Location1");
        user.BrickCount.Should().Be(10);
    }

    [Fact]
    public async Task GetUserDetails_WithInvalidUsername_ShouldThrowException()
    {
        // Arrange
        var mockUserRepository = new Mock<IUserRepository>();
        mockUserRepository.Setup(u => u.GetUserByUsernameAsync(It.IsAny<string>())).Throws<Exception>();
        var userService = new UserService(mockUserRepository.Object);
        var username = "invalid_user";

        // Assert is handled by ExpectedException
        await Assert.ThrowsAsync<Exception>(async () => await userService.GetUserByUsernameAsync(username));
    }
}