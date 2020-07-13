using ChatApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ChatApp.Domain.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<List<Message>> GetMessages(Expression<Func<Message, bool>> predicate);
    }
}
