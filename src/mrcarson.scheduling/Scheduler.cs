using System;
using Chroniton;
using Chroniton.Jobs;
using Chroniton.Schedules;

namespace mrcarson.scheduling
{
    public class Scheduler
    {
        private readonly EveryXTimeSchedule schedule;

        public Scheduler()
            : this(new TimeSpan(0, 0, 1, 0)) {
        }

        internal Scheduler(TimeSpan timeSpan) {
            schedule = new EveryXTimeSchedule(timeSpan);
        }

        public void AddTask(Action task) {
            var job = new SimpleJob(scheduleTime => task());

            Singularity.Instance.ScheduleJob(schedule, job, false);
        }

        public void Start() {
            Singularity.Instance.Start();
        }
    }
}