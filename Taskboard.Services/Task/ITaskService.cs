using Taskboard.Entities.Task;
using Taskboard.Models;
using Taskboard.Models.DTO;

namespace Taskboard.Services.Task;

public interface ITaskService
{
    public Task<TaskItemDTO> CreateTaskAsync(TaskItemDTO task);
    public Task<List<TaskItemDTO>> GetAllTasksAsync();
    public Task<TaskItemEntity> GetTaskByIdAsync(int id);
    public Task<TaskItemEntity> UpdateTaskAsync(TaskItemDTO task);
    public System.Threading.Tasks.Task DeleteTaskAsync(TaskItemDTO task);
}