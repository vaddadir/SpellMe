using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpellMe.Commands
{
    public class ClickCommand : ICommand
    {
        private readonly Action<object> _clickEventHandler = null;
        private readonly String _commandName = String.Empty;
        public String Name
        {
            get { return _commandName; }
        }
        public ClickCommand(String commandName, Action<object> clickEventHandler)
        {
            _clickEventHandler = clickEventHandler;
            _commandName = commandName;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (_clickEventHandler != null)
                _clickEventHandler(parameter);
        }
    }
}
