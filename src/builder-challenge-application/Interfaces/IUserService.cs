using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface IUserService
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task<User> GetUserByIdAsync(Guid userResponseId);
}