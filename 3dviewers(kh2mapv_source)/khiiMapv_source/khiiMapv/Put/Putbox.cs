namespace khiiMapv.Put
{
    using khiiMapv.CollTest;
    using SlimDX;
    using System;
    using System.Collections.Generic;

    public class Putbox
    {
        public List<Putfragment> alf;
        public List<int> ali;
        public List<Vector3> alv;

        public Putbox()
        {
            this.alv = new List<Vector3>();
            this.ali = new List<int>();
            this.alf = new List<Putfragment>();
            base..ctor();
            return;
        }

        public Putfragment Add(Co1 o1)
        {
            Putfragment putfragment;
            return this.AddBox(o1.Min, o1.Max);
        }

        public Putfragment Add(Co2 o1)
        {
            Putfragment putfragment;
            return this.AddBox(o1.Min, o1.Max);
        }

        public unsafe Putfragment AddBox(Vector3 v0, Vector3 v1)
        {
            int num;
            int num2;
            Putfragment putfragment;
            num = this.alv.Count;
            num2 = this.ali.Count;
            this.alv.Add(new Vector3(Math.Min(&v0.X, &v1.X), Math.Min(&v0.Y, &v1.Y), Math.Min(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Max(&v0.X, &v1.X), Math.Min(&v0.Y, &v1.Y), Math.Min(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Min(&v0.X, &v1.X), Math.Max(&v0.Y, &v1.Y), Math.Min(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Max(&v0.X, &v1.X), Math.Max(&v0.Y, &v1.Y), Math.Min(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Min(&v0.X, &v1.X), Math.Min(&v0.Y, &v1.Y), Math.Max(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Max(&v0.X, &v1.X), Math.Min(&v0.Y, &v1.Y), Math.Max(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Min(&v0.X, &v1.X), Math.Max(&v0.Y, &v1.Y), Math.Max(&v0.Z, &v1.Z)));
            this.alv.Add(new Vector3(Math.Max(&v0.X, &v1.X), Math.Max(&v0.Y, &v1.Y), Math.Max(&v0.Z, &v1.Z)));
            this.AddQuad(0, 2, 3, 1);
            this.AddQuad(4, 5, 7, 6);
            this.AddQuad(2, 6, 7, 3);
            this.AddQuad(0, 1, 5, 4);
            this.AddQuad(0, 4, 6, 2);
            this.AddQuad(1, 3, 7, 5);
            putfragment = new Putfragment(num, 0, 8, num2, 12);
            return putfragment;
        }

        private void AddQuad(int i0, int i1, int i2, int i3)
        {
            this.ali.Add(i0);
            this.ali.Add(i1);
            this.ali.Add(i2);
            this.ali.Add(i2);
            this.ali.Add(i3);
            this.ali.Add(i0);
            return;
        }
    }
}

