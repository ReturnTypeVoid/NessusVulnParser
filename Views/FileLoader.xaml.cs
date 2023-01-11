using NessusVulnParser.ViewModels;
using System.Windows.Controls;

namespace NessusVulnParser.Views
{
    public partial class FileLoader : UserControl
    {
        private FileLoaderViewModel _viewModel = new();
        public FileLoader()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
