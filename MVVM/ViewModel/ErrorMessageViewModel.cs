using SymlinkSwapper.Core;
using System;

namespace SymlinkSwapper.MVVM.ViewModel
{
    public class ErrorMessageViewModel : ObservableObject
    {
        public RelayCommand CloseErrorMessage => new(o =>
        {
            RequestCloseErrorMessage.Invoke(this, null);
        });

        public event EventHandler RequestCloseErrorMessage;

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                NotifyPropertyChanged();
            }
        }

    }
}
