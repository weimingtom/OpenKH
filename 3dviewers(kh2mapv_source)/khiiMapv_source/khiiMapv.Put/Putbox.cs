using System;
using System.Collections.Generic;
using khiiMapv.CollTest;
using SlimDX;

namespace khiiMapv.Put
{
    public class Putbox
    {
        public List<Putfragment> alf = new List<Putfragment>();
        public List<int> ali = new List<int>();
        public List<Vector3> alv = new List<Vector3>();

        public Putfragment Add(Co1 o1)
        {
            return AddBox(o1.Min, o1.Max);
        }

        public Putfragment Add(Co2 o1)
        {
            return AddBox(o1.Min, o1.Max);
        }

        public Putfragment AddBox(Vector3 v0, Vector3 v1)
        {
            int count = alv.Count;
            int count2 = ali.Count;
            alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Min(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Min(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Min(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
            alv.Add(new Vector3(Math.Max(v0.X, v1.X), Math.Max(v0.Y, v1.Y), Math.Max(v0.Z, v1.Z)));
            AddQuad(0, 2, 3, 1);
            AddQuad(4, 5, 7, 6);
            AddQuad(2, 6, 7, 3);
            AddQuad(0, 1, 5, 4);
            AddQuad(0, 4, 6, 2);
            AddQuad(1, 3, 7, 5);
            return new Putfragment(count, 0, 8, count2, 12);
        }

        private void AddQuad(int i0, int i1, int i2, int i3)
        {
            ali.Add(i0);
            ali.Add(i1);
            ali.Add(i2);
            ali.Add(i2);
            ali.Add(i3);
            ali.Add(i0);
        }
    }
}