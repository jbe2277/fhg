using Waf.FileHashGenerator.Domain;

namespace Waf.FileHashGenerator.Applications.ViewModels;

public record FileHashItemModel(FileHashListViewModel Context, FileHashItem Item);
