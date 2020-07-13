namespace ChatApp.Dal.Repositories
{
    public class RepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly ChatDbContext chatDbContext;

        public RepositoryBase(ChatDbContext chatDbContext)
        {
            this.chatDbContext = chatDbContext;
        }

        public TEntity Insert(TEntity entity)
        {
            chatDbContext.Set<TEntity>().Add(entity);
            return entity;
        }
    }
}
