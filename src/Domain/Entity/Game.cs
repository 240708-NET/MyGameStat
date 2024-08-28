using System.ComponentModel.DataAnnotations;
using MyGameStat.Domain.Common;

namespace MyGameStat.Domain.Entity;

public class Game : AuditableEntity<string>
{
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public required string Developer { get; set; }
    public required string Publisher { get; set; }
    public ICollection<Platform> Platforms { get; set; } = [];
}
