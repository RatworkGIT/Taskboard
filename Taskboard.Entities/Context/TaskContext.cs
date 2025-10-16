using Microsoft.EntityFrameworkCore;
using Taskboard.Entities.Task;
using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Entities.Context;

public class TaskContext : DbContext
{
    public DbSet<TaskItemEntity> Tasks { get; set; }
    
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

}