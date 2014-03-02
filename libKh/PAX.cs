using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Diagnostics;
using R = Kh.Pax.R;
using St3 = Kh.Pax.St3;
using St3r = Kh.Pax.St3r;
using STXYC = Kh.Pax.STXYC;

namespace Kh
{
    namespace Pax
    {
        public class R
        {
            public List<Bitmap> pics = new List<Bitmap>();

            [DebuggerDisplay("")]
            public List<St3> als3 = new List<St3>();

            public St3[] _als3 { get { return als3.ToArray(); } }
        }
        public class St3
        {
            public int cnt1, cnt2;

            [DebuggerDisplay("")]
            public List<St3r> al3r = new List<St3r>();

            public St3r[] _al3r { get { return al3r.ToArray(); } }

            public override string ToString()
            {
                return string.Format("{0},{1}", cnt1, cnt2);
            }
        }
        public class St3r
        {
            public int v0, v2, v4;

            [DebuggerDisplay("")]
            public List<STXYC> alv = new List<STXYC>();

            public STXYC[] _alv { get { return alv.ToArray(); } }

            public override string ToString()
            {
                return string.Format("{0:x4},{1:x4},{2:x4}", v0, v2, v4);
            }
        }
        [DebuggerDisplay("({x},{y}),({s},{t}),{clr}")]
        public struct STXYC
        {
            public int x, y, s, t;
            public Color clr;

            public STXYC(int x, int y, int s, int t, Color clr)
            {
                this.x = x;
                this.y = y;
                this.s = s;
                this.t = t;
                this.clr = clr;
            }

            public override string ToString()
            {
                return string.Format("{0},{1},{2},[3},{4}", x, y, s, t, clr);
            }
        }
    }

