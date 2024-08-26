using MyGameStat.Application.Extension;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;

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

    public string? Upsert(string? userId, UserGame userGame)
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
            return retrievedUserGame.Id;
        }

        return userGameRepository.Save(userGame);
    }

    public void Delete(string id)
    {
        userGameRepository.Delete(id);
    }

    public ICollection<UserGame> GetByUserId(string? userId)
    {
        return userGameRepository.GetByUserId(userId);
    }

    public int Update(string? userId, UserGame upToDate)
    {
        if(string.IsNullOrWhiteSpace(userId))
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
