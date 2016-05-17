using System;
using System.Windows.Input;

namespace AccountingSystem.ViewModel.Commands
{
    public class RelayCommand : ICommand
    {
        private Action Action { get; }

        public RelayCommand(Action action)
        {
            Action = action;
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Action();
        }

        #endregion
    }
}
