using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.DTO;

public class NoIdUserGameDto
{
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required DateOnly ReleaseDate { get; set; }
    public required string Developer { get; set; }
    public required string Publisher { get; set; }
    public required string PlatformName { get; set; }
    public required string PlatformManufacturer { get; set; }
    public required Status Status { get; set; }
}

public class UserGameDto : NoIdUserGameDto
{
    public required string Id { get; set; }
    public required string UserId { get; set; }
    public required string GameId { get; set; }
    public required string PlatformId { get; set; }
}
