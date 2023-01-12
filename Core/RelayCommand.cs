using System;
using System.Windows.Input;

namespace NessusVulnParser.Core
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter != null)
            {
                this.execute(parameter);
            }
        }
    }
}
