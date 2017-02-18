using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PomodoroPlus {
    public class PomodoroClock {
        private readonly Timer _timer;
        private bool _isRunning = false;
        private DateTime _startTime = DateTime.Now;
        private DateTime _countDownStart = DateTime.Now;
        private TimeSpan _expectedTimeInMinutes;
        public event EventHandler<EventArgs> ClockTicked;
        public event EventHandler<EventArgs> Completed;

        public PomodoroClock() {
            _timer = new Timer(TimerCallback, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            TimerCallback(null);
        }

        private void TimerCallback(object timerState) {
            var currentTime = (_expectedTimeInMinutes - (DateTime.Now - _countDownStart));
            if (currentTime <= TimeSpan.Zero) {
                currentTime = TimeSpan.Zero;

                if (Completed != null)
                    Completed(this, EventArgs.Empty);
            }
            RemainingTime = currentTime;
            if (ClockTicked != null)
                ClockTicked(this, EventArgs.Empty);
        }

        public DateTime StartTime { get { return _startTime; } }
        public TimeSpan RemainingTime { get; private set; }

        internal void Start(double expectedTimeInMinutes) {
            _isRunning = true;
            _startTime = _countDownStart = DateTime.Now;
            Continue(expectedTimeInMinutes);
        }

        internal void Continue(double expectedTimeInMinutes) {
            if (!_isRunning) throw new InvalidOperationException("Cannot continue when not running.");

            if (expectedTimeInMinutes > 0f) {
                _countDownStart = DateTime.Now;
                _expectedTimeInMinutes = TimeSpan.FromMinutes(expectedTimeInMinutes).Add(TimeSpan.FromSeconds(1));
            }
            TimerCallback(null);
            _timer.Change(0, 1000);
        }

        internal TimeSpan Stop() {
            _isRunning = false;
            _timer.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
            return DateTime.Now - _startTime;
        }

        internal TimeSpan Clip() {
            var elapsed = DateTime.Now - _startTime;
            _startTime = DateTime.Now;
            return elapsed;
        }
    }


}
