using System;
using mrcarson.scheduling;

namespace mrcarson
{
    public class Program
    {
        public static void Main(string[] args) {
            var mrCarson = new MrCarson();

            if (mrCarson.AuthenticationRequired()) {
                mrCarson.Authenticate();
            }
            if (mrCarson.ContentImportRequired()) {
                mrCarson.ImportContent();
            }

            var at_1150 = new DateTime();
            var at_1530 = new DateTime();

            var scheduler = new Scheduler();
            scheduler.AddTask(() => {
                var currentTime = DateTime.Now;
                if (currentTime.TimeOfDay > new TimeSpan(11, 37, 0) && at_1150.Date < currentTime.Date) {
                    at_1150 = currentTime;
                    Console.WriteLine("11:37 - Time for a tweet!");
                    mrCarson.Tweet();
                }
                if (currentTime.TimeOfDay > new TimeSpan(15, 30, 0) && at_1530.Date < currentTime.Date) {
                    at_1530 = currentTime;
                    Console.WriteLine("15:30 - Time for a tweet!");
                    mrCarson.Tweet();
                }
            });

            scheduler.Start();

            Console.WriteLine("Press any key to stop...");
            Console.ReadLine();
        }
    }
}