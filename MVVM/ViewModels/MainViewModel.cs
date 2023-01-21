using NessusVulnParser.Core;
using NessusVulnParser.Services;

namespace NessusVulnParser.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private INavigationService? _navigation;

        public INavigationService? Navigation
        {
            get => _navigation;
            set {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateVulnListCommand { get; set; }
        public RelayCommand NavigateFileLoaderCommand { get; set; }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            Navigation.CurrentView = new FileLoaderViewModel();
            NavigateFileLoaderCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<FileLoaderViewModel>(); }, canExecute: o => true);
            NavigateVulnListCommand = new RelayCommand(execute: o => { Navigation.NavigateTo<VulnListViewModel>(); }, canExecute: o => true);
        }
    }
}
