using SlimDX;
using System;
using System.IO;
namespace khiiMapv.CollTest
{
	public class Co1
	{
		public short v00;
		public short v02;
		public short v04;
		public short v06;
		public short v08;
		public short v0a;
		public short v0c;
		public short v0e;
		public short v10;
		public short v12;
		public short v14;
		public short v16;
		public short v18;
		public short v1a;
		public short v1c;
		public short v1e;
		public Vector3 Min
		{
			get
			{
				return new Vector3((float)this.v10, (float)this.v12, (float)this.v14);
			}
		}
		public Vector3 Max
		{
			get
			{
				return new Vector3((float)this.v16, (float)this.v18, (float)this.v1a);
			}
		}
		public override string ToString()
		{
			return string.Format("?({0,3},{1,3},{2,3},{3,3},{4,3},{5,3},{6,3},{7,3}) ", new object[]
			{
				this.v00,
				this.v02,
				this.v04,
				this.v06,
				this.v08,
				this.v0a,
				this.v0c,
				this.v0e
			}) + string.Format("bbox-min({0,7}, {1,6}, {2,6}) bbox-max({3,6}, {4,6}, {5,6}) {6,3} {7,3}", new object[]
			{
				this.v10,
				this.v12,
				this.v14,
				this.v16,
				this.v18,
				this.v1a,
				this.v1c,
				this.v1e
			});
		}
		public Co1(BinaryReader br)
		{
			this.v00 = br.ReadInt16();
			this.v02 = br.ReadInt16();
			this.v04 = br.ReadInt16();
			this.v06 = br.ReadInt16();
			this.v08 = br.ReadInt16();
			this.v0a = br.ReadInt16();
			this.v0c = br.ReadInt16();
			this.v0e = br.ReadInt16();
			this.v10 = br.ReadInt16();
			this.v12 = br.ReadInt16();
			this.v14 = br.ReadInt16();
			this.v16 = br.ReadInt16();
			this.v18 = br.ReadInt16();
			this.v1a = br.ReadInt16();
			this.v1c = br.ReadInt16();
			this.v1e = br.ReadInt16();
		}
	}
}
