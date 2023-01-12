using NessusVulnParser.ViewModels;
using System.Windows.Controls;

namespace NessusVulnParser.Views
{
    public partial class Vulnerabilities : UserControl
    {
        private VulnerabilitiesViewModel _viewModel = new("C:\\Users\\ReeceAlqotaibi\\OneDrive - My Personal Tenant\\Desktop\\BEA Internal_9pifu5.nessus");
        public Vulnerabilities()
        {
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
