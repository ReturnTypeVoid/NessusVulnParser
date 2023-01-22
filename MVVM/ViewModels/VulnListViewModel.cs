using NessusVulnParser.Core;
using NessusVulnParser.MVVM.Models;
using NessusVulnParser.Services;
using System;
using System.Collections.Generic;
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

                    if (severity != "0")
                    {
                        switch (severity)
                        {
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
        }
    }
}
