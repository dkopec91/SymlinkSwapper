using SymlinkSwapper.Core;
using System;
using System.IO;

namespace SymlinkSwapper.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private readonly SymLinkWorker symlinkWorker;
        public SettingsViewModel SettingsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }
        public ErrorMessageViewModel ErrorMessageVM { get; set; }

        public RelayCommand ToggleAboutViewCommand => new(o =>
        {
            CurrentView = CurrentView is SettingsViewModel ? AboutVM : SettingsVM;
        });

        public RelayCommand StartStopWorkerCommand => new(o =>
        {
            WorkerRunning = symlinkWorker.ToggleWorker(
                                SettingsVM.SourceFolder,
                                SettingsVM.TargetSymlink,
                                SettingsVM.Delay);
        });

        public static string AppName => typeof(MainViewModel).Assembly.GetName().Name;

        private bool _workerRunning;

        public bool WorkerRunning
        {
            get => _workerRunning;
            set
            {
                _workerRunning = value;
                NotifyPropertyChanged();
            }
        }

        private object _curentView;

        public object CurrentView
        {
            get => _curentView;
            set
            {
                _curentView = value;
                NotifyPropertyChanged();
            }
        }

        public MainViewModel()
        {
            AboutVM = new AboutViewModel();
            SettingsVM = new SettingsViewModel();
            ErrorMessageVM = new ErrorMessageViewModel();
            CurrentView = SettingsVM;

            symlinkWorker = new();
            symlinkWorker.WorkerIssue += ReportWorkerIssue;
            ErrorMessageVM.RequestCloseErrorMessage += CloseErrorMessage;
        }

        private void ReportWorkerIssue(object sender, ErrorEventArgs e)
        {
            WorkerRunning = false;
            ErrorMessageVM.ErrorMessage = e.GetException().Message;
            CurrentView = ErrorMessageVM;
        }

        private void CloseErrorMessage(object sernder, EventArgs e)
        {
            CurrentView = SettingsVM;
        }
    }
}
