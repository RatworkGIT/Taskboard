using Microsoft.EntityFrameworkCore;
using Taskboard.Entities.Context;
using Taskboard.Entities.Task;
using Taskboard.Models.DTO;

namespace Taskboard.Services.Task;

public class TaskService : ITaskService
{
    private readonly TaskContext _db;

    public TaskService(TaskContext context)
    {
        _db = context;
    }
    
    public async Task<TaskItemEntity> CreateTaskAsync(TaskItemEntity task)
    {
        task.Created = DateTime.Now;
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<List<TaskItemEntity>> GetAllTasksAsync()
    {
        return await _db.Tasks.ToListAsync();
    }

    public async Task<TaskItemEntity> GetTaskByIdAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        return task ?? throw new KeyNotFoundException($"Task with ID: {id} not found");
    }

    public async Task<TaskItemEntity> UpdateTaskAsync(UpdateTaskDTO dto)
    {
        var entity = await _db.Tasks.FindAsync(dto.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Task with ID: {dto.Id} not found");
        }
        
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.TaskStatus = dto.TaskStatus;
        
        await _db.SaveChangesAsync();
        return entity;
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(TaskItemEntity task)
    {
        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync();
    }
}