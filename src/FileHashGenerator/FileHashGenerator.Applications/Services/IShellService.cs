namespace Waf.FileHashGenerator.Applications.Services
{
    public interface IShellService
    {
        object ShellView { get; }

        void UpdateProgress(object source, double progress);

        void RemoveProgress(object source);
    }
}
