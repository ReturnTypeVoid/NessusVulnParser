using NessusVulnParser.ViewModels;
using System.Windows;

namespace NessusVulnParser
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel = new();
        public MainWindow()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
