using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.Select(x => new TodoItemDTO(x)).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(new TodoItemDTO(todo))
            : Results.NotFound());

app.MapPost("/todoitems", async (TodoItemDTO todoItemDTO, TodoDb db) =>
{
var todoItem = new Todo
{
IsComplete = todoItemDTO.IsComplete,
Name = todoItemDTO.Name
};

db.Todos.Add(todoItem);
await db.SaveChangesAsync();

return Results.Created($"/todoitems/{todoItem.Id}", new TodoItemDTO(todoItem));
});

app.MapPut("/todoitems/{id}", async (int id, TodoItemDTO todoItemDTO, TodoDb db) =>
{
var todo = await db.Todos.FindAsync(id);

if (todo is null) return Results.NotFound();

todo.Name = todoItemDTO.Name;
todo.IsComplete = todoItemDTO.IsComplete;

await db.SaveChangesAsync();

return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
if (await db.Todos.FindAsync(id) is Todo todo)
{
db.Todos.Remove(todo);
await db.SaveChangesAsync();
return Results.Ok(new TodoItemDTO(todo));
}

return Results.NotFound();
});

app.Run();

public class Todo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }
    public string? Secret { get; set; }
}

public class TodoItemDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsComplete { get; set; }

    public TodoItemDTO() { }
    public TodoItemDTO(Todo todoItem) =>
    (Id, Name, IsComplete) = (todoItem.Id, todoItem.Name, todoItem.IsComplete);
}


class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
}

//-Filter deste�i yok: �rne�in, IAsyncAuthorizationFilter, IAsyncActionFilter, IAsyncExceptionFilter, IAsyncResultFilter ve IAsyncResourceFilter i�in destek yok.
//-ModelBinding deste�i yok, yani IModelBinderProvider, IModelBinder gibi. �zel bir binding i�lemi ile destek eklenebilir.
//-Form binding deste�i yok. IFormBinder
//-JsonPatch i�in destek yok
//-OData i�in destek yok
//-ApiVersioning i�in destek yok.
