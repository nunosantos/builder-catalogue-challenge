namespace builder_challenge_domain.Entities;

public class UserCollectionPiece
{
    public string PieceId { get; set; }
    public List<PieceVariant> Variants { get; set; }
}