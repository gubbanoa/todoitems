using Kjetil.Todo.Core.Domain;
using System.Collections.Generic;

namespace Kjetil.Todo.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<TodoItem> GetTodoItemsForUser(int id);
    }
}
