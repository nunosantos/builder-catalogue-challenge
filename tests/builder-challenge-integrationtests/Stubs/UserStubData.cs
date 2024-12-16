using System;
using builder_challenge_domain.Entities;

namespace builder_challenge_integrationtests.Stubs;

public static class UserStubData
{
    public static IEnumerable<User> GetDefaultUser()
    {
        yield return new User
        {
            Id = Guid.NewGuid(),
            Username = "Test User",
            Location = "CPH",
            BrickCount = 1000
        };
    }
}
