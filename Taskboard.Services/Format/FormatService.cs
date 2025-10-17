namespace Taskboard.Services.Format;

public class FormatService : IFormatService
{
    public string TimeSinceCreation(DateTimeOffset created)
    {
        var span = DateTimeOffset.UtcNow - created;
        if (span.TotalSeconds < 60)
            return $"{(int)span.TotalSeconds}s";
        if (span.TotalMinutes < 60)
            return $"{(int)span.TotalMinutes}min";
        if (span.TotalHours < 24)
            return $"{(int)span.TotalHours}h";
        if (span.TotalDays < 7)
            return $"{(int)span.TotalDays}d";
        
        return created.ToLocalTime().ToString("d");
    }
}