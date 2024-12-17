using builder_challenge_domain.Entities;

namespace builder_challenge_domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> GetUserByIdAsync(Guid id);
}