using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyGameStat.Application.Repository;

namespace MyGameStat.Web.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserRepository<string> repository) : ControllerBase
{
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await repository.GetById(id);
        return user is not null ? Ok(user) : NotFound($"User with id {id} not found");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers([FromQuery(Name="username")] string? userName)
    {
        if (userName is not null)
        {
             var user = await repository.GetByUserName(userName);
             return user is not null ? Ok(user) : NotFound($"User {userName} not found");
        }
        var users = await repository.GetAll();
        return users.IsNullOrEmpty() ? NotFound("No users found") : Ok(users);
    }
}
