using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Models.DTO;

public class CreateTaskDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus TaskStatus { get; set; } = TaskStatus.ToDo;
}