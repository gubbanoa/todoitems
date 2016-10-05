using Kjetil.Todo.Core.Repositories;
using System;

namespace Kjetil.Todo.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITodoItemRepository TodoItems { get; }
        int Complete();
    }
}
