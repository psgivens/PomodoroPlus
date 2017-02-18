using PomodoroPlus.Data;
using PomodoroPlus.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlusDetails {
    public class DetailsViewModel : ViewModelBase {
        private ITimeSliceRepository _repository = new TimeSliceRepository();
        public void Refresh() {
            Dates = (from d in _repository.Dates
                     select d.Date).Distinct();
            _repository.Load(_selectedDate);
            TimeSlices = _repository.TimeSlices;
        }

        public void Save() {
            _repository.Save();
        }

        private DateTime _selectedDate = DateTime.Now.Date;

        private bool _isChangingSelectedDate;
        public DateTime SelectedDate {
            get { return _selectedDate; }
            set {

                if (_isChangingSelectedDate) return;
                try {
                    _isChangingSelectedDate = true;

                    _selectedDate = value;
                    _repository = new TimeSliceRepository();
                    Refresh();
                    RaisePropertyChanged("SelectedDate");
                }
                finally {
                    _isChangingSelectedDate = false;
                }
            }
        }

        private ObservableCollection<TimeSlice> _timeSlices;
        public ObservableCollection<TimeSlice> TimeSlices {
            get { return _timeSlices; }
            private set {
                _timeSlices = value;
                RaisePropertyChanged("TimeSlices");
            }
        }

        private IEnumerable<DateTime> _dates;
        public IEnumerable<DateTime> Dates {
            get { return _dates; }
            private set {
                _dates = value;
                RaisePropertyChanged("Dates");
            }
        }
    }
}
