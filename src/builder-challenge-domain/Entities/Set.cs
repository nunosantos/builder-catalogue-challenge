using System;

namespace builder_challenge_domain.Entities;

public class Set
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string SetNumber { get; set; }
    public List<Piece> Part { get; set; }
    public int TotalPieces { get; set; }
}
