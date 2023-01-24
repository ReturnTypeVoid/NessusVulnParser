using NessusVulnParser.Core;
using NessusVulnParser.MVVM.Models;
namespace NessusVulnParser.MVVM.ViewModels
{
    
    public class VulnDetailsViewModel : ViewModel
    {
        private readonly Vulnerability _vulnerability;
        public string Name
        {
            get => _vulnerability.Name;
            set {
                _vulnerability.Name = value;
                OnPropertyChanged();
            }
        }
        public string? Severity
        {
            get => _vulnerability.Severity;
            set {
                _vulnerability.Severity = value;
                OnPropertyChanged();
            }
        }
        public VulnDetailsViewModel(Vulnerability vulnerability)
        {
            _vulnerability = vulnerability;
        }
    }
}
