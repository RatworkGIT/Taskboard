using Taskboard.Models;
using Taskboard.Models.DTO;

namespace Taskboard.Services;

public interface ITaskService
{
    public Task<TaskItem> CreateTaskAsync(TaskItem task);
    public Task<List<TaskItem>> GetAllTasksAsync();
    public Task<TaskItem> GetTaskByIdAsync(int id);
    public Task<TaskItem> UpdateTaskAsync(UpdateTaskDTO task);
    public Task DeleteTaskAsync(TaskItem task);
}