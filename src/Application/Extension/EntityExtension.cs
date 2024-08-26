using MyGameStat.Domain.Common;
using MyGameStat.Domain.Entity;

namespace MyGameStat.Application.Extension;

public static class EntityExtension
{
    public static void SetCreateAuditValues(this AuditableEntity<string> entity, string userId)
    {
        entity.CreatorId = userId;
        entity.CreateDate = DateTimeOffset.UtcNow;
        entity.UpdaterId = userId;
        entity.UpdateDate = DateTimeOffset.UtcNow;
    }

    public static void SetUpdateAuditValues(this AuditableEntity<string> entity, string userId)
    {
        entity.UpdaterId = userId;
        entity.UpdateDate = DateTimeOffset.UtcNow;
    }

    public static void SetValues(this UserGame target, UserGame source)
    {
        target.Game = source.Game;
        target.Platform = source.Platform;
        target.Status = source.Status;
    }

    public static void UpdateValues(this UserGame target, UserGame source)
    {
        target.Platform.UpdateValues(source.Platform);
        target.Game.UpdateValues(source.Game);
        target.Status = source.Status;
    }

    public static void SetCreateAuditValues(this UserGame userGame, string userId)
    {
        ((AuditableEntity<string>) userGame).SetCreateAuditValues(userId);
        userGame.Game.SetCreateAuditValues(userId);
        userGame.Platform.SetCreateAuditValues(userId);
    }

    public static void SetUpdateAuditValues(this UserGame userGame, string userId)
    {
        ((AuditableEntity<string>) userGame).SetUpdateAuditValues(userId);
        userGame.Game.SetUpdateAuditValues(userId);
        userGame.Platform.SetUpdateAuditValues(userId);
    }

    private static void UpdateValues(this Platform target, Platform source)
    {
        target.UpdateDate = source.UpdateDate;
        target.UpdaterId = source.UpdaterId;
        target.Manufacturer = source.Manufacturer;
        target.Name = source.Name;
    }

    private static void UpdateValues(this Game target, Game source)
    {
        target.UpdateDate = source.UpdateDate;
        target.UpdaterId = source.UpdaterId;
        target.Title = source.Title;
        target.Developer = source.Developer;
        target.Genre = source.Genre;
        target.Platforms = source.Platforms;
        target.Publisher = source.Publisher;
        target.ReleaseDate = source.ReleaseDate;
    }

    public static void AddPlatform(this Game game, Platform newPlatform)
    {
        foreach(var platform in game.Platforms)
        {
            if(platform.Same(newPlatform))
            {
                if(platform.Id is null)
                {
                    game.Platforms.Remove(platform);
                    game.Platforms.Add(newPlatform);
                }
                return;
            }
        }
        game.Platforms.Add(newPlatform);
    }

    private static bool Same(this Platform l, Platform r)
    {
        return l.Name.Equals(r.Name) &&
               l.Manufacturer.Equals(r.Manufacturer);
    }
}
