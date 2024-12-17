using System.Net;
using System.Net.Mime;
using builder_challenge_application.DTOs;
using builder_challenge_application.Interfaces;
using builder_challenge_application.Services;
using builder_challenge_utilities.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace builder_challenge_api.Endpoints.Queries;

public class GetBuildableSetsByUser : AsynchronousConnection.WithRequestType<string>.WithResponseType<IEnumerable<BuildableSet>>
{
    private readonly IBuildableSetService _buildableSetService;
    private readonly IUserService _userService;

    public GetBuildableSetsByUser(IBuildableSetService buildableSetService, IUserService userService)
    {
        _buildableSetService = buildableSetService;
        _userService = userService;
    }
    
    [HttpGet("/buildable-sets/{username}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public override async Task<ActionResult<IEnumerable<BuildableSet>>> HandleAsync(string username)
    {
        try
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }
            
            var users = await _userService.GetAllUsersAsync();
            
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }
            
            var userResponse = users.First(u => u.Username == username);
            
            if (userResponse == null)
            {
                return NotFound("No user found with that username.");
            }

            userResponse = await _userService.GetUserByIdAsync(userResponse.Id).ConfigureAwait(false);
            
            var buildableSets = await _buildableSetService.GetBuildableSetsForUser(userResponse).ConfigureAwait(false);
            
            var dto = buildableSets.Select(set => new BuildableSet
            {
                Name = set.Name
            });
            
            return Ok(dto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}