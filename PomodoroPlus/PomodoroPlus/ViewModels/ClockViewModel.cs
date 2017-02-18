using PomodoroPlus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.ViewModels {
    public class ClockViewModel : StateViewModelBase {
        #region Fields
        private readonly DateTime _startTime = DateTime.Now;
        private string _clockDisplay;
        #endregion

        #region Initialize and teardown
        public ClockViewModel(string title, params NamedCommand[] namedCommands)
            : base(title, namedCommands) {        }
        #endregion

        protected TimeSpan CurrentTime { get; private set; }

        protected virtual void OnTick(TimeSpan remainingTime) { }
        
        public string ClockDisplay {
            get { return _clockDisplay; }
            internal set {
                _clockDisplay = value;
                RaisePropertyChanged("ClockDisplay");
            }
        }

        public TimeSpan RemainingTime { get; private set; }
    }
}
