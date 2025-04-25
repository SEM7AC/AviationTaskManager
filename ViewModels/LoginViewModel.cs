using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AviationTaskManager.ViewModels
    {
    public class LoginViewModel : INotifyPropertyChanged
        {
        private string _statusMessage = "Waiting for login..."; // Default text

        public string StatusMessage
            {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
            }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        public event PropertyChangedEventHandler PropertyChanged;
        }
    }
