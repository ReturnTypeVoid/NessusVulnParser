using NessusVulnParser.Core;
using NessusVulnParser.Services;
namespace NessusVulnParser.MVVM.ViewModels
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
            Navigation.CurrentView = new FileLoaderViewModel(Navigation);
            NavigateFileLoaderCommand = new RelayCommand(execute: _ => { Navigation.NavigateTo<FileLoaderViewModel>(); }, canExecute: _ => true);
            NavigateVulnListCommand = new RelayCommand(execute: _ => { 
                if(_navigation != null && _navigation.XmlFilePath != null) Navigation.NavigateTo<VulnListViewModel>(); 
            }, canExecute: _ => true);
        }
    }
}
