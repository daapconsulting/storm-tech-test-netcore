using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;

namespace Todo.Tests
{
    /*
     * This class makes it easier for tests to create new TodoLists with TodoItems correctly hooked up
     */
    public class TestTodoListBuilder
    {
        private readonly string title;
        private readonly IdentityUser owner;
        private readonly List<(string, Importance)> items = new List<(string, Importance)>();

        public TestTodoListBuilder(IdentityUser owner, string title)
        {
            this.title = title;
            this.owner = owner;
        }

        public TestTodoListBuilder WithItem(string itemTitle, Importance importance)
        {
            items.Add((itemTitle, importance));
            return this;
        }

        public TodoList Build()
        {
            var todoList = new TodoList(owner, title);
            var todoItems = items.Select(itm => new TodoItem(todoList.TodoListId, owner.Id, itm.Item1, itm.Item2));
            todoItems.ToList().ForEach(tlItm =>
            {
                todoList.Items.Add(tlItm);
                tlItm.TodoList = todoList;
            });
            return todoList;
        }
    }

    /*
     *         public int TodoListId { get; set; }
        public string Title { get; set; }
        public IdentityUser Owner { get; set; }

        public ICollection<TodoItem> Items { get; set; } = new List<TodoItem>();

        protected TodoList() { }

        public TodoList(IdentityUser owner, string title)
        {
            Owner = owner;
            Title = title;
        }

            public int TodoItemId { get; set; }
        public string Title { get; set; }
        public string ResponsiblePartyId { get; set; }
        public IdentityUser ResponsibleParty { get; set; }
        public bool IsDone { get; set; }
        public Importance Importance { get; set; }

        public int TodoListId { get; set; }
        public TodoList TodoList { get; set; }

        public int? Rank { get; set; }

        protected TodoItem() { }

        public TodoItem(int todoListId, string responsiblePartyId, string title, Importance importance)
        {
            TodoListId = todoListId;
            ResponsiblePartyId = responsiblePartyId;
            Title = title;
            Importance = importance;
        }

        public TodoItem(int todoListId, string responsiblePartyId, string title, Importance importance, int? rank) : this(todoListId, responsiblePartyId, title, importance)
        {
            Rank = rank;
        }
    }
     */
}