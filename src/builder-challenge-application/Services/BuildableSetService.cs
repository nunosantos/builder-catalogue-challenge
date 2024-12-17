using builder_challenge_application.Interfaces;
using builder_challenge_domain.Entities;

namespace builder_challenge_application.Services;

public class BuildableSetService(ISetService setService) : IBuildableSetService
{
    public bool CanUserBuildSet(User user, Set set)
    {
        try
        {
            if (user == null || set == null) return false;

            var userInventory = new Dictionary<(string designId, int material), int>();

            foreach (var userPiece in user
                         .Collection
                         .SelectMany(up => up.Variants, (up, variant) => new { up, variant }))
            {
                AddUserPieceToInventory(userPiece.up, userPiece.variant, userInventory);
            }

            return set.Pieces.All(requiredPiece => HasSufficientPieces(requiredPiece, userInventory));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static bool HasSufficientPieces(Piece requiredPiece, Dictionary<(string designId, int material), int> userInventory)
    {
        var key = (requiredPiece.Part.DesignID, requiredPiece.Part.Material);

        if (!userInventory.TryGetValue(key, out var ownedCount) ||
            ownedCount < requiredPiece.Quantity)
            return false;
        return true;
    }

    public IEnumerable<Set> GetBuildableSetsForUser(User user)
    {
        var allSets = setService.GetAllSets();
        return allSets.Where(s => CanUserBuildSet(user, s));
    }

    private static void AddUserPieceToInventory(UserCollectionPiece userPiece, PieceVariant variant,
        Dictionary<(string designId, int material), int> userInventory)
    {
        var key = (userPiece.PieceId, variant.Color);
        if (userInventory.ContainsKey(key))
            userInventory[key] += variant.Count;
        else
            userInventory[key] = variant.Count;
    }
}