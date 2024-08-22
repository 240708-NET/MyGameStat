using MyGameStat.Domain.Common;

namespace MyGameStat.Domain.Entity;

public class Game : AuditableEntity<string>, IEquatable<Game>
{
    public required string Title { get; set; }

    public required string Genre { get; set; }

    public DateTimeOffset ReleaseDate { get; set; }

    public required string Developer { get; set; }

    public required string Publisher { get; set; }

    // Navigation properties
    public ICollection<Platform> Platforms { get; set; } = [];

    public override bool Equals(object? obj) => Equals(obj as Game);

    public bool Equals(Game? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }
        
        if (GetType() != other.GetType())
        {
                return false;
        }

        return Title.Equals(other.Title) &&
        Genre.Equals(other.Genre) &&
        ReleaseDate.Equals(other.ReleaseDate) &&
        Developer.Equals(other.Developer) &&
        Publisher.Equals(other.Publisher);
    }
    
    public override int GetHashCode() => (Title, Genre, ReleaseDate, Developer, Publisher).GetHashCode();

    public static bool operator ==(Game lhs, Game rhs)
    {
        if (lhs is null)
        {
            if (rhs is null)
            {
               return true;
            }
            return false;
        }
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Game lhs, Game rhs) => !(lhs == rhs);
}
