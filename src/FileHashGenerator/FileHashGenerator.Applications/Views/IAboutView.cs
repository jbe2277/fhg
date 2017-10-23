using System.Waf.Applications;

namespace Waf.FileHashGenerator.Applications.Views
{
    public interface IAboutView : IView
    {
        void ShowDialog(object owner);
    }
}
