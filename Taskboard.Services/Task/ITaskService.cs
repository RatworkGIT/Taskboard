using Taskboard.Entities.Task;
using Taskboard.Models;
using Taskboard.Models.DTO;

namespace Taskboard.Services.Task;

public interface ITaskService
{
    public Task<TaskItemEntity> CreateTaskAsync(TaskItemEntity task);
    public Task<List<TaskItemEntity>> GetAllTasksAsync();
    public Task<TaskItemEntity> GetTaskByIdAsync(int id);
    public Task<TaskItemEntity> UpdateTaskAsync(UpdateTaskDTO task);
    public System.Threading.Tasks.Task DeleteTaskAsync(TaskItemEntity task);
}