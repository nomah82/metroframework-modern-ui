/**
 * MetroFramework - Modern UI for WinForms
 * 
 * The MIT License (MIT)
 * Copyright (c) 2011 Sven Walter, http://github.com/viperneo
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of 
 * this software and associated documentation files (the "Software"), to deal in the 
 * Software without restriction, including without limitation the rights to use, copy, 
 * modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
 * and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
 * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
 * PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
 * OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace MetroFramework
{
    class MetroImage
    {
        public static Image ResizeImage(Image imgToResize, Rectangle maxOffset)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (float)maxOffset.Width / sourceWidth;
            nPercentH = (float)maxOffset.Height / sourceHeight;

            nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            return imgToResize.GetThumbnailImage(destWidth, destHeight, null, IntPtr.Zero);
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static Color ContrastColor(Color color)
        {
            int d = 0;

            // Counting the perceptive luminance - human eye favors green color... 
            double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

            if (a < 0.5)
                d = 0; // bright colors - black font
            else
                d = 255; // dark colors - white font

            return Color.FromArgb(d, d, d);
        }

        public static Bitmap WhiteBlackToTransparentFore(Bitmap bitmapImage, Color foreColor)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = (byte)(pixelColor.A);
                    R = (byte)(pixelColor.R);
                    G = (byte)(pixelColor.G);
                    B = (byte)(pixelColor.B);

                    int[] diff = { Math.Abs(R - G), Math.Abs(R - B), Math.Abs(G - B) };
                    if (diff.Max() < 5)
                    {

                        if (R < 50 && G < 50 && G < 50)
                        {
                            R = foreColor.R;
                            G = foreColor.G;
                            B = foreColor.B;
                        }
                        else if (R > 235 && G > 235 && G > 235)
                        {
                            A = 0;
                        }

                        //bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                        bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    }
                }
            }
            return bitmapImage;
        }

        public static Bitmap WhiteBlackToTransparentForeStyle(Bitmap bitmapImage, Color foreColor, Color style)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = (byte)(pixelColor.A);
                    R = (byte)(pixelColor.R);
                    G = (byte)(pixelColor.G);
                    B = (byte)(pixelColor.B);

                    int[] diff = { Math.Abs(R - G), Math.Abs(R - B), Math.Abs(G - B) };
                    if (diff.Max() < 5)
                    {

                        if (R < 50 && G < 50 && G < 50)
                        {
                            R = foreColor.R;
                            G = foreColor.G;
                            B = foreColor.B;
                            //A = foreColor.A;
                        }
                        else if (R > 235 && G > 235 && G > 235)
                        {
                            A = 0;
                        }

                        //bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                        bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    }
                    else
                    {
                        R = style.R;
                        G = style.G;
                        B = style.G;
                        bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));

                    }
                }
            }
            return bitmapImage;
        }

        public static Bitmap WhiteBlackToBackFore(Bitmap bitmapImage, Color backColor, Color foreColor)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(pixelColor.R);
                    G = (byte)(pixelColor.G);
                    B = (byte)(pixelColor.B);

                    if (R < 20 && G < 20 && G < 20)
                    {
                        R = foreColor.R;
                        G = foreColor.G;
                        B = foreColor.B;
                        A = foreColor.A;
                    }
                    else if (R > 235 && G > 235 && G > 235)
                    {
                        R = backColor.R;
                        G = backColor.G;
                        B = backColor.B;
                        A = backColor.A;
                    }

                    //bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            return bitmapImage;
        }


        public static Bitmap WhiteToTransparent(Bitmap bitmapImage)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(pixelColor.R);
                    G = (byte)(pixelColor.G);
                    B = (byte)(pixelColor.B);

                    if (R > 235 && G > 235 && G > 235)
                    {
                        A = 0;
                    }
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            return bitmapImage;
        }

        public static Bitmap ApplyInvert(Image bitmap)
        {
            Bitmap bitmapImage = new Bitmap(bitmap);
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            return bitmapImage;

        }

        public static Bitmap ApplyInvertOnlyGray(Image bitmap)
        {
            Bitmap bitmapImage = new Bitmap(bitmap);
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);

                    int[] diff = { Math.Abs(R - G), Math.Abs(R - B), Math.Abs(G - B) };
                    if (diff.Max()<5)
                        bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            return bitmapImage;
        }

        public static Bitmap ConvertGrayScale(Image image)
        {
            Bitmap d = new Bitmap(image);

            for (int i = 0; i < d.Width; i++)
            {
                for (int x = 0; x < d.Height; x++)
                {
                    Color oc = d.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    d.SetPixel(i, x, nc);
                }
            }
            return d;
        }


        /*
        public static Bitmap ApplyInvert(Bitmap bitmapImage)
        {
            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);

                    if (R <= 0) R = 17;
                    if (G <= 0) G = 17;
                    if (B <= 0) B = 17;

                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)R, (int)G, (int)B));
                }
            }

            return bitmapImage;
        }*/

    }
}
