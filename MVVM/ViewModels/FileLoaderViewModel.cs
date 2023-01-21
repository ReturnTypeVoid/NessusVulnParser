﻿using NessusVulnParser.Core;
using NessusVulnParser.Services;
using System.IO;
using System.Windows;

namespace NessusVulnParser.ViewModels
{
    public class FileLoaderViewModel : ViewModel, IFilesDropped
    {
        private string _reportFilePath;

        public string ReportFilePath
        {
            get => _reportFilePath;
            set {
                _reportFilePath = value;
                OnPropertyChanged();
                Navigation.NavigateTo<VulnListViewModel>();
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
        
        public FileLoaderViewModel()
        {

        }

        public void OnFilesDropped(string[] files)
        {
            if (files.Length > 1)
            {
                MessageBox.Show("Multi file drop is not supported.", "Invalid Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                string fileExtension = Path.GetExtension(files[0]);
                if (fileExtension != ".nessus")
                {
                    MessageBox.Show("File is not a Nessus export file.", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ReportFilePath = Path.GetFullPath(files[0]);

                    MessageBox.Show(ReportFilePath);

                }
            }
        }
    }
}
