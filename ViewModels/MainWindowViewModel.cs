using NessusVulnParser.Core;

namespace NessusVulnParser.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private BaseViewModel _curretViewModel;

        public BaseViewModel CurrentViewModel
        {
            get { return _curretViewModel; }
            set { _curretViewModel = value; }
        }

        public MainWindowViewModel()
        {
            CurrentViewModel = new FileLoaderViewModel();
        }
    }
}
