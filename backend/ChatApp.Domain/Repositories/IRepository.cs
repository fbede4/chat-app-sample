namespace ChatApp.Domain.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        TEntity Insert(TEntity entity);
    }
}
