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
    
    public async Task<ReadTaskDTO> CreateTaskAsync(CreateTaskDTO task)
    {
        var entity = task.ToEntity();
        _db.Tasks.Add(entity);
        await _db.SaveChangesAsync();
        return entity.ToReadDTO();
    }

    public async Task<List<ReadTaskDTO>> GetAllTasksAsync()
    {
        List<ReadTaskDTO> dtoList = new();
        var entities = await _db.Tasks.AsNoTracking().ToListAsync();

        foreach (var entity in entities)
        {
            dtoList.Add(entity.ToReadDTO());
        }

        return dtoList;
    }

    public async Task<ReadTaskDTO> GetTaskByIdAsync(Guid id)
    {
        var task = await _db.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return task.ToReadDTO() ?? throw new KeyNotFoundException($"Task with ID: {id} not found");
    }

    public async Task<ReadTaskDTO> UpdateTaskAsync(UpdateTaskDTO dto)
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
        return entity.ToReadDTO();
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Guid id)
    {
        try
        {
            var entity = await _db.Tasks.Where(x => x.Id == id)
                .ExecuteDeleteAsync();


            if (entity == 0) throw new KeyNotFoundException($"Task with ID: {id} not found");
        }
        catch (KeyNotFoundException ex)
        {
           
        }

    }
}