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
    public User? User { get; set; }
    public required Game Game { get; set; }
    public required Platform Platform { get; set; }
    public required Status Status { get; set; }
}
