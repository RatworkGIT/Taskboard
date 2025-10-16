using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskboard.Entities.Task;

[Table("TaskItem")]
public class TaskItemEntity
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; } = String.Empty;
    [MaxLength(200)]
    public string Description { get; set; } = String.Empty;
    public TaskStatus TaskStatus { get; set; } = TaskStatus.ToDo;
    public DateTime Created { get; set; }
    public bool? Archived { get; set; }
}