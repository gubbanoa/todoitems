using System;
using Kjetil.Todo.Core.Domain;
using Kjetil.Todo.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Kjetil.Todo.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TodoContext _context) : base (_context)
        {
        }

        public IEnumerable<TodoItem> GetTodoItemsForUser(int id)
        {
            var todoList = TodoContext.TodoItems.Where(t => t.UserId == id);
            return todoList;
        }

        public TodoContext TodoContext
        {
            get { return Context as TodoContext; }
        }
    }
}
