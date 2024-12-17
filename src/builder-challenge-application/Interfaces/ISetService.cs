using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface ISetService
{
    Task<Set> GetSetDetails(Guid setId);
    Task<IEnumerable<Set>> GetAllSets();
}