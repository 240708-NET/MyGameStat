using MyGameStat.Application.DTO;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Extension;

public static class DtoExtension
{
    public static UserGameDto ToDto(this UserGame userGame)
    {
        return new UserGameDto
        {
            Id = userGame.Id,
            UserId = userGame.CreatorId,
            GameId = userGame.Game.Id,
            PlatformId = userGame.Platform.Id,
            Status = userGame.Status,
            PlatformName = userGame.Platform.Name,
            PlatformManufacturer = userGame.Platform.Manufacturer,
            Title = userGame.Game.Title,
            Genre = userGame.Game.Genre,
            ReleaseDate = userGame.Game.ReleaseDate,
            Developer =  userGame.Game.Developer,
            Publisher = userGame.Game.Publisher,
        };
    }

    public static ICollection<UserGameDto> ToDto(this ICollection<UserGame> userGames)
    {
        ICollection<UserGameDto> userGameDtos = [];
        foreach(var userGame in userGames)
        {
            userGameDtos.Add(userGame.ToDto());
        }
        return userGameDtos;
    }

    
    public static UserGame ToModel(this UserGameDto dto)
    {
        #pragma warning disable CS8601 // Possible null reference assignment.
        return new UserGame
        {
            Id = dto.Id,
            CreatorId = dto.Id,
            Status = dto.Status,
            Platform = new Platform
            {
                CreatorId = dto.Id,
                Name = dto.PlatformName,
                Manufacturer = dto.PlatformManufacturer
            },
            Game = new Game
            {
                CreatorId = dto.Id,
                Id = dto.GameId,
                Title = dto.Title,
                Genre = dto.Genre,
                ReleaseDate = dto.ReleaseDate,
                Developer =  dto.Developer,
                Publisher = dto.Publisher
            }
        };
        #pragma warning restore CS8601 // Possible null reference assignment.
    }
}
