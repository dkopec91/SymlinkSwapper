using Ookii.Dialogs.Wpf;
using SymlinkSwapper.Core;
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
            VistaFolderBrowserDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
            {
                SourceFolder = openFileDialog.SelectedPath;
            }
        });

        public RelayCommand SetOutputPathCommand { get; set; }

        private string _sourceFolder;

        public string SourceFolder
        {
            get => _sourceFolder;
            set
            {
                _sourceFolder = value;
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
                OnPropertyChanged();
            }
        }


        public SettingsViewModel()
        {
            SourceFolder = Properties.Settings.Default.SourceFolder;
            TargetSymlink = Properties.Settings.Default.TargetSymlink;
            Delay = Properties.Settings.Default.Delay;

            //IncreaseDelayCommand = new RelayCommand(o => Delay = Delay < int.MaxValue - 100 ? Delay + 100 : Delay);

            //DecreaseDelayCommand = new RelayCommand(o => Delay = Delay > 100 ? Delay - 100 : Delay);

            //SetSourcePathCommand = new RelayCommand(o =>
            //{
            //    VistaFolderBrowserDialog openFileDialog = new();
            //    if (openFileDialog.ShowDialog() == true)
            //    {
            //        SourceFolder = openFileDialog.SelectedPath;
            //    }
            //});

        }
    }
}
