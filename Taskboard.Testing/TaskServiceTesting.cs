using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Taskboard.Entities.Context;
using Taskboard.Models.DTO;
using Taskboard.Services.Task;
using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Testing;

public class TaskServiceTesting : IAsyncLifetime
{
    private readonly DbConnection  _connection;
    private readonly DbContextOptions<TaskContext> _options;

    public TaskServiceTesting()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<TaskContext>()
            .UseSqlite(_connection)
            .Options;
    }

    public async Task InitializeAsync()
    {
        await using var context = new TaskContext(_options);
        await context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        _connection.Dispose();
        return Task.CompletedTask;
    }
    
    private TaskContext NewContext() => new TaskContext(_options);
    
    [Fact]
    public async Task CreateTaskAsync()
    {
        await using var context = NewContext();
        
        var service = new TaskService(context);

        var dto = new TaskItemDTO
        {
            Title =  "New Task",
            Description = "Create a Task",
            TaskStatus = TaskStatus.ToDo,
            Created = DateTime.UtcNow
        };

        var result = await service.CreateTaskAsync(dto);
        
        Assert.Equal("New Task", result.Title);
        
        var saved = await context.Tasks.AsNoTracking().SingleAsync();
        
        Assert.Equal("New Task", saved.Title);
        Assert.Equal("Create a Task", saved.Description);
        Assert.Equal(TaskStatus.ToDo, saved.TaskStatus);
        Assert.NotEqual(Guid.Empty, saved.Id);
    }
}