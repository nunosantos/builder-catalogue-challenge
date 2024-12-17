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

            var userInventory = new Dictionary<(string designId, string material), int>();

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
    
    public async Task<IEnumerable<Set>> GetBuildableSetsForUser(User user)
    {
        var allSets = await setService.GetAllSets().ConfigureAwait(false);
        
        var buildableSets = new List<Set>();

        foreach (var set in allSets)
        {
            await AddBuildableSetIfEligibleAsync(user, set, buildableSets);
        }
        
        return buildableSets;
    }

    private async Task AddBuildableSetIfEligibleAsync(User user, Set set, List<Set> buildableSets)
    {
        var detailedSet = await setService.GetSetDetails(set.Id).ConfigureAwait(false);

        if (CanUserBuildSet(user, detailedSet))
        {
            buildableSets.Add(detailedSet);
        }
    }

    private static bool HasSufficientPieces(Piece requiredPiece, Dictionary<(string designId, string material), int> userInventory)
    {
        var key = (requiredPiece.Part.DesignID, requiredPiece.Part.Material.ToString());
        
        var hasSufficientPieces = userInventory.TryGetValue(key, out var ownedCount) &&
                                  ownedCount >= requiredPiece.Quantity;
        
        return hasSufficientPieces;
    }

    private static void AddUserPieceToInventory(UserCollectionPiece userPiece, PieceVariant variant,
        Dictionary<(string designId, string material), int> userInventory)
    {
        var key = (userPiece.PieceId, variant.Color);
        if (userInventory.ContainsKey(key))
            userInventory[key] += variant.Count;
        else
            userInventory[key] = variant.Count;
    }
}