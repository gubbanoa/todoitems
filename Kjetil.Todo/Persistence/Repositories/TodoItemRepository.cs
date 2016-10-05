using Kjetil.Todo.Core.Domain;
using Kjetil.Todo.Core.Repositories;

namespace Kjetil.Todo.Persistence.Repositories
{
    public class TodoItemRepository : Repository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext _context) : base(_context)
        {
        }
    }
}
