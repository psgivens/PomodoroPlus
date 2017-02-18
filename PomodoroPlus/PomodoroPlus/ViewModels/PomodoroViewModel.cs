using PomodoroPlus.Data;
using PomodoroPlus.EntityFramework;
using PomodoroPlus.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.ViewModels {
    public class PomodoroViewModel : ViewModelBase {

        #region Constants
        private const float AsIsTime = 0f;
        private const double BreakAccumulationRate = 0.3f;
#if DEBUG
        private const float RunningTime = 0.1f;
        private const float Overtime = 0.1f;
        private const float BreakTime = 0.1f;
        private const float ReadyToBeginTime = 0.1f;
        private const float FiveMinutes = 0.1f;
        private const float TenMinutes = 0.2f;
        private const float FifteenMinutes = 0.3f;
        private const float TwentyMinutes = 0.4f;
#else
        private const float RunningTime = 25f;
        private const float Overtime = 5f;
        private const float BreakTime = 3f;
        private const float ReadyToBeginTime = 2f;
        private const float FiveMinutes = 5f;
        private const float TenMinutes = 10f;
        private const float FifteenMinutes = 15f;
        private const float TwentyMinutes = 20f;
#endif
        #endregion


        #region Fields
        private StateViewModelBase _stateDisplay;
        private readonly PomodoroClock _clock = new PomodoroClock();
        private SoundPlayer _endSoundPlayer = new SoundPlayer(@".\Sounds\Zen Buddhist Temple Bell-SoundBible.com-331362457.wav");
        private SoundPlayer _startSoundPlayer = new SoundPlayer(@".\Sounds\crank.wav");
        private State _state;
        private TimeSpan _timeInFocus;
        private TimeSpan _timeOnBreak;
        private readonly ITimeSliceRepository _repository = new TimeSliceRepository();
        #endregion

        public PomodoroViewModel() {
            _clock.ClockTicked += _clock_ClockTicked;
            _clock.Completed += _clock_Completed;

            SetState(State.Stopped);
        }

        private void StopClockAndAccumulateTotals() {
            var elapsedTime = _clock.Stop();
            TimeSliceType type = TimeSliceType.Unknown;
            if (IsInProductiveState) {
                type = TimeSliceType.Productive;
                _timeInFocus.Add(elapsedTime);
            }
            else {
                type = TimeSliceType.OnBreak;
                _timeOnBreak.Add(elapsedTime);
            }
            _repository.PublishTimeSlice(_clock.StartTime, (int)Math.Round(elapsedTime.TotalMinutes), type, PomodoroDescription);
            PomodoroDescription = string.Empty;
        }

        private string _pomodoroDescription;
        public string PomodoroDescription {
            get { return _pomodoroDescription; }
            set {
                _pomodoroDescription = value;
                RaisePropertyChanged("PomodoroDescription");
            }
        }

        private bool _isInProductiveState;

        public bool IsInProductiveState {
            get { return _isInProductiveState; }
            set {
                _isInProductiveState = value;
                RaisePropertyChanged("IsInProductiveState");
            }
        }

        private bool _isEditingTask;
        public bool IsEditingTask {
            get { return _isEditingTask; }
            set {
                _isEditingTask = value;
                RaisePropertyChanged("IsEditingTask");
            }
        }

        private EditTaskViewModel _editTask;
        public EditTaskViewModel EditTask {
            get { return _editTask; }
            private set {
                _editTask = value;
                RaisePropertyChanged("EditTask");
            }
        }

        private void StartEditing() {
            IsEditingTask = true;
            EditTask = new EditTaskViewModel(
                new NamedCommand("Save", CommitTaskChange),
                new NamedCommand("Cancel", CancelTaskChange)
                );

        }

        public void CommitTaskChange() {
            if (IsInProductiveState) {
                this.IsEditingTask = false;
                var startTime = _clock.StartTime;
                TimeSpan elapsedTime = _clock.Clip();
                _repository.PublishTimeSlice(startTime, (int)Math.Round(elapsedTime.TotalMinutes), TimeSliceType.Productive, PomodoroDescription);
            }
            PomodoroDescription = EditTask.TaskDescription;
        }

        public void CancelTaskChange() {
            this.IsEditingTask = false;
            EditTask = null;
        }


        private void SetState(State state) {
            switch (state) {
                case State.Stopped:
                    IsInProductiveState = false;
                    if (_state != State.Stopped) {
                        StopClockAndAccumulateTotals();
                    }

                    var nextSteps = new List<NamedCommand>();
                    if (GetElegableBreakMinutes() > BreakTime) {
                        nextSteps.Add(NextStep("Break", State.Break));
                    }
                    nextSteps.Add(NextStep("Start Pomodoro", State.Running));
                    StateDisplay = new MessageViewModel("Not running",
                        nextSteps.ToArray());
                    break;
                case State.Running:
                    IsInProductiveState = true;
                    _startSoundPlayer.Play();
                    var messageViewModel = (MessageViewModel)StateDisplay;
                    StateDisplay = new ClockViewModel("Focused",
                        new NamedCommand("Change task", StartEditing),
                        NextStep("Interrupt", State.Stopped));
                    _clock.Start(RunningTime);
                    break;
                case State.AutoContinue:
                    StateDisplay = new ClockViewModel("Auto continuation",
                        NextStep("Interrupt", State.Stopped),
                        NextStep("Break", State.Break),
                        ContinueWith(AsIsTime),
                        ContinueWith(FiveMinutes),
                        ContinueWith(TenMinutes),
                        ContinueWith(FifteenMinutes),
                        ContinueWith(TwentyMinutes));
                    _clock.Continue(Overtime);
                    break;
                case State.Continuing:
                    StateDisplay = new ClockViewModel("Focus in overtime",
                        NextStep("Interrupt", State.Stopped),
                        NextStep("Break", State.Break));
                    _clock_ClockTicked(null, null);
                    break;
                case State.Break:
                    IsInProductiveState = false;
                    if (_state != State.Stopped) {
                        StopClockAndAccumulateTotals();
                    }
                    StateDisplay = new ClockViewModel("Three minute break",
                        NextStep("Interrupt", State.Stopped));
                    _clock.Start(BreakTime);
                    break;
                case State.Ready:
                    StateDisplay = new ClockViewModel("Waiting to begin",
                        NextStep("Interrupt", State.Stopped),
                        NextStep("Start Pomodooro", State.Running));
                    _clock.Continue(GetElegableBreakMinutes());
                    break;
                default:
                    break;
            }
            _state = state;
        }

        private double GetElegableBreakMinutes() {
            double accumulatedBreak = (_timeInFocus.TotalMinutes * BreakAccumulationRate);
            double spentBreak = (_timeOnBreak.TotalMinutes - BreakTime);
            double eligableBreakMinutes = accumulatedBreak - spentBreak;
            return eligableBreakMinutes;
        }

        private void _clock_Completed(object sender, EventArgs e) {
            _endSoundPlayer.Play();
            switch (_state) {
                case State.Running:
                    SetState(State.AutoContinue);
                    break;
                case State.AutoContinue:
                    SetState(State.Stopped);
                    break;
                case State.Continuing:
                    SetState(State.AutoContinue);
                    break;
                case State.Break:
                    SetState(State.Ready);
                    break;
                case State.Ready:
                    SetState(State.Stopped);
                    break;
                case State.Stopped:
                default:
                    throw new InvalidOperationException("Clock should not be running.");
            }
        }

        private NamedCommand NextStep(string title, State state) {
            return new NamedCommand(title, () => SetState(state));
        }

        private NamedCommand ContinueWith(float minutes) {
            return new NamedCommand(
                minutes > 0
                ? string.Format("Continue with {0} minutes", minutes)
                : "Continue as is",
                () => {
                    SetState(State.Continuing);
                    _clock.Continue(minutes);
                });
        }

        private void _clock_ClockTicked(object sender, EventArgs e) {
            var clockView = _stateDisplay as ClockViewModel;
            if (clockView != null) {
                clockView.ClockDisplay = _clock.RemainingTime.ToString(@"mm\:ss");
            }
            MinutesRemaining = (int)Math.Ceiling(_clock.RemainingTime.TotalMinutes);
        }

        private int _minutesRemaining;
        public int MinutesRemaining {
            get { return _minutesRemaining; }
            private set {
                _minutesRemaining = value;
                RaisePropertyChanged("MinutesRemaining");
            }
        }

        public StateViewModelBase StateDisplay {
            get { return _stateDisplay; }
            private set {
                _stateDisplay = value;
                RaisePropertyChanged("StateDisplay");
            }
        }
        public enum State {
            Stopped,
            Running,
            AutoContinue,
            Continuing,
            Break,
            Ready
        }
    }


}
