using System.Net;
using System.Net.Mime;
using builder_challenge_application.DTOs;
using builder_challenge_utilities.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace builder_challenge_api.Endpoints.Queries;

public class GetBuildableSetsByUser : AsynchronousConnection.WithRequestType<string>.WithResponseType<BuildableSet>
{
    [HttpGet("/buildable-sets/{username}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<BuildableSet>> HandleAsync(string username)
    {
        try
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
        
            return Ok(new BuildableSet());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}