using Microsoft.EntityFrameworkCore;
using Taskboard.Entities.Task;
using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Entities.Context;

public class TaskContext : DbContext
{
    public DbSet<TaskItemEntity> Tasks { get; set; }
    
    public TaskContext(DbContextOptions<TaskContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TaskItemEntity>().ToTable("TaskItem");
        modelBuilder.Entity<TaskItemEntity>().HasData(
            new TaskItemEntity { Id = 1, Title = "This is my first Task", Description = "I am describing my first Task", Created = DateTime.UtcNow, TaskStatus = TaskStatus.InProgress},
            new TaskItemEntity { Id = 2, Title = "This is my second Task", Description = "I am describing my second Task", Created = DateTime.UtcNow, TaskStatus = TaskStatus.ToDo}
            );
    }
}