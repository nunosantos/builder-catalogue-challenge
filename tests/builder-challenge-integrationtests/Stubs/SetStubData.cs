using builder_challenge_domain.Entities;

namespace builder_challenge_integrationtests.Stubs;

public static class SetStubData
{
    public static IEnumerable<Set> GetStubData()
    {
        yield return new Set
        {
            Id = Guid.NewGuid(),
            Name = "Set 1",
            SetNumber = "1",
            Pieces = new List<Piece>
            {
                new Piece
                {
                    Part = new Part
                    {
                        DesignID = "1",
                        Material = 1,
                        PartType = "Part 1"
                    },
                    Quantity = 1
                }
            },
            TotalPieces = 1
        };
    }
}
