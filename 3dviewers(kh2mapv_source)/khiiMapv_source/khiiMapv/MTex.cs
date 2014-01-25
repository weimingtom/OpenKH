namespace khiiMapv
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class MTex
    {
        public int[] alIndirIndex;
        public Bitmap[] pics;

        public MTex(List<Bitmap> pics)
        {
            this.alIndirIndex = new int[0];
            base..ctor();
            this.pics = pics.ToArray();
            return;
        }
    }
}

