using SymlinkSwapper.Logic;
using System.Windows;

namespace SymlinkSwapper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (SingleAppInstanceHelper.IsAppAlreadyRunning())
            {
                Shutdown();
            }
            else
            {
                base.OnStartup(e);
            }
        }

    }
}
