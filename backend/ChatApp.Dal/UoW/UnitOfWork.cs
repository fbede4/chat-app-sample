using ChatApp.Domain.UoW;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatApp.Dal.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
