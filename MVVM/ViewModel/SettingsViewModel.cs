using Ookii.Dialogs.Wpf;
using SymlinkSwapper.Core;
using SymlinkSwapper.Properties;
using System;
using System.ComponentModel.DataAnnotations;

namespace SymlinkSwapper.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        public RelayCommand IncreaseDelayCommand => new(o => Delay = Delay < int.MaxValue - 100 ? Delay + 100 : Delay);

        public RelayCommand DecreaseDelayCommand => new(o => Delay = Delay > 100 ? Delay - 100 : Delay);

        public RelayCommand SetSourcePathCommand => new(o =>
        {
            VistaFolderBrowserDialog openFolderDialog = new();
            if (openFolderDialog.ShowDialog() == true)
            {
                SourceFolder = openFolderDialog.SelectedPath;
            }
        });

        public RelayCommand SetOutputPathCommand => new(o =>
        {
            VistaOpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
            {
                TargetSymlink = openFileDialog.FileName;
            }
        });

        private string _sourceFolder;

        private Settings _appSettings = Settings.Default;

        public string SourceFolder
        {
            get => _sourceFolder;
            set
            {
                _sourceFolder = value;
                _appSettings.SourceFolder = _sourceFolder;
                SaveSettings();
                OnPropertyChanged();
            }
        }

        private string _targetSymlink;

        public string TargetSymlink
        {
            get => _targetSymlink;
            set
            {
                _targetSymlink = value;
                _appSettings.TargetSymlink = _targetSymlink;
                SaveSettings();
                OnPropertyChanged();
            }
        }

        private int _delay;

        [Range(300, int.MaxValue)]
        public int Delay
        {
            get => _delay;
            set
            {
                _delay = value;
                _appSettings.Delay = _delay;
                SaveSettings();
                OnPropertyChanged();
            }
        }

        private bool _autostart;

        public bool Autostart
        {
            get => _autostart;
            set
            {
                _autostart = value;
                _appSettings.Autostart = _autostart;
                SaveSettings();
                OnPropertyChanged();
            }
        }

        private void SaveSettings() => Settings.Default.Save();

        public SettingsViewModel()
        {
            _sourceFolder = _appSettings.SourceFolder;
            _targetSymlink = _appSettings.TargetSymlink;
            _delay = _appSettings.Delay;
            _autostart = _appSettings.Autostart;
        }
    }
}
