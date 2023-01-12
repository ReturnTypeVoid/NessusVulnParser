using NessusVulnParser.ViewModels;
using System;
using System.Windows.Input;

namespace NessusVulnParser.Core
{
    public class UpdateViewCommand : ICommand
    {
        private MainWindowViewModel viewModel;

        public UpdateViewCommand(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Home")
            {
                viewModel.CurrentViewModel = new FileLoaderViewModel();
            }
            else if (parameter.ToString() == "Account")
            {
                viewModel.CurrentViewModel = new VulnerabilitiesViewModel();
            }
        }
    }
}
