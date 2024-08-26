using Microsoft.EntityFrameworkCore;
using MyGameStat.Application.Repository;
using MyGameStat.Domain.Common;
using MyGameStat.Infrastructure.Persistence;

namespace MyGameStat.Infrastructure.Repository;

public class Repository<TEntity, Id> : QueryRepository<TEntity, Id>, IRepository<TEntity, Id> where TEntity : BaseEntity<Id>
{
    public Repository(ApplicationDbContext ctx) : base(ctx) {}

    public virtual Id? Save(TEntity entity)
    {
        dbSet.Add(entity);
        ctx.SaveChanges();
        return entity.Id;
    }

    public virtual void Delete(Id id)
    {
        var entity = GetById(id);
        if (entity is null)
        {
            return;
        }

        if(ctx.Entry(entity).State == EntityState.Detached)
        {
            dbSet.Attach(entity);
        }

        dbSet.Remove(entity);
        ctx.SaveChanges();
    }

    public virtual int Update(TEntity update)
    {
        Id? id = update.Id;
        if(id is null)
        {
            return 0;
        }

        var outdate = GetById(id);
        if(outdate is null)
        {
            return 0;
        }
        
        ctx.Entry(outdate).CurrentValues.SetValues(update);
        ctx.Entry(outdate).State = EntityState.Modified;
        return ctx.SaveChanges();
    }

    public virtual TEntity? Retrieve(TEntity entity)
    {
        // TODO: Does Find() handle a null argument?
        return dbSet.Find(entity.Id);
    }
}
