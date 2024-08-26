using Microsoft.AspNetCore.Identity;

namespace MyGameStat.Domain.Entity;

public class User : IdentityUser
{
    public List<UserGame> UserGames { get; } = [];
}
