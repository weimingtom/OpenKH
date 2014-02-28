using System;
using System.Drawing;
namespace khiiMapv
{
	internal class CUtil
	{
		public static Color Gamma(Color a, float gamma)
		{
			return Color.FromArgb((int)a.A, Math.Min(255, (int)(Math.Pow((double)a.R / 255.0, (double)gamma) * 255.0)), Math.Min(255, (int)(Math.Pow((double)a.G / 255.0, (double)gamma) * 255.0)), Math.Min(255, (int)(Math.Pow((double)a.B / 255.0, (double)gamma) * 255.0)));
		}
	}
}
