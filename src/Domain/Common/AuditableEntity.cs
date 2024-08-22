using System.Text.Json.Serialization;

namespace MyGameStat.Domain.Common;

public abstract class AuditableEntity<T> : BaseEntity<T>
{
    [JsonIgnore]
    public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;

    // TODO: Make non-nullable
    public T? CreatorId { get; set; }

    [JsonIgnore]
    public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.UtcNow;

    public T? UpdaterId { get; set; }
}
