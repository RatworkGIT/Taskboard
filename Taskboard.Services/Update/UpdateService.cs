namespace Taskboard.Services.Update;

public class UpdateService : IUpdateService
{
    public event Action UpdateRequested;
    public void CallRequestUpdate()
    {
        UpdateRequested?.Invoke();
    }
}