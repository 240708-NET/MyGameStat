using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.DTO;

public class GameDto
{
    public string? Id { get; set; }

    public required string Title { get; set; }

    public required string Genre { get; set; }

    public DateTimeOffset ReleaseDate { get; set; }

    public required string Developer { get; set; }

    public required string Publisher { get; set; }

    public ICollection<Platform> Platforms { get; set; } = [];
}
