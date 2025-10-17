using TaskStatus = Taskboard.Entities.Task.TaskStatus;

namespace Taskboard.Models.DTO;

public class ReadTaskDTO
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus TaskStatus { get; set; }
    public DateTimeOffset Created { get; set; }
    public bool Archived { get; set; }
}