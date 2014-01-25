namespace khkh_xldMii
{
    using SlimDX;
    using SlimDX.Direct3D9;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PTex3
    {
        public const VertexFormat Format = (VertexFormat.Texture3 | VertexFormat.Position);
        public const int Size = 0x24;
        public float X;
        public float Y;
        public float Z;
        public float Tu1;
        public float Tv1;
        public float Tu2;
        public float Tv2;
        public float Tu3;
        public float Tv3;
        public PTex3(Vector3 pos, Vector2 tex)
        {
            this.X = pos.X;
            this.Y = pos.Y;
            this.Z = pos.Z;
            this.Tu1 = tex.X;
            this.Tv1 = tex.Y;
            this.Tu2 = tex.X;
            this.Tv2 = tex.Y;
            this.Tu3 = tex.X;
            this.Tv3 = tex.Y;
        }
    }
}

