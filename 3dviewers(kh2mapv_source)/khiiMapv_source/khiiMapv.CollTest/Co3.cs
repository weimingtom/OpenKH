using System;
using System.IO;
namespace khiiMapv.CollTest
{
	public class Co3
	{
		public short v0;
		public short v2;
		public short v4;
		public short v6;
		public short v8;
		public short va;
		public short vc;
		public short ve;
		public int vi0
		{
			get
			{
				return (int)this.v2;
			}
		}
		public int vi1
		{
			get
			{
				return (int)this.v4;
			}
		}
		public int vi2
		{
			get
			{
				return (int)this.v6;
			}
		}
		public int vi3
		{
			get
			{
				return (int)this.v8;
			}
		}
		public int PlaneCo5
		{
			get
			{
				return (int)this.va;
			}
		}
		public override string ToString()
		{
			return string.Format("{0,4} PolyCo4({1,4},{2,4},{3,4},{4,4}) PlaneCo5({5,3}) {6,3} {7,3}", new object[]
			{
				this.v0,
				this.v2,
				this.v4,
				this.v6,
				this.v8,
				this.va,
				this.vc,
				this.ve
			});
		}
		public Co3(BinaryReader br)
		{
			this.v0 = br.ReadInt16();
			this.v2 = br.ReadInt16();
			this.v4 = br.ReadInt16();
			this.v6 = br.ReadInt16();
			this.v8 = br.ReadInt16();
			this.va = br.ReadInt16();
			this.vc = br.ReadInt16();
			this.ve = br.ReadInt16();
		}
	}
}
