using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyGameStat.Application.DTO;
using MyGameStat.Application.Extension;
using MyGameStat.Application.Service;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Web.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserGameController(IUserGameService<UserGame, string> userGameService) : ControllerBase
{
    [Authorize]
    [HttpGet("games")]
    public IActionResult GetUserGames(
        [FromQuery(Name="status")] Status? status,
        [FromQuery(Name="genre")] string? genre,
        [FromQuery(Name="platformName")] string? platformName
        )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userGames = userGameService.GetByUserIdAndFilter(userId, status, genre, platformName);
        var dto = userGames.ToDto();
        return dto.Any() ? Ok(dto) : NotFound($"No usergames found for user: {userId}.");
    }

    [Authorize]
    [HttpPost("games")]
    public IActionResult CreateUserGame([FromBody] NoIdUserGameDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userGame = userGameService.Upsert(userId, dto.ToModel());
        if(userGame == null)
        {
            return UnprocessableEntity($"Failed to create usergame for user: {userId}.");
        }
        return base.Created(Request.GetEncodedUrl() + $"/{userGame.Id}", userGame.ToDto());
    }

    [Authorize]
    [HttpPut("games/{id}")]
    public IActionResult UpdateUserGame(string id, [FromBody] NoIdUserGameDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userGame = dto.ToModel();
        userGame.Id = id;
        int updates = userGameService.Update(userId, userGame);
        if(updates > 0)
        {
            return Ok($"Updated usergame: {id} for user: {userId}.");
        }
        return UnprocessableEntity($"Failed to update usergame: {id} for user: {userId}.");
    }

    [Authorize]
    [HttpDelete("games/{id}")]
    public IActionResult DeleteUserGame(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        int deletes = userGameService.Delete(id);
        if(deletes > 0)
        {
            return Ok($"Deleted usergame: {id} for user: {userId}.");
        }
        return UnprocessableEntity($"Failed to delete usergame: {id} for user: {userId}. May have been deleted already.");
    }
}
