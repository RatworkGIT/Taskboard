using Taskboard.Entities.Task;
using Taskboard.Models.DTO;

namespace Taskboard.Models.Mapper;

public static class TaskItemMapper
{
    /// Converts from DTO to Entity
    public static TaskItemEntity ToEntity(this CreateTaskDTO dto) => new TaskItemEntity
    {
        Title = dto.Title,
        Description = dto.Description,
        TaskStatus = dto.TaskStatus,
        Created = DateTimeOffset.UtcNow,
        Archived = false
    };
    
    public static ReadTaskDTO ToReadDTO(this TaskItemEntity entity) => new()
    {
        Id = entity.Id,
        Number = entity.Number,
        Title = entity.Title,
        Description = entity.Description,
        TaskStatus = entity.TaskStatus,
        Created = entity.Created,
        Archived = entity.Archived ?? false
    };
}
