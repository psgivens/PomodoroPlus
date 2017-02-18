using PomodoroPlus.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.Infrastructure {
    public class ViewModelBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IEnumerable<NamedCommand> _namedCommands;

        public ViewModelBase(params NamedCommand[] namedCommands) {
            _namedCommands = namedCommands;
        }
        protected void RaisePropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IEnumerable<NamedCommand> NamedCommands {
            get { return _namedCommands; }
        }
    }
}
