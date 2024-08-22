using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyGameStat.Application.DTO;
using MyGameStat.Application.Extension;
using MyGameStat.Application.Repository;

namespace MyGameStat.Web.API.Controllers;

[ApiController]
[Route("api/games")]
public class GameController(IGameRepository repository) : ControllerBase
{
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var game = await repository.GetById(id);

        return game is not null ? Ok(game.ToDto()) : NotFound($"Game with id {id} not found");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetGames([FromQuery(Name="title")] string? title)
    {
        if(!string.IsNullOrWhiteSpace(title))
        {
             var games = await repository.GetGamesByTitle(title);

             return !games.IsNullOrEmpty() ? Ok(games.ToDto()) : NotFound($"No games found for title {title}");
        }

        var allGames = await repository.GetAll();

        return !allGames.IsNullOrEmpty() ? Ok(allGames.ToDto()) : NotFound("No games found");
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateGame([FromBody] GameDto dto)
    {
        var game = dto.ToModel();

        var userName = User.Identity?.Name;
        if(string.IsNullOrWhiteSpace(userName))
        {
            return UnprocessableEntity();
        }

        game.SetCreateAudits(userName);

        int createCount = await repository.Create(game);

        return createCount == 1 ? Created() : UnprocessableEntity();
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> UpdateGame([FromBody] GameDto dto)
    {
        var game = dto.ToModel();

        var userName = User.Identity?.Name;
        if(string.IsNullOrWhiteSpace(userName))
        {
            return UnprocessableEntity();
        }

        game.SetUpdateAudits(userName);

        int updateCount = await repository.Update(game);

        return updateCount == 1 ? Ok(): UnprocessableEntity();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(string id)
    {
        await repository.Delete(id);
        return Ok(id);
    }
}
