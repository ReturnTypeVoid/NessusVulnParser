using NessusVulnParser.Core;
using NessusVulnParser.Models;
using System.Collections.Generic;
using System.Windows;
using System.Xml;
using System;
using System.Threading.Tasks;

namespace NessusVulnParser.ViewModels
{
    public class VulnerabilitiesViewModel : BaseViewModel
    {
        private List<Vulnerability>? _vulnerabilities;
        private Vulnerability? _selectedVulnerability;
        private string? _xmlFilePath;
        public RelayCommand CopyToClipboard { get; }

        public string? XmlFilePath
        {
            get { return _xmlFilePath; }
            set { _xmlFilePath = value; NotifyPropertyChanged(); }
        }

        public Vulnerability? SelectedVulnerability
        {
            get { return _selectedVulnerability; }
            set { _selectedVulnerability = value; NotifyPropertyChanged(); }
        }

        public List<Vulnerability>? Vulnerabilities
        {
            get { return _vulnerabilities; }
            set { _vulnerabilities = value; NotifyPropertyChanged(); }
        }

        public VulnerabilitiesViewModel(string xmlFilePath)
        {
            CopyToClipboard = new RelayCommand(ToClipBoard);
            XmlFilePath = xmlFilePath;

            LoadVulns();
        }

        private List<Vulnerability> GetVulnerabilities()
        {
            List<Vulnerability> vulnerabilities = new();
            XmlDocument xmlDoc = new XmlDocument();

            try
            {
                if(_xmlFilePath != null) 
                {
                    xmlDoc.Load(_xmlFilePath);
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
            if(_vulnerabilities == null)
            {
                return;
            }    
            _vulnerabilities.Add(vulnerability);
            NotifyPropertyChanged("Vulnerabilities");
        }

        public void RemoveVulnerability(Vulnerability vulnerability)
        {
            if (_vulnerabilities == null)
            {
                return;
            }
            _vulnerabilities.Remove(vulnerability);
            NotifyPropertyChanged("Vulnerabilities");
        }

        private void ToClipBoard(object parameter)
        {
            Vulnerability? selectedVulnerability = parameter as Vulnerability;
            if (selectedVulnerability != null)
            {
                string textToCopy = selectedVulnerability.Name;
                Clipboard.SetText(textToCopy);
            }
        }

        private async void LoadVulns()
        {
            Vulnerabilities = await Task.Run(() => GetVulnerabilities());
        }
    }
}
