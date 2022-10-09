using System.Waf.Applications.Services;

namespace Waf.FileHashGenerator.Presentation.DesignData;

public class MockSettingsService : ISettingsService
{
    public string FileName { get; set; } = "";

    public event EventHandler<SettingsErrorEventArgs>? ErrorOccurred;

    public T Get<T>() where T : class, new() => new();

    public void Save() { }

    public void RaiseErrorOccurred(SettingsErrorEventArgs e) => ErrorOccurred?.Invoke(this, e);
}
