using System;
using System.Windows.Input;

namespace PowerfullTool.Commands
{
	public class RelayCommand<TParameter> : ICommand
		where TParameter : class
	{
		private readonly Action<TParameter> _execute;

		private readonly Func<TParameter, bool> _canExecute;
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}


		public RelayCommand(
			Action<TParameter> execute,
			Func<TParameter, bool> canExecute = null)
		{
			this._execute = execute;
			this._canExecute = canExecute;
		}


		public bool CanExecute(object parameter)
		{
			return this._canExecute == null
				|| (parameter is TParameter typedParameter && this._canExecute(typedParameter));
		}

		public void Execute(object parameter)
		{
			TParameter typedParameter = parameter as TParameter;
			if (typedParameter == null)
			{
				return;
			}

			this._execute(typedParameter);
		}
	}
}
