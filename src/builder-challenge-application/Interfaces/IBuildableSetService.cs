using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface IBuildableSetService
{
    bool CanUserBuildSet(User user, Set set);
    IEnumerable<Set> GetBuildableSetsForUser(User user);
}