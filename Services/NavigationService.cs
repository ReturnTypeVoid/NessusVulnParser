using NessusVulnParser.Core;
using System;

namespace NessusVulnParser.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private ViewModel? _currentView;
        private readonly Func<Type, ViewModel>? _viewModelFactory;
        private string? _xmlFilePath;

        public ViewModel? CurrentView
        {
            get => _currentView;
            set {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public string? XmlFilePath
        {
            get => _xmlFilePath;
            set {
                _xmlFilePath = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModel
        {
            if (_viewModelFactory == null)
            {
                return;
            }
            var viewModel = _viewModelFactory.Invoke((typeof(TViewModel)));
            CurrentView = viewModel;
        }
    }
}
