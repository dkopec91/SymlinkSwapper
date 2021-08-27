using SymlinkSwapper.Core;

namespace SymlinkSwapper.MVVM.ViewModel
{
    internal class AboutViewModel : ObservableObject
    {

        public string AppVersion => $"v. {typeof(MainViewModel).Assembly.GetName().Version}";

        public static string AppName => typeof(MainViewModel).Assembly.GetName().Name;

        public static string ProjectUrl => "github.com/dkopec91";

    }
}
