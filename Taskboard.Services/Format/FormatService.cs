namespace Taskboard.Services.Format;

public class FormatService : IFormatService
{
    public string TimeSinceCreation(DateTime created)
    {
        var span = DateTime.UtcNow - created;
        if (span.TotalSeconds < 60)
            return $"{(int)span.TotalSeconds}s ago";
        if (span.TotalMinutes < 60)
            return $"{(int)span.TotalMinutes}m ago";
        if (span.TotalHours < 24)
            return $"{(int)span.TotalHours}h ago";
        if (span.TotalDays < 7)
            return $"{(int)span.TotalDays}d ago";
        
        return created.ToLocalTime().ToString("d");
    }
}