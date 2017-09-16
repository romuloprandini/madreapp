using System;
using System.ComponentModel;
using System.Windows.Input;

namespace MadreApp.Customs
{
    public class ButtonCommand : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _func;

        public ButtonCommand(Action action, Func<bool> func, INotifyPropertyChanged npc = null)
        {
            _action = action;
            _func = func;
            if (npc != null)
            {
                npc.PropertyChanged += delegate { RaiseCanExecuteChanged(); };
            }
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _func == null ? true : _func();
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        #endregion
    }
}
