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
                AutostartHelper.StartedOnSystemStartup = e.Args.Length == 1 && e.Args[0] == "/startup";
                base.OnStartup(e);
            }
        }

    }
}
