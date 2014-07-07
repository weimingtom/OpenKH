using System;
using SlimDX;
using SlimDX.Direct3D9;

namespace khiiMapv.Put
{
    public class Putvi : IDisposable
    {
        public IndexBuffer ib;
        public VertexBuffer vb;

        public Putvi(Putbox p, Device dev)
        {
            vb = new VertexBuffer(dev, CustomVertex.Position.Size*p.alv.Count, Usage.Points,
                CustomVertex.Position.Format, Pool.Managed);
            DataStream dataStream = vb.Lock(0, 0, LockFlags.Discard);
            try
            {
                dataStream.WriteRange(p.alv.ToArray());
            }
            finally
            {
                vb.Unlock();
            }
            ib = new IndexBuffer(dev, 4*p.ali.Count, Usage.Points, Pool.Managed, false);
            DataStream dataStream2 = ib.Lock(0, 0, LockFlags.Discard);
            try
            {
                dataStream2.WriteRange(p.ali.ToArray());
            }
            finally
            {
                ib.Unlock();
            }
        }

        public void Dispose()
        {
            if (vb != null)
            {
                vb.Dispose();
            }
            if (ib != null)
            {
                ib.Dispose();
            }
        }
    }
}