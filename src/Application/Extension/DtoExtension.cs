using MyGameStat.Application.DTO;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Extension;

public static class DtoExtension
{
    public static NoIdUserGameDto ToNoIdDto(this UserGame userGame)
    {
        return new NoIdUserGameDto
        {
            Title = userGame.Game.Title,
            Genre = userGame.Game.Genre,
            ReleaseDate = userGame.Game.ReleaseDate,
            Developer = userGame.Game.Developer,
            Publisher = userGame.Game.Publisher,
            PlatformName = userGame.Platform.Name,
            PlatformManufacturer = userGame.Platform.Manufacturer,
            Status = userGame.Status
        };
    }

    public static UserGameDto ToDto(this UserGame userGame)
    {
        return new UserGameDto
        {
            #pragma warning disable CS8601 // Possible null reference assignment.
            Id = userGame.Id,
            Title = userGame.Game.Title,
            Genre = userGame.Game.Genre,
            ReleaseDate = userGame.Game.ReleaseDate,
            Developer = userGame.Game.Developer,
            Publisher = userGame.Game.Publisher,
            PlatformName = userGame.Platform.Name,
            PlatformManufacturer = userGame.Platform.Manufacturer,
            Status = userGame.Status,
            UserId = userGame.CreatorId,
            GameId = userGame.Game.Id,
            PlatformId = userGame.Platform.Id
            #pragma warning restore CS8601 // Possible null reference assignment.
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

    public static UserGame ToModel(this NoIdUserGameDto dto)
    {
        #pragma warning disable CS8601 // Possible null reference assignment.
        return new UserGame
        {
            Status = dto.Status,
            Platform = new Platform
            {
                Name = dto.PlatformName,
                Manufacturer = dto.PlatformManufacturer
            },
            Game = new Game
            {
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
