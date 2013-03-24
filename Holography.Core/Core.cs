using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;

namespace Holography.Core
{
    public class Core
    {
		
		public static void DrawLissajous(Settings settings)
		{
			Bitmap bmp = new Bitmap(settings.destinationWidth, settings.destinationHeight);
			Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
			BitmapData bitmapData = bmp.LockBits(rectangle, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
			int x;
			int y;

			IntPtr intPtr = bitmapData.Scan0;

			unsafe
			{
			byte* ptr = (byte*)intPtr.ToPointer();

			//fixed (byte* ptr = &bitmapData)
			
				int stride = bitmapData.Stride;

				for (int n = 0; n < 31400 * 2; n++)
				{
					x = xf(n);
					y = yf(n);
					ptr[(x * 3) + y * stride + 2] = 250;  //red channel
					ptr[(x * 3) + y * stride + 1] = 100;  //green channel
					ptr[(x * 3) + y * stride] = 100;	   //blue channel
				}
			}
			bmp.UnlockBits(bitmapData);
			bmp.Save(settings.destinationFile);
		}

		static int xf(int n)
		{ 

			return (int)Math.Truncate (150 + 120*Math.Cos(103* n/100000.0));
		}
		static int yf(int n)
		{
			return (int)Math.Truncate(150 + 120 * Math.Sin(102* n / 100000.0));
		}

    }
}
