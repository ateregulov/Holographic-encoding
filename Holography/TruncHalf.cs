using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Holography
{
    class TruncHalf
    {
        public static Bitmap Cut(Bitmap bp)
        { 
            Color cc = Color.FromArgb(128,128,128);
            int x,y;
            double h = bp.Height / 2;
            int h2 = (int)Math.Truncate(h);
            for (y = h2; y < bp.Height; y++)
                for (x = 0; x < bp.Width; x++)
                    bp.SetPixel(x,y, cc);
            return bp;
        }
    }
}
