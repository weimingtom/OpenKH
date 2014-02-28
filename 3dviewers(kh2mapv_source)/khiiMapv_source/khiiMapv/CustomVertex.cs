using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Runtime.InteropServices;
namespace khiiMapv
{
	public class CustomVertex
	{
		public struct PositionColoredTextured
		{
			public float X;
			public float Y;
			public float Z;
			public int Color;
			public float Tu;
			public float Tv;
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
					return VertexFormat.Texture1 | VertexFormat.Diffuse | VertexFormat.Position;
				}
			}
			public static int Size
			{
				get
				{
					return Marshal.SizeOf(typeof(CustomVertex.PositionColoredTextured));
				}
			}
			public PositionColoredTextured(Vector3 v, int clr, float tu, float tv)
			{
				this.X = v.X;
				this.Y = v.Y;
				this.Z = v.Z;
				this.Color = clr;
				this.Tu = tu;
				this.Tv = tv;
			}
			public PositionColoredTextured(float x, float y, float z, int clr, float tu, float tv)
			{
				this.X = x;
				this.Y = y;
				this.Z = z;
				this.Color = clr;
				this.Tu = tu;
				this.Tv = tv;
			}
		}
		public struct PositionColored
		{
			public float X;
			public float Y;
			public float Z;
			public int Color;
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
					return VertexFormat.Diffuse | VertexFormat.Position;
				}
			}
			public static int Size
			{
				get
				{
					return Marshal.SizeOf(typeof(CustomVertex.PositionColored));
				}
			}
			public PositionColored(Vector3 v, int clr)
			{
				this.X = v.X;
				this.Y = v.Y;
				this.Z = v.Z;
				this.Color = clr;
			}
			public PositionColored(float x, float y, float z, int clr)
			{
				this.X = x;
				this.Y = y;
				this.Z = z;
				this.Color = clr;
			}
		}
		public struct Position
		{
			public float X;
			public float Y;
			public float Z;
			public static VertexFormat Format
			{
				get
				{
					return VertexFormat.Position;
				}
			}
			public static int Size
			{
				get
				{
					return Marshal.SizeOf(typeof(CustomVertex.Position));
				}
			}
			public Position(Vector3 v)
			{
				this.X = v.X;
				this.Y = v.Y;
				this.Z = v.Z;
			}
			public Position(float x, float y, float z)
			{
				this.X = x;
				this.Y = y;
				this.Z = z;
			}
		}
		public struct PositionNormalColored
		{
			public float X;
			public float Y;
			public float Z;
			public float Nx;
			public float Ny;
			public float Nz;
			public int Color;
			public static VertexFormat Format
			{
				get
				{
					return VertexFormat.Diffuse | VertexFormat.PositionNormal;
				}
			}
			public static int Size
			{
				get
				{
					return Marshal.SizeOf(typeof(CustomVertex.PositionNormalColored));
				}
			}
			public PositionNormalColored(Vector3 v, Vector3 n, int c)
			{
				this.X = v.X;
				this.Y = v.Y;
				this.Z = v.Z;
				this.Nx = n.X;
				this.Ny = n.Y;
				this.Nz = n.Z;
				this.Color = c;
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
			}
		}
	}
}
