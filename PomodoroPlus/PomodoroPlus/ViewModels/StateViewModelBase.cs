using PomodoroPlus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.ViewModels {
    public abstract class StateViewModelBase : ViewModelBase {
        private readonly string _statusText;
        
        public StateViewModelBase(string statusText, params NamedCommand[] namedCommands)
            : base(namedCommands) {
            _statusText = statusText;
        }

        public string StatusText { get { return _statusText; } }
    }
}
