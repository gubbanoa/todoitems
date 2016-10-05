using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kjetil.Todo.Persistence;
using Kjetil.Todo.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Kjetil.Todo.Test
{
    [TestClass]
    public class RepositoryTests
    {
        [TestInitialize]
        public void setup()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                unitOfWork.TodoItems.RemoveAll();
                unitOfWork.Users.RemoveAll();
                unitOfWork.Complete();
            }
        }

        [TestMethod]
        public void GetAllUsers_NoException()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                var users = unitOfWork.Users.GetAll();
            }
        }

        [TestMethod]
        public void AddUser_GetAllUsers()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                var users = unitOfWork.Users.GetAll();
                Assert.AreEqual(false, users.Any());

                var user = new User();
                user.Name = "Kjetil";
                user.Email = "kjetil@mail.no";
                unitOfWork.Users.Add(user);
                unitOfWork.Complete();

                users = unitOfWork.Users.GetAll();
                Assert.AreEqual(1, users.Count());
                Assert.AreEqual("Kjetil", users.First().Name.Trim());

            }
        }

        [TestMethod]
        public void GetAllTodoItems_NoException()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                var todoItems = unitOfWork.TodoItems.GetAll();
            }
        }

        [TestMethod]
        public void AddTodoItem_NoUser_MustThrowsException()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                try
                {
                    var todoItem = new TodoItem();
                    todoItem.Title = "write only for tests";
                    todoItem.Description = "You are not allowed to write any production code unless it is to make a failing unit test pass";
                    unitOfWork.TodoItems.Add(todoItem);
                    unitOfWork.Complete();

                    Assert.Fail();
                }
                catch (Exception)
                {
                    // all good - test passes
                }
            }
        }

        [TestMethod]
        public void AddTodoItem_GetTodoItem()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                var user = new User();
                user.Name = "Kjetil";
                user.Email = "kjetil@mail.no";
                unitOfWork.Users.Add(user);

                AddTodoItems(unitOfWork, user);

                var todoItems = unitOfWork.TodoItems.GetAll();
                Assert.AreEqual(3, todoItems.Count());
            }
        }

        private static IEnumerable<TodoItem> AddTodoItems(UnitOfWork unitOfWork, User user)
        {
            var todoItems = unitOfWork.TodoItems.GetAll();
            Assert.AreEqual(false, todoItems.Any());

            var todoItem = new TodoItem();
            todoItem.Title = "write only for tests";
            todoItem.Description = "You are not allowed to write any production code unless it is to make a failing unit test pass";
            user.TodoItems.Add(todoItem);

            todoItem = new TodoItem();
            todoItem.Title = "just write enough test to fail";
            todoItem.Description = "You are not allowed to write any more of a unit test than is sufficient to fail; and compilation failures are failures.";
            user.TodoItems.Add(todoItem);

            todoItem = new TodoItem();
            todoItem.Title = "just make test pass, not more";
            todoItem.Description = "You are not allowed to write any more production code than is sufficient to pass the one failing unit test.";
            user.TodoItems.Add(todoItem);

            unitOfWork.Complete();
            return todoItems;
        }

        [TestMethod]
        public void GetTodoItemsForPerson()
        {
            using (var unitOfWork = new UnitOfWork(new TodoContext()))
            {
                var user = new User();
                user.Name = "Kjetil";
                user.Email = "kjetil@mail.no";
                unitOfWork.Users.Add(user);

                AddTodoItems(unitOfWork, user);

                try
                {
                    //This should throw an exception because of lazy load
                    var failedTodoItems = unitOfWork.Users.Get(1).TodoItems;
                    
                    Assert.Fail();
                }
                catch (Exception)
                {
                    // all good - test passes
                }

                var todoItems = unitOfWork.Users.GetTodoItemsForUser(user.Id);
                Assert.AreEqual(3, todoItems.Count());
            }
        }
    }
}

