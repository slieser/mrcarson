using System;
using System.Threading;
using NUnit.Framework;

namespace mrcarson.scheduling.tests
{
    [TestFixture]
    public class SchedulerTests
    {
        [Test, Explicit]
        public void Run_scheduler() {
            var sut = new Scheduler(new TimeSpan(0, 0, 0, 1));
            sut.AddTask(() => Console.WriteLine($"Hello from the scheduler at {DateTime.Now}"));
            sut.Start();

            Thread.Sleep(10 * 1000);
        }
    }
}