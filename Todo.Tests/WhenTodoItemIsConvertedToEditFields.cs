using System.Linq;
using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Xunit;

namespace Todo.Tests
{
    public class WhenTodoItemIsConvertedToEditFields
    {
        private readonly TodoItem srcTodoItem;
        private readonly TodoItemEditFields resultFields;

        public WhenTodoItemIsConvertedToEditFields()
        {
            // Use AutoFixture so that we don't depend on 'magic' values
            var testFixture = new Fixture();

            // source data
            var identity = testFixture.Create<string>();
            var todoListTile = testFixture.Create<string>();
            var itemTitle = testFixture.Create<string>();
            var importance = testFixture.Create<Importance>(); // TODO: Possibly always returns High on single use. Review.

            var todoList = new TestTodoListBuilder(new IdentityUser(identity), todoListTile)
                                                  .WithItem(itemTitle, importance).Build();

            srcTodoItem = todoList.Items.Single(); // Explicitly say we expect one item.
            resultFields = TodoItemEditFieldsFactory.Create(srcTodoItem);
        }

        [Fact]
        public void EqualTodoListId()
        {
            Assert.Equal(srcTodoItem.TodoListId, resultFields.TodoListId);
        }

        [Fact]
        public void EqualTitle()
        {
            Assert.Equal(srcTodoItem.Title, resultFields.Title);
        }

        [Fact]
        public void EqualImportance()
        {
            Assert.Equal(srcTodoItem.Importance, resultFields.Importance);
        }

        [Fact]
        public void RankIsNullWhenNoValueSet()
        {
            Assert.Null(srcTodoItem.Rank);
            Assert.Null(resultFields.Rank);
        }
    }
}