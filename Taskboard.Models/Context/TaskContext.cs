using Microsoft.EntityFrameworkCore;
using Taskboard.Models.Enums;

namespace Taskboard.Models.Context;

public class TaskContext : DbContext
{
    public DbSet<TaskItem> Tasks { get; set; }
    
    public TaskContext(DbContextOptions<TaskContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TaskItem>().HasData(
            new TaskItem { Id = 1, Title = "This is my first Task", Description = "I am describing my first Task", Created = DateTime.UtcNow, Status = Status.InProgress},
            new TaskItem { Id = 2, Title = "This is my second Task", Description = "I am describing my second Task", Created = DateTime.UtcNow, Status = Status.ToDo}
            );
    }
}