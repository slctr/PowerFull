using System;
using System.Windows.Input;

namespace PowerfullTool.Commands
{
	public class RelayCommandNoArgs : ICommand
	{
		private readonly Action _execute;

		private readonly Func<bool> _canExecute;
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}


		public RelayCommandNoArgs(
			Action execute,
			Func<bool> canExecute = null)
		{
			this._execute = execute;
			this._canExecute = canExecute;
		}


		public bool CanExecute(object parameter)
		{
			return this._canExecute == null || this._canExecute();
		}

		public void Execute(object parameter)
		{
			this._execute();
		}
	}
}
