using PomodoroPlus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.ViewModels {
    public class MessageViewModel : StateViewModelBase {
        public MessageViewModel(string statusText, params NamedCommand[] namedCommands)
            : base(statusText, namedCommands) {
        }

        //public string NextPomodoro { get; set; }
    }
}
