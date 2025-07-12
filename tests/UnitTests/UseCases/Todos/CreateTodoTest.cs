using Application.Todos.Create;
using Core.Todos;
using Core.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;
using Xunit;

namespace UnitTests.UseCases.Todos;

public class CreateTodoTest
{
    private readonly IRepository<TodoItem> _todoRepo = Substitute.For<IRepository<TodoItem>>();
    private readonly IReadRepository<User> _userRepo = Substitute.For<IReadRepository<User>>();
    private readonly CreateTodoCommandHandler _handler;

    public CreateTodoTest()
    {
        _handler = new CreateTodoCommandHandler(_userRepo, _todoRepo);
    }

    [Fact]
    public async Task ReturnsSuccessGivenValidData()
    {
        // Arrange
        var command = new CreateTodoCommand
        {
            UserId = 1,
            Description = "Test Todo",
            Priority = Priority.Medium,
            DueDate = DateTime.Now.AddDays(1),
            Labels = new List<string> { "test", "todo" }
        };

        // Mock user lookup
        _userRepo.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new User { Id = 1 });

        // Set the ID on the TodoItem passed into Add
        _todoRepo
            .Add(Arg.Any<TodoItem>(), Arg.Any<CancellationToken>())
            .Returns(call =>
            {
                TodoItem todo = call.Arg<TodoItem>();
                todo.Id = 1; // Simulate DB assigning ID
                return Task.FromResult(todo);
            });

        // Act
        Result<int> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1);

        await _todoRepo.Received(1).Add(Arg.Any<TodoItem>(), Arg.Any<CancellationToken>());
    }
}
