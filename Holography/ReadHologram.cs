using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Holography
{
    class ReadHologram
    {
        public static Bitmap Read(Bitmap bmpHologram, Bitmap bmpOutput, DataVariables dv)
        {
            double d; //расстояние между точками считаемыми точками
            double cph; //косинус фазы волны
            double intensity; //интенсивность света
            double r, g, b; // цвета
            int xO, xH, yO, yH;
            Bitmap bmpNew = new Bitmap(bmpOutput.Width, bmpOutput.Height, PixelFormat.Format24bppRgb);
            BitmapData bmpDataHologram = bmpHologram.LockBits(new Rectangle(0, 0, bmpHologram.Width, bmpHologram.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData bmpDataOutput = bmpNew.LockBits(new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            int hWidth = bmpDataHologram.Width;
            int hHeight = bmpDataHologram.Height;

            int oWidth = bmpDataOutput.Width;
            int oHeight = bmpDataOutput.Height;

            IntPtr oldFPx = bmpDataHologram.Scan0;
            IntPtr newFPx = bmpDataOutput.Scan0;

            unsafe
            {

                byte* oldBPtr = (byte*)oldFPx.ToPointer();
                byte* newBPtr = (byte*)newFPx.ToPointer();

                byte* oldBPtrHelper = oldBPtr;
                byte* newBPtrHelper = newBPtr;

                for (xO = 0; xO < oWidth; xO++)
                {
                    Console.WriteLine(xO);
                    for (yO = 0; yO < oHeight; yO++)
                    {
                        r = 0;
                        g = 0;
                        b = 0;

                        oldFPx = bmpDataHologram.Scan0;
                        oldBPtr = (byte*)oldFPx.ToPointer();
                        oldBPtrHelper = oldBPtr;

                        for (xH = 0; xH < hWidth; xH++)
                        {
                            for (yH = 0; yH < hHeight; yH++)
                            {
                                byte rS = *(oldBPtr++);
                                byte gS = *(oldBPtr++);
                                byte bS = *(oldBPtr++);

                                d = Math.Sqrt((xH - xO + dv.p) * (xH - xO + dv.p) + (yH - yO + dv.p) * (yH - yO + dv.p) + dv.z * dv.z);
                                cph = Math.Cos(d / dv.w);
                                intensity = 1 / (d * d);
                                //intensity = 1; 
                                r += rS * cph * intensity * dv.sensitivityOutput;
                                g += gS * cph * intensity * dv.sensitivityOutput;
                                b += bS * cph * intensity * dv.sensitivityOutput;

                            }
                            oldBPtrHelper += bmpDataHologram.Stride;
                            oldBPtr = oldBPtrHelper;
                        }

                        r = (Math.Abs(Math.Truncate(r)) < 255) ? Math.Abs(Math.Truncate(r)) : 255;
                        g = (Math.Abs(Math.Truncate(g)) < 255) ? Math.Abs(Math.Truncate(g)) : 255;
                        b = (Math.Abs(Math.Truncate(b)) < 255) ? Math.Abs(Math.Truncate(b)) : 255;

                        *(newBPtr++) = (byte)r;
                        *(newBPtr++) = (byte)g;
                        *(newBPtr++) = (byte)b;

                    }
                    newBPtrHelper += bmpDataOutput.Stride;
                    newBPtr = newBPtrHelper;
                }
            }


            bmpNew.UnlockBits(bmpDataOutput);
            bmpHologram.UnlockBits(bmpDataHologram);
            return bmpNew;
        }
    }
}
