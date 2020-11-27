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

        game game;
        int[,] worldMap;


        public rayCaster (int inputHeight, int inputWidth, int inputBytesPerPixel, ref Image inputViewPort, ref game inputGame)
        {
            frameWidth = inputWidth;
            frameHeight = inputHeight;
            bytesPerPixel = inputBytesPerPixel;
            viewPort = inputViewPort;
            game = inputGame;
            frameStride = frameWidth * bytesPerPixel;

            frameBuffer = new byte[frameWidth * frameHeight * bytesPerPixel];
        }



        public void doRayCast()
        {
            float posX = game.player.xPos;
            float posY = game.player.yPos;
            float dirX = game.player.xDir;
            float dirY = game.player.yDir;
            float planeX = game.player.xPlane;
            float planeY = game.player.yPlane;




            for (int x=0; x < frameWidth; x++)
            {
                double cameraX = 2 * x / (float)frameWidth - 1;
                double rayDirX = dirX + planeX * cameraX;
                double rayDirY = dirY + planeY * cameraX;

                int mapX = (int)posX;
                int mapY = (int)posY;

                double sideDistX;
                double sideDistY;


                double deltaDistX = Math.Abs(1 / rayDirX);
                double deltaDistY = Math.Abs(1 / rayDirY);
                double perpWallDist;

                int stepX;
                int stepY;


                int hit = 0;
                int side = 0;

                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (posX - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - posX) * deltaDistX;
                }
                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (posY - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - posY) * deltaDistY;
                }


                while (hit == 0)
                {
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }

                    if (worldMap[mapX, mapY] > 0)
                    {
                        hit = 1;
                    }
                }



                if (side == 0)
                {
                    perpWallDist = (mapX - posX + (1 - stepX) / 2) / rayDirX;
                }
                else
                {
                    perpWallDist = (mapY - posY + (1 - stepY) / 2) / rayDirY;
                }



                Trace.WriteLine("perpWallDistance: " + perpWallDist);

                int lineHeight = (int)(frameHeight / perpWallDist);



                int drawStart = -lineHeight / 2 + frameHeight / 2;
                if (drawStart < 0) drawStart = 0;
                int drawEnd = lineHeight / 2 + frameHeight / 2;
                if (drawEnd >= frameHeight) drawEnd = frameHeight - 1;



                byte R = (byte)((worldMap[mapX, mapY] + 1) * 50);
                byte G = 0;
                byte B = 0;

                //if (side == 1) { R = R / 2; }

                byte[] colour = new byte[4] { R, G, B, 0xff };

                drawSolidWall(colour, x, 1, drawStart, drawEnd);




            }
        }

        public void drawSolidWall(byte[] colour, int x, int width, int drawStart, int drawEnd)
        {
            Trace.WriteLine("Start drawWall: " + x);
            Trace.WriteLine("DrawStart: " + drawStart);
            Trace.WriteLine("DrawEnd: " + drawEnd);
            for (int i = 0; i < (drawStart - drawEnd); i++)
            {
                int drawHeight = drawStart + i;
                int byteOffset = frameStride * drawHeight + x;
                Trace.WriteLine(byteOffset);

                if (byteOffset == (bytesPerPixel * frameHeight * frameWidth))
                {
                    byteOffset -= bytesPerPixel;
                }

                frameBuffer[byteOffset + 0] = colour[0];
                frameBuffer[byteOffset + 1] = colour[1];
                frameBuffer[byteOffset + 2] = colour[2];
                frameBuffer[byteOffset + 3] = colour[3];
            }
            Trace.WriteLine("End drawWall: " + x);


        }

        // Self explanatory, repeatedly calls renderFrame
        public void renderLoop()
        {
            do
            {
                worldMap = game.worldMap;
            } while (worldMap == null);
            

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


            doRayCast();

            /*
            Trace.WriteLine(frameBuffer[frameHeight / 2 * frameStride + 0]);
            Trace.WriteLine(frameBuffer[frameHeight / 2 * frameStride + 1]);
            Trace.WriteLine(frameBuffer[frameHeight / 2 * frameStride + 2]);
            Trace.WriteLine(frameBuffer[frameHeight / 2 * frameStride + 3]);
            */

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
