using PomodoroPlus.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroPlus.EntityFramework
{
    public class PomodoroPlusContext : DbContext
    {
        static PomodoroPlusContext() {
            Database.SetInitializer<PomodoroPlusContext>(new CreateDatabaseIfNotExists<PomodoroPlusContext>());
        }
        public virtual DbSet<TimeSlice> TimeSlices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new TimeSliceTypeConfiguration());
        }
    }
}
