using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;

namespace builder_challenge_application.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public User GetUserDetails(string username)
    {
        try
        {
            return userRepository.GetUserDetails(username);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the user", ex);
        }
    }

    public IEnumerable<User> GetUsers()
    {
        try
        {
            return userRepository.GetUsers();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the set", ex);
        }
    }
}