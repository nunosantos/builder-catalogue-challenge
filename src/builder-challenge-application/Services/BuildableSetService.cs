using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;

namespace builder_challenge_application.Services;

public class BuildableSetService(ISetService setService) : IBuildableSetService
{
    public bool CanUserBuildSet(User user, Set set)
    {
        try
        {
            if (user == null || set == null)
            {
                return false;
            }

            var userInventory = new Dictionary<(string designId, int material), int>();

            foreach (var userPiece in user.Collection)
            {
                foreach (var variant in userPiece.Variants)
                {
                    var key = (userPiece.PieceId, variant.Color);
                    if (userInventory.ContainsKey(key))
                    {
                        userInventory[key] += variant.Count;
                    }
                    else
                    {
                        userInventory[key] = variant.Count;
                    }
                }
            }

            foreach (var requiredPiece in set.Pieces)
            {
                var key = (requiredPiece.Part.DesignID, requiredPiece.Part.Material);
                if (!userInventory.TryGetValue(key, out var ownedCount) ||
                    ownedCount < requiredPiece.Quantity)
                {
                    return false; // Missing or insufficient quantity
                }
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IEnumerable<Set> GetBuildableSetsForUser(User user)
    {
        var allSets = setService.GetAllSets();
        return allSets.Where(s => CanUserBuildSet(user, s));
    }
}