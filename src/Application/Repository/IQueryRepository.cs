namespace MyGameStat.Application.Repository;

public interface IQueryRepository<TEntity, Id> where TEntity : class
{
    Task<TEntity?> GetById(Id id);

    Task<ICollection<TEntity>> GetAll();
}
