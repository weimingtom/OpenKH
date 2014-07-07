using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;

namespace khiiMapv
{
    public class CustomVertex
    {
        /// <summary>
        /// Struct for the position of Vertexes
        /// </summary>
        public struct Position
        {
            public float X;
            public float Y;
            public float Z;

            public Position(Vector3 v)
            {
                X = v.X;
                Y = v.Y;
                Z = v.Z;
            }

            public Position(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public static VertexFormat Format
            {
                get { return VertexFormat.Position; }
            }

            public static int Size
            {
                get { return Marshal.SizeOf(typeof (Position)); }
            }
        }
        /// <summary>
        /// Struct for the position of colored Vertexes
        /// </summary>
        public struct PositionColored
        {
            public int Color;
            public float X;
            public float Y;
            public float Z;

            public PositionColored(Vector3 v, int clr)
            {
                X = v.X;
                Y = v.Y;
                Z = v.Z;
                Color = clr;
            }

            public PositionColored(float x, float y, float z, int clr)
            {
                X = x;
                Y = y;
                Z = z;
                Color = clr;
            }

            public Vector3 Position
            {
                get { return new Vector3(X, Y, Z); }
            }

            public static VertexFormat Format
            {
                get { return VertexFormat.Diffuse | VertexFormat.Position; }
            }

            public static int Size
            {
                get { return Marshal.SizeOf(typeof (PositionColored)); }
            }
        }
        /// <summary>
        /// Struct for the position of textured and colored Vertexes
        /// </summary>
        public struct PositionColoredTextured
        {
            public int Color;
            public float Tu;
            public float Tv;
            public float X;
            public float Y;
            public float Z;

            public PositionColoredTextured(Vector3 v, int clr, float tu, float tv)
            {
                X = v.X;
                Y = v.Y;
                Z = v.Z;
                Color = clr;
                Tu = tu;
                Tv = tv;
            }

            public PositionColoredTextured(float x, float y, float z, int clr, float tu, float tv)
            {
                X = x;
                Y = y;
                Z = z;
                Color = clr;
                Tu = tu;
                Tv = tv;
            }

            public Vector3 Position
            {
                get { return new Vector3(X, Y, Z); }
            }

            public static VertexFormat Format
            {
                get { return VertexFormat.Texture1 | VertexFormat.Diffuse | VertexFormat.Position; }
            }

            public static int Size
            {
                get { return Marshal.SizeOf(typeof (PositionColoredTextured)); }
            }
        }
        /// <summary>
        /// Struct for normal colored vertexes
        /// </summary>
        public struct PositionNormalColored
        {
            public int Color;
            public float Nx;
            public float Ny;
            public float Nz;
            public float X;
            public float Y;
            public float Z;

            public PositionNormalColored(Vector3 v, Vector3 n, int c)
            {
                X = v.X;
                Y = v.Y;
                Z = v.Z;
                Nx = n.X;
                Ny = n.Y;
                Nz = n.Z;
                Color = c;
            }

            public PositionNormalColored(float x, float y, float z, float nx, float ny, float nz, int c)
            {
                X = x;
                Y = y;
                Z = z;
                Nx = nx;
                Ny = ny;
                Nz = nz;
                Color = c;
            }

            public static VertexFormat Format
            {
                get { return VertexFormat.Diffuse | VertexFormat.PositionNormal; }
            }

            public static int Size
            {
                get { return Marshal.SizeOf(typeof (PositionNormalColored)); }
            }
        }
    }
}