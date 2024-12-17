using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface ISetService
{
    Set GetSetDetails(string setName);
    IEnumerable<Set> GetAllSets();
}