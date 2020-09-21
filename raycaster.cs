using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.IO;

namespace RayCaster
{
    class rayCaster
    {
        // Needed to send the generated frame data back to the ui
        public delegate void rayCasterDelegate();

        public void setViewPort()
        {
            viewPort.Source = frame;
        }



        int frameHeight;
        int frameWidth;
        int bytesPerPixel;
        int frameStride;
        int frameCount;
        BitmapSource frame;
        Image viewPort;
        byte[] frameBuffer;

        public rayCaster (int inputHeight, int inputWidth, int inputBytesPerPixel, ref Image inputViewPort)
        {
            frameWidth = inputWidth;
            frameHeight = inputHeight;
            bytesPerPixel = inputBytesPerPixel;
            viewPort = inputViewPort;
            frameStride = frameWidth * bytesPerPixel;

            frameBuffer = new byte[frameWidth * frameHeight * bytesPerPixel];
        }

        public void loadMap()
        {
            string mapText = Properties.Resources.simpleTestMap;
            Trace.Write(mapText);
        }



        public void drawSolidWall(byte[] colour, int x, int width, int height)
        {

        }

        // Self explanatory, repeatedly calls renderFrame
        public void renderLoop()
        {
            loadMap();
            while (true)
            {
                renderFrame();
            }
        }

        public void renderFrame() 
        {
            // Draws a gradient over the back, mostly just testing if viewport works
            for (int i = 0; i < frameBuffer.Length; i++)
            {
                float multiplier = (float)i / frameBuffer.Length;
                if (i % 4 < 3) {
                    frameBuffer[i] = (byte)(0xff * multiplier);
                } else if (i % 4 == 3) {
                    frameBuffer[i] = 0xff;
                }
            }


            // Write the buffer to a bitmapsource object
            frame = BitmapSource.Create(frameWidth, frameHeight, 96, 96, PixelFormats.Bgra32, null, frameBuffer, frameStride);
            frame.Freeze();
            frameCount++;

            // Send the frame to the ui
            viewPort.Dispatcher.Invoke(
            new rayCasterDelegate(setViewPort)
            );
        }
    }
}
