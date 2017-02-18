using PomodoroPlus.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.EntityFramework {
    public class TimeSliceTypeConfiguration : EntityTypeConfiguration<TimeSlice> {
        public TimeSliceTypeConfiguration() {
            //Property(s => s.Id)
            //    .IsRequired();

            //Property(d => d.DepartmentID).
            //  HasDatabaseGenerationOption(DatabaseGenerationOption.None);

            //HasMany(d => d.Courses).
            //  WithRequired(c => c.Department).
            //  HasForeignKey(c => c.DepartmentID).
            //  WillCascadeOnDelete();

            //Ignore(d => d.Administrator);
        }
    }
}
