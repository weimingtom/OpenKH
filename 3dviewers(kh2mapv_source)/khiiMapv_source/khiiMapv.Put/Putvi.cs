using SlimDX;
using SlimDX.Direct3D9;
using System;
namespace khiiMapv.Put
{
	public class Putvi : IDisposable
	{
		public VertexBuffer vb;
		public IndexBuffer ib;
		public Putvi(Putbox p, Device dev)
		{
			this.vb = new VertexBuffer(dev, CustomVertex.Position.Size * p.alv.Count, Usage.Points, CustomVertex.Position.Format, Pool.Managed);
			DataStream dataStream = this.vb.Lock(0, 0, LockFlags.Discard);
			try
			{
				dataStream.WriteRange<Vector3>(p.alv.ToArray());
			}
			finally
			{
				this.vb.Unlock();
			}
			this.ib = new IndexBuffer(dev, 4 * p.ali.Count, Usage.Points, Pool.Managed, false);
			DataStream dataStream2 = this.ib.Lock(0, 0, LockFlags.Discard);
			try
			{
				dataStream2.WriteRange<int>(p.ali.ToArray());
			}
			finally
			{
				this.ib.Unlock();
			}
		}
		public void Dispose()
		{
			if (this.vb != null)
			{
				this.vb.Dispose();
			}
			if (this.ib != null)
			{
				this.ib.Dispose();
			}
		}
	}
}
