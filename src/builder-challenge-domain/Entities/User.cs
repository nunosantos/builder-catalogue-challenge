using System;

namespace builder_challenge_domain.Entities;

public record User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Location { get; set; }
    public int BrickCount { get; set; }
}
