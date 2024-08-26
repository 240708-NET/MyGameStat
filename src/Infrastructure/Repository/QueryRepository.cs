using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
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

    public virtual TEntity? GetById(Id? id)
    {
        return ctx.Find<TEntity>(id);
    }

    public virtual ICollection<TEntity> GetAll()
    {
        return [.. dbSet];
    }
}
