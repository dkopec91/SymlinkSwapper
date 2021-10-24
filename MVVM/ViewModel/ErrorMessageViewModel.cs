using SymlinkSwapper.Core;
using SymlinkSwapper.Logic;
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

        private string errorHeader;

        public string ErrorHeader
        {
            get => errorHeader;
            set
            {
                errorHeader = value;
                NotifyPropertyChanged();
            }
        }

        public void SetException(Exception e)
        {
            if (e is KnownException)
            {
                KnownException knownException = e as KnownException;
                ErrorHeader = knownException.ErrorHeader;
                ErrorMessage = knownException.ErrorMessage;
                return;
            }

            ErrorMessage = e.Message;
            ErrorHeader = "Unexpected error";
        }
    }
}