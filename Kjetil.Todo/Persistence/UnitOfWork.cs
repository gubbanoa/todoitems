using Kjetil.Todo.Core;
using Kjetil.Todo.Core.Repositories;
using Kjetil.Todo.Persistence.Repositories;

namespace Kjetil.Todo.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TodoContext _context;

        public UnitOfWork(TodoContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            TodoItems = new TodoItemRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public ITodoItemRepository TodoItems { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}