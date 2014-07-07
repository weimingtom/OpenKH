using System.Collections.Generic;
using System.Drawing;

namespace khiiMapv
{
    public class MTex
    {
        public int[] alIndirIndex = new int[0];
        public Bitmap[] pics;

        public MTex(List<Bitmap> pics)
        {
            this.pics = pics.ToArray();
        }
    }
}