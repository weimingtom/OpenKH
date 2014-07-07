using System.Drawing;

namespace khiiMapv
{
    public class PicIMGD
    {
        public Bitmap pic;
        /// <summary>
        /// Function that will parse the bitmap picture onto the pic var
        /// </summary>
        /// <param name="p">Bitmap picture</param>
        public PicIMGD(Bitmap p)
        {
            pic = p;
        }
    }
}