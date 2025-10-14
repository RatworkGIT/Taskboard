using Taskboard.Models.Enums;

namespace Taskboard.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public Status Status { get; set; } = Status.ToDo;
    public DateTime Created { get; set; }
}