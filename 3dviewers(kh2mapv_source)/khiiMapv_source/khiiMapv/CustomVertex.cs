namespace khiiMapv
{
    using SlimDX;
    using SlimDX.Direct3D9;
    using System;
    using System.Runtime.InteropServices;

    public class CustomVertex
    {
        public CustomVertex()
        {
            base..ctor();
            return;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Position
        {
            public float X;
            public float Y;
            public float Z;
            public unsafe Position(Vector3 v)
            {
                this.X = &v.X;
                this.Y = &v.Y;
                this.Z = &v.Z;
                return;
            }

            public Position(float x, float y, float z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                return;
            }

            public static VertexFormat Format
            {
                get
                {
                    return 2;
                }
            }
            public static int Size
            {
                get
                {
                    return Marshal.SizeOf(typeof(CustomVertex.Position));
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PositionColored
        {
            public float X;
            public float Y;
            public float Z;
            public int Color;
            public unsafe PositionColored(Vector3 v, int clr)
            {
                this.X = &v.X;
                this.Y = &v.Y;
                this.Z = &v.Z;
                this.Color = clr;
                return;
            }

            public PositionColored(float x, float y, float z, int clr)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Color = clr;
                return;
            }

            public Vector3 Position
            {
                get
                {
                    return new Vector3(this.X, this.Y, this.Z);
                }
            }
            public static VertexFormat Format
            {
                get
                {
                    return 0x42;
                }
            }
            public static int Size
            {
                get
                {
                    return Marshal.SizeOf(typeof(CustomVertex.PositionColored));
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PositionColoredTextured
        {
            public float X;
            public float Y;
            public float Z;
            public int Color;
            public float Tu;
            public float Tv;
            public unsafe PositionColoredTextured(Vector3 v, int clr, float tu, float tv)
            {
                this.X = &v.X;
                this.Y = &v.Y;
                this.Z = &v.Z;
                this.Color = clr;
                this.Tu = tu;
                this.Tv = tv;
                return;
            }

            public PositionColoredTextured(float x, float y, float z, int clr, float tu, float tv)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Color = clr;
                this.Tu = tu;
                this.Tv = tv;
                return;
            }

            public Vector3 Position
            {
                get
                {
                    return new Vector3(this.X, this.Y, this.Z);
                }
            }
            public static VertexFormat Format
            {
                get
                {
                    return 0x142;
                }
            }
            public static int Size
            {
                get
                {
                    return Marshal.SizeOf(typeof(CustomVertex.PositionColoredTextured));
                }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PositionNormalColored
        {
            public float X;
            public float Y;
            public float Z;
            public float Nx;
            public float Ny;
            public float Nz;
            public int Color;
            public unsafe PositionNormalColored(Vector3 v, Vector3 n, int c)
            {
                this.X = &v.X;
                this.Y = &v.Y;
                this.Z = &v.Z;
                this.Nx = &n.X;
                this.Ny = &n.Y;
                this.Nz = &n.Z;
                this.Color = c;
                return;
            }

            public PositionNormalColored(float x, float y, float z, float nx, float ny, float nz, int c)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Nx = nx;
                this.Ny = ny;
                this.Nz = nz;
                this.Color = c;
                return;
            }

            public static VertexFormat Format
            {
                get
                {
                    return 0x52;
                }
            }
            public static int Size
            {
                get
                {
                    return Marshal.SizeOf(typeof(CustomVertex.PositionNormalColored));
                }
            }
        }
    }
}

