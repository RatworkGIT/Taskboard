using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Taskboard.Entities.Context;

public class TaskContextFactory : IDesignTimeDbContextFactory<TaskContext>
{
    public TaskContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=myappdb;Username=myapp;Password=devpass;Include Error Detail=true");
        return new TaskContext(optionsBuilder.Options);
    }
}