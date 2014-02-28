using khiiMapv.CollTest;
using SlimDX;
using System;
using System.Collections.Generic;
namespace khiiMapv.Put
{
	public class Putbox
	{
		public List<Vector3> alv = new List<Vector3>();
		public List<int> ali = new List<int>();
		public List<Putfragment> alf = new List<Putfragment>();
		public Putfragment Add(Co1 o1)
		{
			return this.AddBox(o1.Min, o1.Max);
		}
		public Putfragment Add(Co2 o1)
		{
			return this.AddBox(o1.Min, o1.Max);
		}
		public Putfragment AddBox(Vector3 v0, Vector3 v1)
		{
			int count = this.alv.Count;
			int count2 = this.ali.Count;
			this.alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
			this.alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
			this.AddQuad(0, 2, 3, 1);
			this.AddQuad(4, 5, 7, 6);
			this.AddQuad(2, 6, 7, 3);
			this.AddQuad(0, 1, 5, 4);
			this.AddQuad(0, 4, 6, 2);
			this.AddQuad(1, 3, 7, 5);
			return new Putfragment(count, 0, 8, count2, 12);
		}
		private void AddQuad(int i0, int i1, int i2, int i3)
		{
			this.ali.Add(i0);
			this.ali.Add(i1);
			this.ali.Add(i2);
			this.ali.Add(i2);
			this.ali.Add(i3);
			this.ali.Add(i0);
		}
	}
}
