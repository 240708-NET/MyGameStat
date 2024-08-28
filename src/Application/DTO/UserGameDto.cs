using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.DTO;

public class UserGameDto
{
    public string? Id { get; set; }
    public required Status Status { get; set; }
    public required string Title { get; set; }
    public required string Genre { get; set; }
    public required DateOnly ReleaseDate { get; set; }
    public required string Developer { get; set; }
    public required string Publisher { get; set; }
    public required string PlatformName { get; set; }
    public required string PlatformManufacturer { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    [JsonIgnore]
    public string? GameId { get; set; }
    [JsonIgnore]
    public string? PlatformId { get; set; }
}
