using Taskboard.Models.DTO;

namespace Taskboard.Services.Task;

public interface ITaskService
{
    public Task<ReadTaskDTO> CreateTaskAsync(CreateTaskDTO task);
    public Task<List<ReadTaskDTO>> GetAllTasksAsync();
    public Task<ReadTaskDTO> GetTaskByIdAsync(Guid id);
    public Task<ReadTaskDTO> UpdateTaskAsync(UpdateTaskDTO task);
    public System.Threading.Tasks.Task DeleteTaskAsync(Guid id);
}