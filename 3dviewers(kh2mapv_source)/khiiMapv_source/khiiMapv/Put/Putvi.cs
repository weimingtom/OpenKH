namespace khiiMapv.Put
{
    using khiiMapv;
    using SlimDX;
    using SlimDX.Direct3D9;
    using System;

    public class Putvi : IDisposable
    {
        public IndexBuffer ib;
        public VertexBuffer vb;

        public Putvi(Putbox p, Device dev)
        {
            DataStream stream;
            DataStream stream2;
            base..ctor();
            this.vb = new VertexBuffer(dev, CustomVertex.Position.Size * p.alv.Count, 0x40, CustomVertex.Position.Format, 1);
            stream = this.vb.Lock(0, 0, 0x2000);
        Label_003E:
            try
            {
                stream.WriteRange<Vector3>(p.alv.ToArray());
                goto Label_005E;
            }
            finally
            {
            Label_0051:
                this.vb.Unlock();
            }
        Label_005E:
            this.ib = new IndexBuffer(dev, 4 * p.ali.Count, 0x40, 1, 0);
            stream2 = this.ib.Lock(0, 0, 0x2000);
        Label_008E:
            try
            {
                stream2.WriteRange<int>(p.ali.ToArray());
                goto Label_00AE;
            }
            finally
            {
            Label_00A1:
                this.ib.Unlock();
            }
        Label_00AE:
            return;
        }

        public void Dispose()
        {
            if (this.vb == null)
            {
                goto Label_0013;
            }
            this.vb.Dispose();
        Label_0013:
            if (this.ib == null)
            {
                goto Label_0026;
            }
            this.ib.Dispose();
        Label_0026:
            return;
        }
    }
}

