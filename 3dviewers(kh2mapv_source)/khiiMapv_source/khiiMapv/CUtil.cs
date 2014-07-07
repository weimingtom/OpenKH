using System;
using System.Drawing;

namespace khiiMapv
{
    internal class CUtil
    {
        /// <summary>
        /// Utility function for process the Gamma in the textures
        /// </summary>
        /// <param name="a">Color</param>
        /// <param name="gamma">Gamma</param>
        /// <returns></returns>
        public static Color Gamma(Color a, float gamma)
        {
            return Color.FromArgb(a.A, Math.Min(255, (int) (Math.Pow(a.R/255.0, gamma)*255.0)),
                Math.Min(255, (int) (Math.Pow(a.G/255.0, gamma)*255.0)),
                Math.Min(255, (int) (Math.Pow(a.B/255.0, gamma)*255.0)));
        }
    }
}