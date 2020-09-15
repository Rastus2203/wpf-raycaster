// None of this code is actually used, I just store stuff here while working


namespace Tester
{ 
    private class Tester 
    {
        private void Tester()
        {

            BitmapSource blackBitmap;


            int imgWidth = 10;
            int imgHeight = 10;
            int bytesPerPixel = 4;

            int stride = imgWidth * bytesPerPixel;

            byte[] blackImgData = new byte[imgWidth * imgHeight * bytesPerPixel];

            for (int i = 0; i < blackImgData.Length; i++)
            {
                if (i % 4 == 3)
                {
                    blackImgData[i] = 0xff;
                }
                else
                {
                    blackImgData[i] = 0x00;
                }

            }

            blackImgData[0] = 0xaa;


            blackBitmap = BitmapSource.Create(imgWidth, imgHeight, 96, 96, PixelFormats.Bgra32, null, blackImgData, stride);
            fractalView.Source = blackBitmap;
        }
    }
}