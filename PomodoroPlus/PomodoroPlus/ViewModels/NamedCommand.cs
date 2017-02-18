using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.ViewModels {
    public abstract class NamedCommandBase : System.Windows.Input.ICommand {

        #region Initialize and Teardown
        public NamedCommandBase(string title) {
            Title = title;
        }
        #endregion

        public string Title { get; private set; }

        bool System.Windows.Input.ICommand.CanExecute(object parameter) {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);
    }
    public class NamedCommand : NamedCommandBase {
        #region Fields
        private readonly Action _action;
        #endregion

        #region Initialize and teardown
        public NamedCommand(string title, Action action)
            : base(title) {
            _action = action;
        }
        #endregion

        public override void Execute(object parameter) {
            _action();
        }
    }

    public class NamedCommand<T> : NamedCommandBase {
        private readonly Action<T> _action;

        public NamedCommand(string title, Action<T> action)
            : base(title) {
            _action = action;
        }

        public override void Execute(object parameter) {
            _action((T)parameter);
        }
    }
}
