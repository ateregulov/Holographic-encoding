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
		BitmapData ReadFileToBitmapData(string fileName)
		{
			Bitmap source = new Bitmap(fileName, true);
			BitmapData bitmapData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			return bitmapData;
		}



    }
}
