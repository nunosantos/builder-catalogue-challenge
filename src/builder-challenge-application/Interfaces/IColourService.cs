using builder_challenge_domain.Entities;

namespace builder_challenge_application.Interfaces;

public interface IColourService
{
    Task<IEnumerable<Colour>> GetColourDetails();
}