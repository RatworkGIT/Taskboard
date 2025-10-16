using Taskboard.Entities.Task;
using Taskboard.Models.DTO;

namespace Taskboard.Models.Mapper;

public static class TaskItemMapper
{
    /// Converts from DTO to Entity
    public static TaskItemEntity ToEntity(this TaskItemDTO dto) => new TaskItemEntity
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        TaskStatus = dto.TaskStatus,
        Created = dto.Created,
        Archived = dto.Archived
    };
    
    public static TaskItemDTO ToDTO(this TaskItemEntity entity) => new TaskItemDTO
    {
        Id = entity.Id,
        Title = entity.Title,
        Description = entity.Description,
        TaskStatus = entity.TaskStatus,
        Created = entity.Created,
        Archived = entity.Archived
    };
}