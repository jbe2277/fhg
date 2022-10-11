namespace Waf.FileHashGenerator.Applications.Services;

public interface ISystemService
{
    IReadOnlyList<string> DocumentFileNames { get; }
}
