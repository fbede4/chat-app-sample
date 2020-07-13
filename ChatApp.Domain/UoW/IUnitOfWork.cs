using System.Threading.Tasks;

namespace ChatApp.Domain.UoW
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
