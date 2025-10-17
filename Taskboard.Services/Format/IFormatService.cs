namespace Taskboard.Services.Format;

public interface IFormatService
{
    public string TimeSinceCreation(DateTimeOffset created);
}