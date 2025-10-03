using Microsoft.EntityFrameworkCore;
using TodoApi.Model;

namespace TodoApi.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItem> Todos => Set<TodoItem>();
}
