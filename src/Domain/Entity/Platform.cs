using MyGameStat.Domain.Common;

namespace MyGameStat.Domain.Entity;

public class Platform : AuditableEntity<string>
{
    public required string Name { get; set; }
    public required string Manufacturer { get; set; }
    public List<Game> Games { get; } = [];
}
