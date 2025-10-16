using Microsoft.EntityFrameworkCore;
using Taskboard.Entities.Task;
using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Entities.Context;

public class TaskContext : DbContext
{
    public DbSet<TaskItemEntity> Tasks { get; set; }
    
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var e = modelBuilder.Entity<TaskItemEntity>();

        e.HasKey(t => t.Id);

        e.Property(t => t.Number)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        e.HasIndex(t => t.Number).IsUnique();
    }
}