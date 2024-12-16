using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface IUserService
{
    User GetUserDetails(string username);
    IEnumerable<User> GetUsers();
    
}