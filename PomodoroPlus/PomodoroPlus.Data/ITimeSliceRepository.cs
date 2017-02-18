using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.Data {
    public interface ITimeSliceRepository {
        IEnumerable<DateTime> Dates { get; }
        void PublishTimeSlice(DateTime startTime, int duration, TimeSliceType type, string description);
        void Load(DateTime date);
        ObservableCollection<TimeSlice> TimeSlices { get; }
        void Save();
    }
}
