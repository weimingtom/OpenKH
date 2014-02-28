using System;
namespace khkh_xldMii.Mc
{
	public class AxBone
	{
		public int cur;
		public int parent;
		public int v08;
		public int v0c;
		public float x1;
		public float y1;
		public float z1;
		public float w1;
		public float x2;
		public float y2;
		public float z2;
		public float w2;
		public float x3;
		public float y3;
		public float z3;
		public float w3;
		public AxBone Clone()
		{
			return (AxBone)base.MemberwiseClone();
		}
	}
}
