using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Taskboard.Entities.Context;

public class TaskContextFactory : IDesignTimeDbContextFactory<TaskContext>
{
    public TaskContext CreateDbContext(string[] args)
    {
        
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        var connectionString = config.GetConnectionString("Default");
        
        var optionsBuilder = new DbContextOptionsBuilder<TaskContext>();
        optionsBuilder.UseNpgsql(connectionString);
        return new TaskContext(optionsBuilder.Options);
    }
}