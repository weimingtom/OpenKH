using SlimDX;
using SlimDX.Direct3D9;

namespace khkh_xldMii
{
    public struct PTex3
    {
        public const VertexFormat Format = VertexFormat.Texture3 | VertexFormat.Position;
        public const int Size = 36;
        public float Tu1;
        public float Tu2;
        public float Tu3;
        public float Tv1;
        public float Tv2;
        public float Tv3;
        public float X;
        public float Y;
        public float Z;

        public PTex3(Vector3 pos, Vector2 tex)
        {
            X = pos.X;
            Y = pos.Y;
            Z = pos.Z;
            Tu1 = tex.X;
            Tv1 = tex.Y;
            Tu2 = tex.X;
            Tv2 = tex.Y;
            Tu3 = tex.X;
            Tv3 = tex.Y;
        }
    }
}