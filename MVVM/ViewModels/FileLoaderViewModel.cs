using NessusVulnParser.Core;
using NessusVulnParser.Services;
using System.IO;
using System.Windows;
namespace NessusVulnParser.MVVM.ViewModels
{
    public class FileLoaderViewModel : ViewModel, IFilesDropped
    {
        public FileLoaderViewModel(INavigationService navService)
        {
            _navigation = navService;
        }

        private string? ReportFilePath
        {
            set {
                if (_navigation != null)
                    _navigation.XmlFilePath = value;
                OnPropertyChanged();
                Navigation?.NavigateTo<VulnListViewModel>();
            }
        }

        private INavigationService? _navigation;

        private INavigationService? Navigation
        {
            get => _navigation;
            set {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        
        public void OnFilesDropped(string[] files)
        {
            if (files.Length > 1)
            {
                MessageBox.Show("Multi file drop is not supported.", "Invalid Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string fileExtension = Path.GetExtension(files[0]);
            if (fileExtension != ".nessus")
            {
                MessageBox.Show("File is not a Nessus export file.", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ReportFilePath = Path.GetFullPath(files[0]);
            }
        }
    }
}
