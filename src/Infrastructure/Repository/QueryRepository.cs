using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Common;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class QueryRepository<TEntity, Id> : IQueryRepository<TEntity, Id> where TEntity : class
{
    protected readonly ApplicationDbContext ctx;
    protected readonly DbSet<TEntity> dbSet;

    public QueryRepository(ApplicationDbContext ctx)
    {
        this.ctx = ctx;
        dbSet = ctx.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetById(Id id)
    {
        return await ctx.FindAsync<TEntity>(id);
    }

    public virtual async Task<ICollection<TEntity>> GetAll()
    {
        return await dbSet.ToListAsync();
    }
}
