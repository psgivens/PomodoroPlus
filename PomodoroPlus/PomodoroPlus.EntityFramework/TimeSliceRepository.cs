using PomodoroPlus.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace PomodoroPlus.EntityFramework {
    public class TimeSliceRepository : ITimeSliceRepository {
        private PomodoroPlusContext _context = new PomodoroPlusContext();

        void ITimeSliceRepository.PublishTimeSlice(DateTime startTime, int duration, TimeSliceType type, string description) {
            _context.TimeSlices.Add(new TimeSlice {
                StartTime = startTime,
                Duration = duration,
                Type = type,
                Description = description
            });
            _context.SaveChanges();
        }

        public void Load(DateTime date) {
            var before = date.Date;
            var after = before.AddDays(1);
            _context.TimeSlices.Where(s=>
                s.StartTime >= before && s.StartTime < after
            ).Load();
        }

        public IEnumerable<DateTime> Dates {
            get {
                return (from i in _context.TimeSlices
                        select i.StartTime);
            }
        }

        ObservableCollection<TimeSlice> ITimeSliceRepository.TimeSlices {
            get {
                return _context.TimeSlices.Local;
            }
        }

        void ITimeSliceRepository.Save() {
            _context.SaveChanges();
        }
    }
}
