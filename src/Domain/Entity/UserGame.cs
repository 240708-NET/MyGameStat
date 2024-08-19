using MyGameStat.Domain.Common;

namespace MyGameStat.Domain.Entity;

public enum Status
{
    Owned,
    Wishlist,
    Playing,
    Completed
}

public class UserGame : AuditableEntity<string>
{
    public required string GameId { get; set; }

    public required Status Status { get; set; }

    // Navigation properties
    public required User User { get; set; }
    public required Game Game { get; set; }
}