using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Taskboard.Entities.Context;
using Taskboard.Entities.Task;
using Taskboard.Models.DTO;
using Taskboard.Models.Mapper;
using Taskboard.Services.Task;
using Xunit.Abstractions;
using Xunit.Sdk;
using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Testing;

public class TaskServiceTesting : IAsyncLifetime
{
    private readonly DbConnection  _connection;
    private readonly DbContextOptions<TaskContext> _options;
    private static readonly DateTimeOffset Fixed = 
        DateTimeOffset.Parse("2025-10-17 08:58:00.162 +02:00");

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

    //Naming
    public Task DisposeAsync()
    {
        _connection.Dispose();
        return Task.CompletedTask;
    }
    
// Helper
private TaskContext NewContext() => new TaskContext(_options);


//
// CREATE
//
[Fact]
public async Task CreateTaskAsync_WithValidDto_ReturnsReadDto_AndPersists()
{
    await using var context = NewContext();
    var service = new TaskService(context);

    var dto = new CreateTaskDTO
    {
        Title = "New Task",
        Description = "Create a Task",
        TaskStatus = TaskStatus.ToDo
    };

    // Act
    var result = await service.CreateTaskAsync(dto);

    // Assert (return)
    Assert.NotNull(result);
    Assert.Equal("New Task", result.Title);
    Assert.Equal("Create a Task", result.Description);
    Assert.Equal(TaskStatus.ToDo, result.TaskStatus);
    Assert.NotEqual(Guid.Empty, result.Id);

    // Assert (db)
    var saved = await context.Tasks.AsNoTracking().SingleAsync();
    Assert.Equal(result.Id, saved.Id);
    Assert.Equal("New Task", saved.Title);
    Assert.Equal("Create a Task", saved.Description);
    Assert.Equal(TaskStatus.ToDo, saved.TaskStatus);
}

//
// READ (GetAll)
//
[Fact]
public async Task GetAllTasksAsync_WhenSeeded_ReturnsAllAsReadDtos()
{
    await using var context = NewContext();

    context.Tasks.AddRange(
        new TaskItemEntity { Title = "Task 1", Number = 1, Description = "First",  TaskStatus = TaskStatus.ToDo,        Created = DateTimeOffset.UtcNow },
        new TaskItemEntity { Title = "Task 2",Number = 2, Description = "Second", TaskStatus = TaskStatus.InProgress,  Created = DateTimeOffset.UtcNow }
    );
    await context.SaveChangesAsync();

    var service = new TaskService(context);

    // Act
    var result = await service.GetAllTasksAsync();

    // Assert
    Assert.NotNull(result);
    Assert.Equal(2, result.Count);
    Assert.All(result, r => Assert.NotEqual(Guid.Empty, r.Id));
    Assert.Contains(result, r => r.Title == "Task 1" && r.Description == "First");
    Assert.Contains(result, r => r.Title == "Task 2" && r.TaskStatus == TaskStatus.InProgress);
}

//
// UPDATE
//
[Fact]
public async Task UpdateTaskAsync_WithValidDto_UpdatesEntity_AndReturnsReadDto()
{
    await using var context = NewContext();

    var entity = new TaskItemEntity
    {
        Number = 1,
        Title = "Old",
        Description = "Old desc",
        TaskStatus = TaskStatus.ToDo,
        Created = DateTimeOffset.UtcNow
    };
    context.Tasks.Add(entity);
    await context.SaveChangesAsync();

    var service = new TaskService(context);

    var update = new UpdateTaskDTO
    {
        Id = entity.Id,
        Title = "New",
        Description = "New desc",
        TaskStatus = TaskStatus.Done
    };

    // Act
    var result = await service.UpdateTaskAsync(update);
    await context.SaveChangesAsync();

    // Assert (return)
    Assert.NotNull(result);
    Assert.Equal(entity.Id, result.Id);
    Assert.Equal("New", result.Title);
    Assert.Equal("New desc", result.Description);
    Assert.Equal(TaskStatus.Done, result.TaskStatus);

    // Assert (db)
    var saved = await context.Tasks.AsNoTracking().SingleAsync(t => t.Id == entity.Id);
    Assert.Equal("New", saved.Title);
    Assert.Equal("New desc", saved.Description);
    Assert.Equal(TaskStatus.Done, saved.TaskStatus);
    // Created unchanged
    Assert.Equal(entity.Created, saved.Created);
}

//
// DELETE
//
[Fact]
public async Task DeleteTaskAsync_WithExistingId_RemovesEntity()
{
    await using var context = NewContext();
    var entity = new TaskItemEntity
    {
        Title = "To delete",
        Number = 1,
        Description = "bye",
        TaskStatus = TaskStatus.ToDo,
        Created = DateTimeOffset.UtcNow
    };
    context.Tasks.Add(entity);
    await context.SaveChangesAsync();

    var service = new TaskService(context);

    // Act
    await service.DeleteTaskAsync(entity.Id);
    await context.SaveChangesAsync();

    // Assert
    var exists = await context.Tasks.AsNoTracking().AnyAsync(t => t.Id == entity.Id);
    Assert.False(exists);
}



}