using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;
using builder_challenge_domain.Interfaces;

namespace builder_challenge_application.Services;

public class ColourService(IColourRepository colourRepository) : IColourService
{
    public IEnumerable<Colour> GetColourDetails()
    {
        try
        {
            return colourRepository.GetColours();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while fetching the colours", ex);
        }
    }
}