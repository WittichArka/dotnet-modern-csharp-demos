using TodoApi.Data;
using TodoApi.Model;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace TodoApi.Tests;

public class TodoContextTests
{
    private TodoContext CreateContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
        return new TodoContext(options);
    }

    [Fact]
    public async Task Can_Add_TodoItem()
    {
        using var context = CreateContext("AddTest");
        var todo = new TodoItem { Title = "Learn .NET 8", IsDone = false };

        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        var saved = await context.Todos.FirstAsync();
        saved.Title.Should().Be("Learn .NET 8");
    }

    [Fact]
    public async Task Can_Get_TodoItem()
    {
        using var context = CreateContext("GetTest");
        var todo = new TodoItem { Title = "Get test", IsDone = false };
        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        var found = await context.Todos.FindAsync(todo.Id);
        found.Should().NotBeNull();
        found!.Title.Should().Be("Get test");
    }

    [Fact]
    public async Task Can_Update_TodoItem()
    {
        using var context = CreateContext("UpdateTest");
        var todo = new TodoItem { Title = "Old Title", IsDone = false };
        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        todo.Title = "New Title";
        todo.IsDone = true;
        await context.SaveChangesAsync();

        var updated = await context.Todos.FindAsync(todo.Id);
        updated!.Title.Should().Be("New Title");
        updated.IsDone.Should().BeTrue();
    }

    [Fact]
    public async Task Can_Delete_TodoItem()
    {
        using var context = CreateContext("DeleteTest");
        var todo = new TodoItem { Title = "Delete Me", IsDone = false };
        context.Todos.Add(todo);
        await context.SaveChangesAsync();

        context.Todos.Remove(todo);
        await context.SaveChangesAsync();

        var deleted = await context.Todos.FindAsync(todo.Id);
        deleted.Should().BeNull();
    }
}
