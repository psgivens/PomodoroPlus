using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.Data
{
    public class TimeSlice
    {
        public virtual long Id { get; set; }
        public virtual DateTime StartTime { get; set; }
        public virtual int Duration { get; set; }
        public virtual TimeSliceType Type { get; set; }
        public virtual string Description { get; set; }
    }

    public enum TimeSliceType {
        Unknown,
        Productive,
        OnBreak,
    }
}
