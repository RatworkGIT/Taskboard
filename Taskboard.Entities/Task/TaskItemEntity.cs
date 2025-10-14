namespace Taskboard.Entities.Task;

public class TaskItemEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public TaskStatus TaskStatus { get; set; } = TaskStatus.ToDo;
    public DateTime Created { get; set; }
}