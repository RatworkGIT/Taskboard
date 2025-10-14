using Taskboard.Models.Enums;

namespace Taskboard.Models.DTO;

public class UpdateTaskDTO
{
    public int Id  { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public Status Status { get; set; }
}