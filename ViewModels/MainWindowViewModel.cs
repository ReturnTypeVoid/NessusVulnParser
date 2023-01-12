using NessusVulnParser.Core;
using System.Windows.Input;

namespace NessusVulnParser.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _curretViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _curretViewModel; }
            set { _curretViewModel = value; NotifyPropertyChanged(); }
        }
        public ICommand UpdateViewCommand { get; set; }

        public MainWindowViewModel()
        {
            CurrentViewModel = new FileLoaderViewModel();
            UpdateViewCommand =  new UpdateViewCommand(this);
        }
    }
}
