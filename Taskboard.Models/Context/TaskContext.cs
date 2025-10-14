using Microsoft.EntityFrameworkCore;

namespace Taskboard.Models.Context;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options){}
    
    public DbSet<TaskItem> Tasks { get; set; }
}