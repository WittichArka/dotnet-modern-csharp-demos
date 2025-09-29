using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Model;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("Todos"));
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todos", async ([FromServices] TodoContext db) =>
    await db.Todos.ToListAsync());

app.MapGet("/todos/{id}", async (int id, [FromServices] TodoContext db) =>
    await db.Todos.FindAsync(id));

app.MapPost("/todos", async ([FromBody] TodoItem todo, [FromServices] TodoContext db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", async (int id, [FromBody] TodoItem updatedTodo, [FromServices] TodoContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Title = updatedTodo.Title;
    todo.IsDone = updatedTodo.IsDone;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todos/{id}", async (int id, [FromServices] TodoContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null) return Results.NotFound();

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok(todo);
});

app.Run();

// 👇 Ajoute ceci pour les tests d’intégration
public partial class Program { }
