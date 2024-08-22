using MyGameStat.Application.DTO;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Extension;

public static class UserGameExtension
{
    public static UserGameDto ToDto(this UserGame userGame)
    {
        return new UserGameDto
        {
            Id = userGame.Id,
            UserId = userGame.CreatorId,
            GameId = userGame.GameId,
            Status = userGame.Status
        };
    }

    public static ICollection<UserGameDto> ToDto(this ICollection<UserGame> userGames)
    {
        ICollection<UserGameDto> userGameDtos = [];
        foreach(var userGame in userGames)
        {
            userGameDtos.Add(
                userGame.ToDto()
            );
        }
        return userGameDtos;
    }

    public static UserGame ToModel(this UserGameDto dto)
    {
        return new UserGame
        {
            Id = dto.Id,
            CreatorId = dto.UserId,
            GameId = dto.GameId,
            Status = dto.Status
        };
    }
}
