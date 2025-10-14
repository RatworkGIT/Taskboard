using Microsoft.EntityFrameworkCore;
using Taskboard.Models.Context;
using Taskboard.Models;
using Taskboard.Models.DTO;

namespace Taskboard.Services;

public class TaskService : ITaskService
{
    private readonly TaskContext _db;

    public TaskService(TaskContext context)
    {
        _db = context;
    }
    
    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        task.Created = DateTime.Now;
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _db.Tasks.ToListAsync();
    }

    public async Task<TaskItem> GetTaskByIdAsync(int id)
    {
        var task = await _db.Tasks.FindAsync(id);
        return task ?? throw new KeyNotFoundException($"Task with ID: {id} not found");
    }

    public async Task<TaskItem> UpdateTaskAsync(UpdateTaskDTO dto)
    {
        var entity = await _db.Tasks.FindAsync(dto.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Task with ID: {dto.Id} not found");
        }
        
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Status = dto.Status;
        
        await _db.SaveChangesAsync();
        return entity;
    }
}