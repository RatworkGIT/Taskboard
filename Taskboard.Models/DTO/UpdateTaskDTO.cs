using Taskboard.Entities.Task;
using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Models.DTO;

public class UpdateTaskDTO
{
    public required int Id  { get; init; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public TaskStatus TaskStatus { get; set; }
}