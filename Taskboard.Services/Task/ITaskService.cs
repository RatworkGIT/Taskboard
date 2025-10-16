using Taskboard.Models.DTO;

namespace Taskboard.Services.Task;

public interface ITaskService
{
    public Task<TaskItemDTO> CreateTaskAsync(TaskItemDTO task);
    public Task<List<TaskItemDTO>> GetAllTasksAsync();
    public Task<TaskItemDTO> GetTaskByIdAsync(Guid id);
    public Task<TaskItemDTO> UpdateTaskAsync(TaskItemDTO task);
    public System.Threading.Tasks.Task DeleteTaskAsync(TaskItemDTO task);
}