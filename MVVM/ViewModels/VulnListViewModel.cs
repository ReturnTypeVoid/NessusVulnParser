using NessusVulnParser.Core;
using NessusVulnParser.MVVM.Models;
using NessusVulnParser.MVVM.Views;
using NessusVulnParser.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
namespace NessusVulnParser.MVVM.ViewModels
{
    public class VulnListViewModel : ViewModel
    {
        private List<Vulnerability>? _vulnerabilities;
        private Vulnerability? _selectedVulnerability;
        public RelayCommand CopyToClipboardCommand { get; }
        public RelayCommand ShowDetailsCommand { get; }

        private string? ReportFilePath
        {
            get {
                if (_navigation != null)
                {
                    return _navigation.XmlFilePath;   
                }
                return null;
            }
        }

        private INavigationService? _navigation;

        public INavigationService? Navigation
        {
            get => _navigation;
            set {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public Vulnerability? SelectedVulnerability
        {
            get => _selectedVulnerability;
            set {
                _selectedVulnerability = value;
                OnPropertyChanged();
            }
        }

        public List<Vulnerability>? Vulnerabilities
        {
            get => _vulnerabilities;
            set {
                _vulnerabilities = value;
                OnPropertyChanged();
            }
        }

        public VulnListViewModel(INavigationService navService)
        {
            ShowDetailsCommand =  new RelayCommand(execute: _ => {ShowDetails();}, canExecute: _ => true);
            _navigation = navService;
            LoadVulns();
            CopyToClipboardCommand = new RelayCommand(execute: async _ =>
            {
                if (SelectedVulnerability != null)
                    await ToClipBoard();
            }, canExecute: _ => true);
            
        }

        private List<Vulnerability> GetVulnerabilities()
        {
            List<Vulnerability> vulnerabilities = new();
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                if (ReportFilePath != null)
                {
                    xmlDoc.Load(ReportFilePath);
                }
                XmlNodeList vulnerabilityNodes = xmlDoc.GetElementsByTagName("ReportItem");

                foreach (XmlNode vulnerabilityNode in vulnerabilityNodes)
                {
                    string name = vulnerabilityNode.SelectSingleNode("@pluginName")?.InnerText ?? "N/A";
                    string severity = vulnerabilityNode.SelectSingleNode("@severity")?.InnerText ?? "N/A";
                    
                    switch (severity)
                    {
                        case "0":
                            severity = "Info";
                            break;
                        case "1":
                            severity = "Low";
                            break;
                        case "2":
                            severity = "Medium";
                            break;
                        case "3":
                            severity = "High";
                            break;
                        case "4":
                            severity = "Critical";
                            break;
                    }

                    Vulnerability vulnerability = new Vulnerability(name, severity);

                    if (vulnerabilities.Find(v => v.Name == vulnerability.Name) == null)
                    {
                        vulnerabilities.Add(vulnerability);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return vulnerabilities;
        }
        
        public void AddVulnerability(Vulnerability vulnerability)
        {
            if (_vulnerabilities == null)
            {
                return;
            }
            _vulnerabilities.Add(vulnerability);
            OnPropertyChanged();
        }

        public void RemoveVulnerability(Vulnerability vulnerability)
        {
            if (_vulnerabilities == null)
            {
                return;
            }
            _vulnerabilities.Remove(vulnerability);
            OnPropertyChanged();
        }

        private async Task ToClipBoard()
        {
            if (SelectedVulnerability != null)
            {
                var textToCopy = SelectedVulnerability.Name;
                // While loop to wait 100ms between attempting to copy to the clipboard, in case the clipboard is being accessed by something else..
                while (true)
                {
                    try
                    {
                        Clipboard.SetText(textToCopy);
                        break;
                    }
                    catch (System.Runtime.InteropServices.COMException)
                    {
                        await Task.Delay(100); 
                    }
                }
            }
        }

        private async void LoadVulns()
        {
            Vulnerabilities = await Task.Run(GetVulnerabilities);
            Vulnerabilities = Vulnerabilities.OrderBy(x => x.Severity == "Info")
                .ThenBy(x => x.Severity == "Low")
                .ThenBy(x => x.Severity == "Medium")
                .ThenBy(x => x.Severity == "High")
                .ThenBy(x => x.Severity == "Critical")
                .ToList();
        }
        
        private void ShowDetails()
        {
            var detailsWindow = new VulnDetails();
            if (_selectedVulnerability != null)
            {
                var detailsViewModel = new VulnDetailsViewModel(_selectedVulnerability);
                detailsWindow.DataContext = detailsViewModel;
            }
            detailsWindow.Show();
        }

    }
}
