using NessusVulnParser.ViewModels;
using System.Windows.Controls;

namespace NessusVulnParser.Views
{
    public partial class VulnList : UserControl
    {
        private VulnListViewModel _viewModel = new();
        public VulnList()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