    public class PicPAX
    {
        public List<R> alr = new List<R>();
    }
    public class ParsePAX
    {
        /// <summary>
        ///     Return bitmap of the PAX file
        ///     <param name="si">MemoryStream of the PAX file/param>
        /// </summary>
        public static PicPAX GetPAX(MemoryStream si)
        {
            si.Position = 0;
            BinaryReader br = new BinaryReader(si);

            if (si.ReadByte() != 0x50) throw new NotSupportedException("!PAX");
            if (si.ReadByte() != 0x41) throw new NotSupportedException("!PAX");
            if (si.ReadByte() != 0x58) throw new NotSupportedException("!PAX");
            if (si.ReadByte() != 0x5F) throw new NotSupportedException("!PAX");

            si.Position = 0xC;
            int v0c = br.ReadInt32(); // off-to-tbl82

            si.Position = v0c;
            int test82 = br.ReadInt32();
            if (test82 != 0x82) throw new NotSupportedException("!82");

            si.Position = v0c + 0xC;
            int cx = br.ReadInt32();

            PicPAX pp = new PicPAX();

            for (int x = 0; x < cx; x++)
            {
                si.Position = v0c + 16 + 32 * x;
                int off96 = (v0c + br.ReadInt32());

                si.Position = off96;
                int test96 = br.ReadInt32();
                if (test96 != 0x96) throw new NotSupportedException("!96");

                int cnt1 = br.ReadInt32();
                List<int> aloff1 = new List<int>(); // dmac tag off?
                for (int t = 0; t < cnt1; t++) aloff1.Add(off96 + br.ReadInt32());

                int cnt2 = br.ReadInt32();
                List<int> aloff2 = new List<int>(); // tex off?
                for (int t = 0; t < cnt2; t++) aloff2.Add(off96 + br.ReadInt32());

                int cnt3 = br.ReadInt32();
                List<int> aloff3 = new List<int>(); // ?
                for (int t = 0; t < cnt3; t++) aloff3.Add(off96 + br.ReadInt32());

                int cnt4 = br.ReadInt32();
                List<int> aloff4 = new List<int>(); // ?
                for (int t = 0; t < cnt4; t++) aloff4.Add(off96 + br.ReadInt32());

                int cnt5 = br.ReadInt32();
                List<int> aloff5 = new List<int>(); // ?
                for (int t = 0; t < cnt5; t++) aloff5.Add(off96 + br.ReadInt32());

                R or = new R();
                pp.alr.Add(or);

                for (int t = 0; t < aloff2.Count; t++)
                {
                    int offt2 = aloff2[t];
                    si.Position = offt2;

                    br.ReadInt32(); // @0
                    br.ReadInt16(); // @4
                    int fmt = br.ReadInt16(); // @6
                    br.ReadInt32(); // @8
                    int tcx = br.ReadInt16(); // @12
                    int tcy = br.ReadInt16(); // @14

                    if (fmt == 0x13)
                    {
                        si.Position = offt2 + 32;
                        byte[] pic = br.ReadBytes(tcx * tcy);
                        byte[] pal = br.ReadBytes(1024);
                        or.pics.Add(BUt.Make8(pic, pal, tcx, tcy, t + 1));
                    }
                }

                for (int t = 0; t < aloff3.Count; t++)
                {
                    int offt3 = aloff3[t];
                    si.Position = offt3 + 0x14;

                    St3 o3 = new St3();
                    or.als3.Add(o3);

                    o3.cnt1 = br.ReadInt16(); // @0x14
                    o3.cnt2 = br.ReadInt16(); // @0x16

                    Debug.Assert(o3.cnt1 == o3.cnt2);

                    for (int s = 0; s < o3.cnt1; s++)
                    {
                        si.Position = offt3 + 0x20 + 8 * s;

                        int off3r = br.ReadUInt16();

                        St3r o3r = new St3r();
                        o3.al3r.Add(o3r);

                        si.Position = offt3 + 0x10 + off3r;

                        o3r.v0 = br.ReadUInt16(); // @0x00
                        o3r.v2 = br.ReadUInt16(); // @0x02
                        o3r.v4 = br.ReadUInt16(); // @0x04

                        si.Position = offt3 + 0x10 + off3r + 0x20;

                        for (int w = 0; w < o3r.v2; w++)
                        {
                            STXYC v0 = new STXYC();
                            STXYC v1 = new STXYC();
                            v0.x = br.ReadInt16(); v0.y = br.ReadInt16();
                            v1.x = br.ReadInt16(); v1.y = br.ReadInt16();
                            v0.s = br.ReadInt16(); v0.t = br.ReadInt16();
                            v1.s = br.ReadInt16(); v1.t = br.ReadInt16();
                            o3r.alv.Add(v0);
                            o3r.alv.Add(v1);
                        }
                        for (int w = 0; w < o3r.v2; w++)
                        {
                            STXYC vv = o3r.alv[w];
                            int vR = br.ReadByte();
                            int vG = br.ReadByte();
                            int vB = br.ReadByte();
                            int vA = br.ReadByte();
                            vv.clr = Color.FromArgb(vA, vR, vG, vB);
                            o3r.alv[w] = vv;
                            br.ReadInt32();
                        }
                    }
                }
            }
            return pp;
        }

        class BUt
        {
            public static Bitmap Make8(byte[] pic, byte[] pal, int cx, int cy, int number)
            {
                Bitmap p = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                BitmapData bd = p.LockBits(new Rectangle(0, 0, p.Width, p.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try
                {
                    Marshal.Copy(pic, 0, bd.Scan0, Math.Min(bd.Stride * bd.Height, pic.Length));
                }
                finally
                {
                    p.UnlockBits(bd);
                }

                ColorPalette cp = p.Palette;
                for (int x = 0; x < 256; x++)
                {
                    int t = x;
                    int toi = Kh.Reform.pals.repl(x);
                    cp.Entries[toi] = Color.FromArgb(
                        Math.Min(255, pal[4 * t + 3] * 2),
                        pal[4 * t + 0],
                        pal[4 * t + 1],
                        pal[4 * t + 2]
                        );
                }
                p.Palette = cp;
                //p.Save("1." + number.ToString() + ".p.8" + ".png", ImageFormat.Png);
                //File.WriteAllBytes("prize.bin", pic);
                //File.WriteAllBytes("prize.pal", pal);
                return p;
            }
        }
    }
}