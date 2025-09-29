using TodoApi.Data;
using TodoApi.Model;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

public class TodoContextTests
{
    [Fact]
    public async Task Can_Add_TodoItem()
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        using var context = new TodoContext(options);
        var todo = new TodoItem { Title = "Hey You out there in the cold.", IsDone = false };

        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        var saved = await context.Todos.FirstAsync();
        saved.Title.Should().Be("Hey You out there in the cold.");
    }
}
