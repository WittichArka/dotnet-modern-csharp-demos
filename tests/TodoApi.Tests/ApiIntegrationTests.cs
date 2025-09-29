using System.Net.Http.Json;
using TodoApi.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using TodoApi.Model;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Then_Get_Todo_Works()
    {
        var todo = new TodoItem { Title = "Integration test", IsDone = false };
        var response = await _client.PostAsJsonAsync("/todos", todo);

        response.EnsureSuccessStatusCode();

        var todos = (await _client.GetFromJsonAsync<List<TodoItem>>("/todos")) ?? new List<TodoItem>();
        Assert.Contains(todos, t => t.Title == "Integration test");
    }
}
