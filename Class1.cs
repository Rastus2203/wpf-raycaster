﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RayCaster
{
    class rayCaster
    {
        int frameHeight;
        int frameWidth;
        int bytesPerPixel;
        int frameStride;
        byte[] frameBuffer;

        public rayCaster (int inputHeight, int inputWidth, int inputBytesPerPixel)
        {
            frameWidth = inputWidth;
            frameHeight = inputHeight;
            bytesPerPixel = inputBytesPerPixel;
            frameStride = frameWidth * bytesPerPixel;

            frameBuffer = new byte[frameWidth * frameHeight * bytesPerPixel];




        }





        public void drawSolidWall(byte[] colour, int x, int width, int height)
        {

        }




        public void renderFrame(ref Image viewPort) {
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



            BitmapSource frame = BitmapSource.Create(frameWidth, frameHeight, 96, 96, PixelFormats.Bgra32, null, frameBuffer, frameStride);
            frame.Freeze();
            viewPort.Source = frame;
        }





    }
}
