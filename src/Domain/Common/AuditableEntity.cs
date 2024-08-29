using System.Text.Json.Serialization;

namespace MyGameStat.Domain.Common;

public abstract class AuditableEntity<Id> : BaseEntity<Id>
{
    [JsonIgnore]
    public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;
    public Id? CreatorId { get; set; }
    [JsonIgnore]
    public DateTimeOffset UpdateDate { get; set; } = DateTimeOffset.UtcNow;
    public Id? UpdaterId { get; set; }
}
