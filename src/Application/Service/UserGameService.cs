using MyGameStat.Application.Extension;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using static System.String;

namespace MyGameStat.Application.Service;

public class UserGameService : IUserGameService<UserGame, string>
{
    private readonly IUserGameRepository userGameRepository;
    private readonly IGameRepository gameRepository;
    private readonly IPlatformRepository platformRepository;

    public UserGameService(
        IUserGameRepository userGameRepository,
        IGameRepository gameRepository,
        IPlatformRepository platformRepository
        )
    {
        this.userGameRepository = userGameRepository;
        this.gameRepository = gameRepository;
        this.platformRepository = platformRepository;
    }

    public UserGame? Upsert(string? userId, UserGame userGame)
    {
        if(userId == null)
        {
            return null;
        }
        userGame.SetCreateAuditValues(userId);

        var retrievedGame = gameRepository.Retrieve(userGame.Game) ?? userGame.Game;
        if(retrievedGame != userGame.Game)
        {
            userGame.Game = retrievedGame;
        }

        var retrievedPlatform = platformRepository.Retrieve(userGame.Platform) ?? userGame.Platform;
        if(retrievedPlatform != userGame.Platform)
        {
            userGame.Platform = retrievedPlatform;
        }
        userGame.Game.AddPlatform(userGame.Platform);

        var retrievedUserGame = userGameRepository.Retrieve(userGame) ?? userGame;
        if(retrievedUserGame != userGame)
        {
            retrievedUserGame.SetValues(userGame);
            userGameRepository.Update(retrievedUserGame);
            return retrievedUserGame;
        }

        return userGameRepository.Save(userGame);
    }

    public int Delete(string id)
    {
        return userGameRepository.Delete(id);
    }

    public ICollection<UserGame> GetByUserIdAndFilter(string? userId, Status? status, string? genre, string? platformName)
    {
        return userGameRepository.GetByUserIdAndFilter(userId, status, genre, platformName);
    }

    public int Update(string? userId, UserGame upToDate)
    {
        if(IsNullOrWhiteSpace(userId))
        {
            return 0;
        }
        upToDate.SetUpdateAuditValues(userId);
        var outDated = userGameRepository.GetById(upToDate.Id);
        if(outDated == null)
        {
            return 0;
        }
        outDated.UpdateValues(upToDate);
        return userGameRepository.Update(outDated);
    }
}
