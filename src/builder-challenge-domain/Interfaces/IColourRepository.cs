using builder_challenge_domain.Entities;

namespace builder_challenge_domain.Interfaces;

public interface IColourRepository
{
    Task<IEnumerable<Colour>> GetColours();
}