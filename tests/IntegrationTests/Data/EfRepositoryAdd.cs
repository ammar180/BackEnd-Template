using Core.Todos;
using Xunit;

namespace IntegrationTests.Data;
public class EfRepositoryAdd : BaseEfRepoTestFixture
{
    [Fact]
    public async Task AddsContributorAndSetsId()
    {
        var repository = GetRepository<TodoItem>();
        var todo = new TodoItem
        {
            UserId = 1,
            Description = "Test Todo",
            Priority = Priority.Medium,
            DueDate = DateTime.Now.AddDays(1),
            Labels = new List<string> { "test", "todo" }
        };

        await repository.Add(todo);

        var newTodo = await repository.GetFirstOrDefaultAsync(repository.Query);

        Assert.Equal(todo.Description, newTodo?.Description);
        Assert.True(newTodo?.Id > 0);
    }
}
