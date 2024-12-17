using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;

namespace builder_challenge_application.Services;

public class SetService(ISetRepository setRepository) : ISetService
{
    public async Task<Set> GetSetDetails(Guid setId)
    {
        try
        {
            return await setRepository.GetSetByIdAsync(setId);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the set", ex);
        }
    }

    public async Task<IEnumerable<Set>> GetAllSets()
    {
        try
        {
            return await setRepository.GetAllSetsAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); //or logging of some sort
            throw;
        }
    }
}