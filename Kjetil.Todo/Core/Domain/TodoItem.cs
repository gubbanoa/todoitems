﻿namespace Kjetil.Todo.Core.Domain
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }
    }
}
