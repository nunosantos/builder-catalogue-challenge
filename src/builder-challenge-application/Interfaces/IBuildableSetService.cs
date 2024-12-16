using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface IBuildableSetService
{
    bool CanUserBuildSet(User user, CatalogSet set);
    IEnumerable<CatalogSet> GetBuildableSetsForUser(User user);
}