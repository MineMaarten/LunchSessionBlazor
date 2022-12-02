using LunchSessionBlazor;
using LunchSessionBlazor.Api.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add DB configuration
builder.Services.AddDbContextFactory<TodoDbContext>(options =>
{
    // When in release mode (usually in Azure) use the SQL Server db in Azure.
#if RELEASE

#else
    // When not in release mode (developing locally) use a sqlite db
    var dbPath = "todoDb.db";
    options.UseSqlite($"Data Source={dbPath}");
#endif
});

var app = builder.Build();

// Allow Cross-origin-resource-sharing because of the WASM app connecting to this API
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.MapGet("/todo/current", async (TodoDbContext todoDb, ILogger<Program> logger) =>
{
    logger.LogInformation("Retrieving current todo items");
    await todoDb.Database.EnsureCreatedAsync();

    return await todoDb
        .TodoItems
        .Where(todoEntity => !todoEntity.Archived)
        .Select(todoEntity => new TodoItem
        {
            CreationDate = todoEntity.CreationDate,
            Id = todoEntity.Id,
            Text = todoEntity.Text,
            Archived = todoEntity.Archived,
            Finished = todoEntity.Finished
        })
        .ToListAsync();
});

app.MapPost("/todo", async (TodoDbContext todoDb, TodoItem itemToUpsert, ILogger<Program> logger) =>
{
    logger.LogInformation($"Saving todo item with Text '{itemToUpsert.Text}, Finished: {itemToUpsert.Finished}, Archived: {itemToUpsert.Archived}");
    await todoDb.Database.EnsureCreatedAsync();

    var existingTodoEntity = await todoDb.TodoItems.FindAsync(itemToUpsert.Id);
    if (existingTodoEntity == null)
    {
        // When the item does not exist yet, create it and mark it to EF to insert it.
        existingTodoEntity = new TodoEntity();
        todoDb.TodoItems.Add(existingTodoEntity);
    }

    // Take over the todo item information from the posted http message.
    existingTodoEntity.Id = itemToUpsert.Id;
    existingTodoEntity.CreationDate = itemToUpsert.CreationDate;
    existingTodoEntity.Text = itemToUpsert.Text;
    existingTodoEntity.Finished = itemToUpsert.Finished;
    existingTodoEntity.Archived = itemToUpsert.Archived;

    await todoDb.SaveChangesAsync();
});

app.Run();