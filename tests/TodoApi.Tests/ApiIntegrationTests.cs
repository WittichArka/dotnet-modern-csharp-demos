using System.Net.Http.Json;
using TodoApi.Data;
using TodoApi.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;

namespace TodoApi.Tests;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Can_Post_And_Get_Todo()
    {
        var todo = new TodoItem { Title = "Integration test", IsDone = false };
        var postResponse = await _client.PostAsJsonAsync("/todos", todo);
        postResponse.EnsureSuccessStatusCode();

        var todos = await _client.GetFromJsonAsync<List<TodoItem>>("/todos");
        todos.Should().ContainSingle(t => t.Title == "Integration test");
    }

    [Fact]
    public async Task Can_Update_Todo()
    {
        var todo = new TodoItem { Title = "Old Title", IsDone = false };
        var postResponse = await _client.PostAsJsonAsync("/todos", todo);
        postResponse.EnsureSuccessStatusCode();
        var created = await postResponse.Content.ReadFromJsonAsync<TodoItem>();

        created.Should().NotBeNull();
        created!.Title = "Updated Title";
        created.IsDone = true;

        var putResponse = await _client.PutAsJsonAsync($"/todos/{created.Id}", created);
        putResponse.EnsureSuccessStatusCode();

        var todos = await _client.GetFromJsonAsync<List<TodoItem>>("/todos");
        todos.Should().ContainSingle(t => t.Title == "Updated Title" && t.IsDone);
    }

    [Fact]
    public async Task Can_Delete_Todo()
    {
        var todo = new TodoItem { Title = "To be deleted", IsDone = false };
        var postResponse = await _client.PostAsJsonAsync("/todos", todo);
        postResponse.EnsureSuccessStatusCode();
        var created = await postResponse.Content.ReadFromJsonAsync<TodoItem>();

        var deleteResponse = await _client.DeleteAsync($"/todos/{created!.Id}");
        deleteResponse.EnsureSuccessStatusCode();

        var todos = await _client.GetFromJsonAsync<List<TodoItem>>("/todos");
        todos.Should().NotContain(t => t.Id == created.Id);
    }
}
