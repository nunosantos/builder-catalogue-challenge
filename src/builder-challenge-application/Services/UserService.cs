using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;

namespace builder_challenge_application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        try
        {
            return await userRepository.GetUserByUsernameAsync(username).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the user", ex);
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            return await userRepository.GetAllUsersAsync().ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the set", ex);
        }
    }

    public async Task<User> GetUserByIdAsync(Guid userResponseId)
    {
        try
        {
            return await userRepository.GetUserByIdAsync(userResponseId).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the set", ex);
        }
    }
}