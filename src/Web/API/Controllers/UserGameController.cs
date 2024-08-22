using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyGameStat.Application.DTO;
using MyGameStat.Application.Extension;
using MyGameStat.Application.Repository;

namespace MyGameStat.Web.API.Controllers;

[ApiController]
[Route("api/usergames")]
public class UserGameController(IUserGameRepository repository) : ControllerBase
{
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var game = await repository.GetById(id);

        return game is not null ? Ok(game) : NotFound($"Usergame with id {id} not found");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserGames([FromQuery(Name="username")] string? userName)
    {
        // Filtering by username
        if(!string.IsNullOrWhiteSpace(userName))
        {
             var games = await repository.GetUserGamesByUsername(userName);

             return !games.IsNullOrEmpty() ? Ok(games) : NotFound($"No games found for user {userName}");
        }

        // TODO: Change so that only users with admin role allowed to get all user games.
        var allGames = await repository.GetAll();

        return !allGames.IsNullOrEmpty() ? Ok(allGames) : NotFound("No games found for the user");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateUserGame([FromBody] UserGameDto dto)
    {
        var userGame = dto.ToModel();

        var userName = User.Identity?.Name;
        if(string.IsNullOrWhiteSpace(userName))
        {
            return UnprocessableEntity();
        }

        userGame.SetCreateAudits(userName);

        int createCount = await repository.Create(userGame);

        return createCount == 1 ? Created() : UnprocessableEntity();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateUserGame([FromBody] UserGameDto dto)
    {
        var userGame = dto.ToModel();

        var userName = User.Identity?.Name;
        if(string.IsNullOrWhiteSpace(userName))
        {
            return UnprocessableEntity();
        }

        userGame.SetUpdateAudits(userName);

        int updateCount = await repository.Update(userGame);

        return updateCount == 1 ? Ok(): UnprocessableEntity();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserGame(string id)
    {
        await repository.Delete(id);
        return Ok(id);
    }
}
