using NessusVulnParser.ViewModels;
using System.Windows.Controls;

namespace NessusVulnParser.Views
{
    public partial class Vulnerabilities : UserControl
    {
        private VulnerabilitiesViewModel _viewModel = new();
        public Vulnerabilities()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
