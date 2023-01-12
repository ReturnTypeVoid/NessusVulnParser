using NessusVulnParser.ViewModels;
using System;
using System.Windows.Input;

namespace NessusVulnParser.Core
{
    public class UpdateViewCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter== null)
            {
                return;
            }
            if (parameter.ToString() == "Home")
            {
                _viewModel.CurrentViewModel = new FileLoaderViewModel();
            }
            else if (parameter.ToString() == "Account")
            {
                _viewModel.CurrentViewModel = new VulnerabilitiesViewModel("C:\\Users\\ReeceAlqotaibi\\OneDrive - My Personal Tenant\\Desktop\\BEA Internal_9pifu5.nessus");
            }
        }
    }
}
