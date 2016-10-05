using Kjetil.Todo.Core.Domain;
using System.Data.Entity;

namespace Kjetil.Todo.Persistence
{
    public class TodoContext : DbContext
    {
        public TodoContext()
            : base("name=TodoContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }
    }
}
