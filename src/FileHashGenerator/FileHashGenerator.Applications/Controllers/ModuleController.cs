using System.Diagnostics;
using System.Waf.Applications;
using System.Waf.Applications.Services;
using Waf.FileHashGenerator.Applications.Properties;
using Waf.FileHashGenerator.Applications.Services;
using Waf.FileHashGenerator.Applications.ViewModels;
using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.Controllers;

internal class ModuleController : IModuleController
{
    private readonly IMessageService messageService;
    private readonly IFileDialogService fileDialogService;
    private readonly ISystemService systemService;
    private readonly HexadecimalFormatter hexadecimalFormatter;
    private readonly Base64Formatter base64Formatter;
    private readonly Func<Sha512HashController> sha512HashControllerFactory;
    private readonly Func<Sha256HashController> sha256HashControllerFactory;
    private readonly Func<Sha1HashController> sha1HashControllerFactory;
    private readonly Func<MD5HashController> md5HashControllerFactory;
    private readonly Lazy<ShellViewModel> shellViewModel;
    private readonly Lazy<FileHashListViewModel> fileHashListViewModel;
    private readonly DelegateCommand openCommand;
    private readonly DelegateCommand closeCommand;
    private readonly FileHashRoot root;
    private IHashFormatter hashFormatter;
    private HashController? hashController;

    public ModuleController(IMessageService messageService, IFileDialogService fileDialogService, ISettingsService settingsService, ISystemService systemService,
        Func<Sha512HashController> sha512HashControllerFactory, Func<Sha256HashController> sha256HashControllerFactory,
        Func<Sha1HashController> sha1HashControllerFactory, Func<MD5HashController> md5HashControllerFactory,
        Lazy<ShellViewModel> shellViewModel, Lazy<FileHashListViewModel> fileHashListViewModel)
    {
        this.messageService = messageService;
        this.fileDialogService = fileDialogService;
        this.systemService = systemService;
        hashFormatter = hexadecimalFormatter = new HexadecimalFormatter();
        base64Formatter = new Base64Formatter();
        this.sha512HashControllerFactory = sha512HashControllerFactory;
        this.sha256HashControllerFactory = sha256HashControllerFactory;
        this.sha1HashControllerFactory = sha1HashControllerFactory;
        this.md5HashControllerFactory = md5HashControllerFactory;
        this.shellViewModel = shellViewModel;
        this.fileHashListViewModel = fileHashListViewModel;

        settingsService.ErrorOccurred += (sender, e) => Trace.TraceError("Error in SettingsService: {0}", e.Error);
        openCommand = new(OpenFile);
        closeCommand = new(CloseFile);
        root = new();
    }

    private ShellViewModel ShellViewModel => shellViewModel.Value;

    private FileHashListViewModel FileHashListViewModel => fileHashListViewModel.Value;

    private IHashFormatter HashFormatter
    {
        get => hashFormatter;
        set
        {
            if (hashFormatter == value) return;
            hashFormatter = value;
            if (hashController != null)
            {
                hashController.HashFormatter = value;
            }
        }
    }

    public void Initialize()
    {
        FileHashListViewModel.SetFileHashItems(root.FileHashItems);
        FileHashListViewModel.OpenFilesAction = OpenCore;
        FileHashListViewModel.CloseCommand = closeCommand;
        ShellViewModel.OpenCommand = openCommand;
        ShellViewModel.ContentView = FileHashListViewModel.View;
        ShellViewModel.HashMode = HashMode.Sha1;
        ShellViewModel.PropertyChanged += ShellViewModelPropertyChanged;
        UpdateHashMode();
        UpdateFormatter();
    }

    public void Run()
    {
        ShellViewModel.Show();
        if (systemService.DocumentFileNames.Any())
        {
            OpenCore(systemService.DocumentFileNames);
        }
    }

    public void Shutdown() { }

    private void OpenFile()
    {
        var result = fileDialogService.ShowOpenFileDialog(ShellViewModel.View, new FileType(Resources.AllFiles, ".*"));
        if (result.IsValid)
        {
            OpenCore(new[] { result.FileName! });
        }
    }

    private void CloseFile(object? parameter) => root.RemoveFileHashItem((FileHashItem)parameter!);

    private void OpenCore(IEnumerable<string> fileNames)
    {
        var filesNotFound = new List<string>();
        foreach (string fileName in fileNames)
        {
            if (!root.FileHashItems.Any(x => x.FileName == fileName))
            {
                if (File.Exists(fileName))
                {
                    root.AddNewFileHashItem(fileName);
                }
                else
                {
                    filesNotFound.Add(fileName);
                }
            }
        }
        if (filesNotFound.Any())
        {
            messageService.ShowError(ShellViewModel.View, Resources.FilesNotFoundError, string.Join(Environment.NewLine, filesNotFound));
        }
    }

    private void UpdateHashMode()
    {
        switch (ShellViewModel.HashMode)
        {
            case HashMode.Sha512:
                UpdateHashModeCore(sha512HashControllerFactory(), Resources.Sha512);
                break;
            case HashMode.Sha256:
                UpdateHashModeCore(sha256HashControllerFactory(), Resources.Sha256);
                break;
            case HashMode.Sha1:
                UpdateHashModeCore(sha1HashControllerFactory(), Resources.Sha1);
                break;
            default:
                UpdateHashModeCore(md5HashControllerFactory(), Resources.MD5);
                break;
        }
    }

    private void UpdateHashModeCore(HashController newHashController, string header)
    {
        CancelActiveController();
        hashController = newHashController;
        hashController.Root = root;
        hashController.HashFormatter = HashFormatter;
        hashController.Initialize();
        FileHashListViewModel.HashHeader = header;
    }

    private void CancelActiveController()
    {
        hashController?.Shutdown();
        hashController = null;
    }

    private void ShellViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ShellViewModel.HashMode))
        {
            UpdateHashMode();
        }
        else if (e.PropertyName == nameof(ShellViewModel.HashFormat))
        {
            UpdateFormatter();
        }
    }

    private void UpdateFormatter() => HashFormatter = ShellViewModel.HashFormat == HashFormat.Hexadecimal ? hexadecimalFormatter : base64Formatter;
}
