using Microsoft.EntityFrameworkCore;
using Taskboard.Entities.Context;
using Taskboard.Entities.Task;
using Taskboard.Models.DTO;
using Taskboard.Models.Mapper;

namespace Taskboard.Services.Task;

public class TaskService : ITaskService
{
    private readonly TaskContext _db;

    public TaskService(TaskContext context)
    {
        _db = context;
    }
    
    public async Task<TaskItemDTO> CreateTaskAsync(TaskItemDTO task)
    {
        var entity = task.ToEntity();
        _db.Tasks.Add(entity);
        await _db.SaveChangesAsync();
        return task;
    }

    public async Task<List<TaskItemDTO>> GetAllTasksAsync()
    {
        List<TaskItemDTO> dtoList = new();
        var entities = await _db.Tasks.AsNoTracking().ToListAsync();

        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToDTO());
        }

        return dtoList;
    }

    public async Task<TaskItemEntity> GetTaskByIdAsync(int id)
    {
        var task = await _db.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return task ?? throw new KeyNotFoundException($"Task with ID: {id} not found");
    }

    public async Task<TaskItemEntity> UpdateTaskAsync(TaskItemDTO dto)
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

    public async System.Threading.Tasks.Task DeleteTaskAsync(TaskItemDTO dto)
    {
        var entity = await _db.Tasks.Where(x => x.Id == dto.Id)
            .ExecuteDeleteAsync();


        if (entity == 0) throw new KeyNotFoundException($"Task with ID: {dto.Id} not found");
    }
}