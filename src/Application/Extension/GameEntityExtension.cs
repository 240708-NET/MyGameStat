using MyGameStat.Application.DTO;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Extension;

public static class GameExtension
{
    public static GameDto ToDto(this Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Genre = game.Genre,
            ReleaseDate = game.ReleaseDate,
            Developer = game.Developer,
            Publisher = game.Publisher,
            Platforms = game.Platforms
        };
    }

    public static ICollection<GameDto> ToDto(this ICollection<Game> games)
    {
        ICollection<GameDto> gameDtos = [];
        foreach(var game in games)
        {
            gameDtos.Add(
                game.ToDto()
            );
        }
        return gameDtos;
    }

    public static Game ToModel(this GameDto dto)
    {
        return new Game
        {
            Id = dto.Id,
            Title = dto.Title,
            Genre = dto.Genre,
            ReleaseDate = dto.ReleaseDate,
            Developer = dto.Developer,
            Publisher = dto.Publisher,
            Platforms = dto.Platforms
        };
    }
}
