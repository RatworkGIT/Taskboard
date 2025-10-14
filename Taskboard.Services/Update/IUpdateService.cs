namespace Taskboard.Services.Update;

public interface IUpdateService
{
    event Action UpdateRequested;
    void CallRequestUpdate();
}