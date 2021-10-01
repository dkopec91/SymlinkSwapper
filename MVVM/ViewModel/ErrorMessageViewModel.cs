using SymlinkSwapper.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
