using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Holography
{
    class WriteHologram
    {
        public static Bitmap Write(Bitmap bmpSource, Bitmap bmpHologram, DataVariables dv)
        {
            double d; //расстояние между точками считаемыми точками
            double cph; //косинус фазы волны
            double intensity; //интенсивность света
            double r, g, b; // цвета
            int xS, xH, yS, yH;
            Bitmap bmpNew = new Bitmap(bmpHologram.Width, bmpHologram.Height, PixelFormat.Format24bppRgb);
            BitmapData bmpDataSource = bmpSource.LockBits(new Rectangle(0, 0, bmpSource.Width, bmpSource.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bmpDataHologram = bmpNew.LockBits(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int sWidth = bmpDataSource.Width;
            int sHeight = bmpDataSource.Height;

            int hWidth = bmpDataHologram.Width;
            int hHeight = bmpDataHologram.Height;

            IntPtr oldFPx = bmpDataSource.Scan0;
            IntPtr newFPx = bmpDataHologram.Scan0;

            unsafe
            {

                byte* oldBPtr = (byte*)oldFPx.ToPointer();
                byte* newBPtr = (byte*)newFPx.ToPointer();

                byte* oldBPtrHelper = oldBPtr;
                byte* newBPtrHelper = newBPtr;

                for (xH = 0; xH < hWidth; xH++)
                {
                    Console.WriteLine(xH);
                    for (yH = 0; yH < hHeight; yH++)
                    {
                        r = 128;
                        g = 128;
                        b = 128;

                        oldFPx = bmpDataSource.Scan0;
                        oldBPtr = (byte*)oldFPx.ToPointer();
                        oldBPtrHelper = oldBPtr;

                        for (xS = 0; xS < sWidth; xS++)
                        {
                            for (yS = 0; yS < sHeight; yS++)
                            {
                                byte rS = *(oldBPtr++);
                                byte gS = *(oldBPtr++);
                                byte bS = *(oldBPtr++);

                                d = Math.Sqrt((xH - xS + dv.p) * (xH - xS + dv.p) + (yH - yS + dv.p) * (yH - yS + dv.p) + dv.z * dv.z);
                                cph = Math.Cos(d / dv.w);
                                intensity = 1 / (d * d);
                                //intensity = 1; 
                                r += rS * cph * intensity * dv.sensitivityHologram;
                                g += gS * cph * intensity * dv.sensitivityHologram;
                                b += bS * cph * intensity * dv.sensitivityHologram;

                            }
                            oldBPtrHelper += bmpDataSource.Stride;
                            oldBPtr = oldBPtrHelper;
                        }

                        r = (Math.Abs(Math.Truncate(r)) < 255) ? Math.Abs(Math.Truncate(r)) : 255;
                        g = (Math.Abs(Math.Truncate(g)) < 255) ? Math.Abs(Math.Truncate(g)) : 255;
                        b = (Math.Abs(Math.Truncate(b)) < 255) ? Math.Abs(Math.Truncate(b)) : 255;

                        *(newBPtr++) = (byte)r;
                        *(newBPtr++) = (byte)g;
                        *(newBPtr++) = (byte)b;

                    }
                    newBPtrHelper += bmpDataHologram.Stride;
                    newBPtr = newBPtrHelper;
                }
            }
            bmpNew.UnlockBits(bmpDataHologram);
            bmpSource.UnlockBits(bmpDataSource);
            return bmpNew;
        }
    }
}
