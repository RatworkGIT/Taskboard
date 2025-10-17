using System.Globalization;

namespace Taskboard.Services.Language;

public class LanguageService : ILanguageService
{
    public event Action? OnChange;
    
    public void SetCulture(string culture)
    {
        var newCulture = new CultureInfo(culture);
        CultureInfo.DefaultThreadCurrentCulture = newCulture;
        CultureInfo.DefaultThreadCurrentCulture = newCulture;
        Thread.CurrentThread.CurrentCulture = newCulture;
        
        Console.WriteLine($"Current Culture from Service {newCulture}");
        
        OnChange?.Invoke();
    }
}