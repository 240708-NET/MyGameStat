using Microsoft.AspNetCore.Identity;
using MyGameStat.Domain.Common;

namespace MyGameStat.Domain.Entity;

public class User : IdentityUser
{
    // Navigation properties
    public ICollection<UserGame> UserGames { get; } = [];
}