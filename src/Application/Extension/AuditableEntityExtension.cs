using MyGameStat.Domain.Common;

namespace MyGameStat.Application.Extension;

public static class AuditableEntityExtension
{
    public static void SetCreateAudits(this AuditableEntity<string> entity, string userName)
    {
        entity.CreatorId = userName;
        entity.CreateDate = DateTimeOffset.UtcNow;
        entity.UpdaterId = userName;
        entity.UpdateDate = DateTimeOffset.UtcNow;
    }

    public static void SetUpdateAudits(this AuditableEntity<string> entity, string userName)
    {
        entity.UpdaterId = userName;
        entity.UpdateDate = DateTimeOffset.UtcNow;
    }
}
