using PomodoroPlus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.ViewModels {
    public class EditTaskViewModel : ViewModelBase {
        public EditTaskViewModel(params NamedCommand[] namedCommands)
            : base(namedCommands) {
        }

        private string _taskDescription;

        public string TaskDescription {
            get { return _taskDescription; }
            set {
                _taskDescription = value;
                RaisePropertyChanged("TaskDescription");
            }
        }

    }
}
