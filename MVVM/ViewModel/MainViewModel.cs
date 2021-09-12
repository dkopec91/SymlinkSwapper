using SymlinkSwapper.Core;

namespace SymlinkSwapper.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public SettingsViewModel SettingsVM { get; set; }

        public AboutViewModel AboutVM { get; set; }

        public RelayCommand ToggleAboutViewCommand => new(o => { CurrentView = CurrentView is SettingsViewModel ? AboutVM : SettingsVM; });

        public string AppName => typeof(MainViewModel).Assembly.GetName().Name;

        public string StartStopButtonLabel => _executionRunning ? "⬛" : "▶";

        private bool _executionRunning => false;

        private object _curentView;

        public object CurrentView
        {
            get { return _curentView; }
            set
            {
                _curentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            AboutVM = new AboutViewModel();
            SettingsVM = new SettingsViewModel();
            CurrentView = SettingsVM;
        }

    }
}
