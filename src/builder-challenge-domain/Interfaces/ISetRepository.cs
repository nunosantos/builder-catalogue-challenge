using builder_challenge_domain.Entities;

namespace builder_challenge_domain.Interfaces;

public interface ISetRepository
{
    Task<List<Set>> GetAllSetsAsync();

    Task<Set> GetSetByNameAsync(string name);

    Task<Set> GetSetByIdAsync(Guid id);
}