using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Common;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class Repository<TEntity, Id> : QueryRepository<TEntity, Id>, IRepository<TEntity, Id> where TEntity : BaseEntity<Id>
{
    public Repository(ApplicationDbContext ctx) : base(ctx) {}

    public virtual async Task<int> Create(TEntity entity)
    {
        dbSet.Add(entity);
        return await ctx.SaveChangesAsync();
    }

    public virtual async Task Delete(Id id)
    {
        var entity = await GetById(id);
        if (entity is null)
        {
            return;
        }

        if(ctx.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }

        dbSet.Remove(entity);
        await ctx.SaveChangesAsync();
    }

    public virtual async Task<int> Update(TEntity _new)
    {
        Id? id = _new.Id;
        if(id is null)
        {
            return 0;
        }

        var _old = await GetById(id);
        if(_old is null)
        {
            return 0;
        }
        
        ctx.Entry(_old).CurrentValues.SetValues(_new);
        //context.Entry(entity).State = EntityState.Modified;
        return await ctx.SaveChangesAsync();
    }
}
