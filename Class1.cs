using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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





        public void drawSolidWall(ref byte[] frameBuffer, byte[] colour, int x, int width, int height)
        {
                        
        }



    }
}
