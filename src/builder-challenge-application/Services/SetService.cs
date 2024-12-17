using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;

namespace builder_challenge_application.Services;

public class SetService(ISetRepository setRepository) : ISetService
{
    public Set GetSetDetails(string setName)
    {
        try
        {
            return setRepository.GetSetDetails(setName);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the set", ex);
        }
    }

    public IEnumerable<Set> GetAllSets()
    {
        throw new NotImplementedException();
    }
}