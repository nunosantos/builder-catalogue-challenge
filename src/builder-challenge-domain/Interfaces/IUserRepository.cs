using builder_challenge_domain.Entities;

namespace builder_challenge_domain.Interfaces;

public interface IUserRepository
{
    IEnumerable<User> GetUsers();
    User GetUserDetails(string username);
}