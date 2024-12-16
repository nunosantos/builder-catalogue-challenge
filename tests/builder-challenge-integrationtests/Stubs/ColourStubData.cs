using System;
using builder_challenge_domain.Entities;

namespace builder_challenge_integrationtests.Stubs;

public static class ColourStubData
{
    public static IEnumerable<Colour> GetColours()
    {
        yield return new Colour
        {
            Name = "Red",
            Code = 2
        };
        yield return new Colour
        {
            Name = "Very Light Gray",
            Code = 49
        };
        yield return new Colour
        {
            Name = "Very Light Bluish Gray",
            Code = 99
        };
        yield return new Colour
        {
            Name = "Light Bluish Gray",
            Code = 86
        };
    }
}
