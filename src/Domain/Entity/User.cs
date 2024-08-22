using Microsoft.AspNetCore.Identity;

namespace MyGameStat.Domain.Entity;

public class User : IdentityUser
{
    // Navigation properties
    public ICollection<UserGame> UserGames { get; } = [];
}
