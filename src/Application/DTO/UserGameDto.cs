using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.DTO;

public class UserGameDto
{
    public string? Id;

    // TODO: Make non-nullable
    public required string? UserId { get; set; }

    public required string GameId { get; set; }

    public required Status Status { get; set; }
}
