using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
    public IActionResult GetUserGames([FromQuery(Name="status")] Status? status)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userGames = userGameService.GetByUserIdAndStatus(userId, status);
        var dto = userGames.ToDto();
        return !userGames.IsNullOrEmpty() ? Ok(dto) : NotFound("No user games found");
    }

    [Authorize]
    [HttpPost("games")]
    public IActionResult CreateUserGame([FromBody] UserGameDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userGameId = userGameService.Upsert(userId, dto.ToModel());
        if(userGameId == null)
        {
            return UnprocessableEntity($"userId: {userId}");
        }
        return Created();
    }

    [Authorize]
    [HttpPut("games/{id}")]
    public IActionResult UpdateUserGame(string id, [FromBody] UserGameDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        dto.Id = id;
        userGameService.Update(userId, dto.ToModel());
        return Ok();
    }

    [Authorize]
    [HttpDelete("games/{id}")]
    public IActionResult DeleteUserGame(string id)
    {
        userGameService.Delete(id);
        return Ok();
    }
}
