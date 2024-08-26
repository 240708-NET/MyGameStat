using MyGameStat.Application.Repository;
using MyGameStat.Domain.Entity;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class PlatformRepository(ApplicationDbContext ctx) : Repository<Platform, string>(ctx), IPlatformRepository
{
    public override Platform? Retrieve(Platform platform)
    {
        return dbSet.SingleOrDefault(e => 
                    e.Name.Equals(platform.Name) && 
                    e.Manufacturer.Equals(platform.Manufacturer));
    }
}
