using BAR_Editor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
namespace IMGZ_Editor
{
    public class MDLX : ImageContainer
    {
        private class parsedImgData
        {
            public int barFile;
            public long baseP;
            public long palBOffs;
            public long palOffs;
            public byte palCs;
            public long imgBOffs;
            public ushort width;
            public ushort height;
            public uint type;
        }
        private BAR BAR;
        private Stream file;
        private bool compatFixes;
        private List<MDLX.parsedImgData> imgDatas = new List<MDLX.parsedImgData>();
        private static byte[] getData(BinaryReader br, long baseP)
        {
            long num;
            long num2;
            return MDLX.getDataRF(br, baseP, out num, out num2);
        }
        private static byte[] getDataR(BinaryReader br, long baseP, out long ramOffset)
        {
            long num;
            return MDLX.getDataRF(br, baseP, out ramOffset, out num);
        }
        private static byte[] getDataF(BinaryReader br, long baseP, out long fileOffset)
        {
            long num;
            return MDLX.getDataRF(br, baseP, out num, out fileOffset);
        }
        private static byte[] getDataRF(BinaryReader br, long baseP, out long ramOffset, out long fileOffset)
        {
            ulong num = br.ReadUInt64();
            ramOffset = (long)((ulong)(256u * ((uint)(num >> 32) & 16383u)));
            int num2 = (int)(num >> 48) & 63;
            uint num3 = (uint)(num >> 56) & 63u;
            br.BaseStream.Position += 8L;
            Trace.Assert((num & 16383uL) == 0uL);
            Trace.Assert((num >> 16 & 63uL) == 0uL);
            Trace.Assert((num >> 24 & 63uL) == 0uL);
            num = br.ReadUInt64();
            br.BaseStream.Position += 8L;
            Trace.Assert((num & 2047uL) == 0uL);
            Trace.Assert((num >> 16 & 2047uL) == 0uL);
            Trace.Assert((num >> 32 & 2047uL) == 0uL);
            Trace.Assert((num >> 48 & 2047uL) == 0uL);
            Trace.Assert((num >> 59 & 3uL) == 0uL);
            br.BaseStream.Position += 16L;
            Trace.Assert((br.ReadUInt64() & 2uL) == 0uL);
            br.BaseStream.Position += 8L;
            int num4 = (int)(br.ReadUInt16() & 32767) << 4;
            br.BaseStream.Position += 18L;
            br.BaseStream.Position = (fileOffset = baseP + (long)br.ReadInt32());
            byte[] array = new byte[num4];
            br.BaseStream.Read(array, 0, num4);
            num4 /= 8192;
            uint num5 = num3;
            if (num5 != 0u)
            {
                switch (num5)
                {
                    case 19u:
                        num2 /= 2;
                        if (num2 > 0)
                        {
                            array = Reform.Encode8(array, num2, num4 / num2);
                        }
                        break;
                    case 20u:
                        num2 /= 2;
                        if (num2 > 0)
                        {
                            array = Reform.Encode4(array, num2, num4 / num2);
                        }
                        break;
                    default:
                        throw new NotSupportedException("Unknown type: " + num3);
                }
            }
            else
            {
                if (num2 > 0)
                {
                    array = Reform.Encode32(array, num2, num4 / num2);
                }
            }
            return array;
        }
        private static void setData(BinaryReader br, long baseP, byte[] data)
        {
            ulong num = br.ReadUInt64();
            int num2 = (int)(num >> 48) & 63;
            uint num3 = (uint)(num >> 56) & 63u;
            br.BaseStream.Position += 56L;
            int num4 = (int)(br.ReadUInt16() & 32767) << 4;
            br.BaseStream.Position += 18L;
            br.BaseStream.Position = baseP + (long)br.ReadInt32();
            int num5 = num4 / 8192;
            uint num6 = num3;
            if (num6 != 0u)
            {
                switch (num6)
                {
                    case 19u:
                        num2 /= 2;
                        if (num2 > 0)
                        {
                            data = Reform.Decode8(data, num2, num5 / num2);
                        }
                        break;
                    case 20u:
                        num2 /= 2;
                        if (num2 > 0)
                        {
                            data = Reform.Decode4(data, num2, num5 / num2);
                        }
                        break;
                    default:
                        throw new NotSupportedException("Unknown type: " + num3);
                }
            }
            else
            {
                if (num2 > 0)
                {
                    data = Reform.Decode32(data, num2, num5 / num2);
                }
            }
            br.BaseStream.Write(data, 0, Math.Min(num4, (int)Math.Min((long)data.Length, br.BaseStream.Length - br.BaseStream.Position)));
        }
        public MDLX(Stream file)
        {
            this.BAR = new BAR(new NonClosingStream(file));
            if (file.CanWrite)
            {
                this.file = file;
            }
            else
            {
                file.Dispose();
                this.file = null;
            }
            bool flag = false;
            foreach (BAR.BARFile current in this.BAR.fileList)
            {
                if (current.type == 7u)
                {
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                throw new FileNotFoundException("No texture files inside that BAR.");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (this.file != null)
            {
                this.file.Dispose();
                this.file = null;
            }
            base.Dispose(disposing);
        }
        private Bitmap getImage(BinaryReader br, ref byte[] imgData, byte[] palData, long palOffs, MDLX.parsedImgData info)
        {
            Trace.Assert(br.ReadUInt64() == 63uL);
            br.BaseStream.Position += 8L;
            Trace.Assert(br.ReadUInt64() == 52uL);
            br.BaseStream.Position += 8L;
            Trace.Assert(br.ReadUInt64() == 54uL);
            ulong num = br.ReadUInt64();
            Trace.Assert(br.ReadUInt64() == 22uL);
            Trace.Assert((num >> 20 & 63uL) == 19uL);
            Trace.Assert((num >> 51 & 15uL) == 0uL);
            Trace.Assert((num >> 55 & 1uL) == 0uL);
            Trace.Assert((num >> 56 & 31uL) == 0uL);
            Trace.Assert((num >> 61 & 7uL) == 4uL);
            br.BaseStream.Position += 8L;
            Trace.Assert(br.ReadUInt64() == 20uL);
            num = br.ReadUInt64();
            Trace.Assert(br.ReadUInt64() == 6uL);
            Trace.Assert((num >> 34 & 1uL) == 1uL);
            Trace.Assert((num >> 51 & 15uL) == 0uL);
            Trace.Assert((num >> 55 & 1uL) == 0uL);
            Trace.Assert((num >> 61 & 7uL) == 0uL);
            br.BaseStream.Position += 8L;
            Trace.Assert(br.ReadUInt64() == 8uL);
            info.type = ((uint)(num >> 20) & 63u);
            if (info.type != 19u && info.type != 20u)
            {
                throw new NotSupportedException("Unknown t0PSM: " + info.type);
            }
            info.palOffs = (long)((ulong)(256u * ((uint)(num >> 37) & 16383u)) - (ulong)palOffs);
            info.width = (ushort)(1 << ((int)(num >> 26) & 15));
            info.height = (ushort)(1 << ((int)(num >> 30) & 15));
            info.palCs = (byte)((uint)(num >> 56) & 31u);
            byte[] array = new byte[1024];
            if (info.palOffs < 0L)
            {
                throw new NotSupportedException("Image palette located before block address.");
            }
            if (info.palOffs + (long)array.Length > (long)palData.Length)
            {
                throw new NotSupportedException("Image palette located after block address.");
            }
            Array.Copy(palData, info.palOffs, array, 0L, (long)array.Length);
            int num2 = (int)(info.width * info.height);
            if (info.type == 20u)
            {
                num2 /= 2;
            }
            if (imgData.Length < num2)
            {
                Array.Resize<byte>(ref imgData, num2);
            }
            return TexUt2.Decode(imgData, array, info.type, info.width, info.height, info.palCs);
        }
        private void ParseTIM(BinaryReader br, long baseP, int barPos)
        {
            br.BaseStream.Position = baseP + 8L + 4L;
            int num = br.ReadInt32();
            int num2 = br.ReadInt32();
            int num3 = br.ReadInt32();
            int num4 = br.ReadInt32();
            br.BaseStream.Position = baseP + (long)num2;
            int[] array = new int[num];
            for (int i = 0; i < num; i++)
            {
                array[i] = (int)br.ReadByte();
            }
            long palBOffs = br.BaseStream.Position = baseP + (long)num3 + 32L;
            long palOffs;
            byte[] dataR = MDLX.getDataR(br, baseP, out palOffs);
            for (int j = 0; j < num; j++)
            {
                MDLX.parsedImgData parsedImgData = new MDLX.parsedImgData
                {
                    barFile = barPos,
                    baseP = baseP,
                    palBOffs = palBOffs
                };
                br.BaseStream.Position = (parsedImgData.imgBOffs = baseP + (long)num3 + 32L + 144L + (long)(144 * array[j]));
                byte[] data = MDLX.getData(br, baseP);
                br.BaseStream.Position = baseP + (long)num4 + 32L + (long)(160 * j) + 8L;
                Bitmap image = this.getImage(br, ref data, dataR, palOffs, parsedImgData);
                this.bmps.Add(image);
                this.imgDatas.Add(parsedImgData);
            }
        }
        private void parseTexture(BAR.BARFile file, int barPos)
        {
            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(file.data, false)))
            {
                int num = binaryReader.ReadInt32();
                if (num == 0)
                {
                    this.ParseTIM(binaryReader, 0L, barPos);
                }
                else
                {
                    if (num != -1)
                    {
                        throw new NotSupportedException("Unknown v0: " + num);
                    }
                    num = binaryReader.ReadInt32();
                    for (int i = 0; i < num; i++)
                    {
                        long position = binaryReader.BaseStream.Position + 4L;
                        this.ParseTIM(binaryReader, (long)binaryReader.ReadInt32(), barPos);
                        binaryReader.BaseStream.Position = position;
                    }
                }
            }
        }
        public override void parse()
        {
            if (this.bmps.Count != 0)
            {
                foreach (Bitmap current in this.bmps)
                {
                    current.Dispose();
                }
                this.bmps.Clear();
            }
            for (int i = 0; i < this.BAR.fileList.Count; i++)
            {
                BAR.BARFile bARFile = this.BAR.fileList[i];
                if (bARFile.type == 7u)
                {
                    this.parseTexture(bARFile, i);
                }
            }
        }
        protected override void setBMPInternal(int index, ref Bitmap bmp)
        {
            if (this.file == null || !this.file.CanWrite)
            {
                throw new NotSupportedException("Stream is readonly");
            }
            if (this.compatFixes)
            {
                throw new NotSupportedException("Input data had various compatibility fixes applied to be able to load! Cannot save with these.");
            }
            MDLX.parsedImgData parsedImgData = this.imgDatas[index];
            if (bmp.Width != (int)parsedImgData.width || bmp.Height != (int)parsedImgData.height)
            {
                throw new NotSupportedException("New image has different dimensions");
            }
            PixelFormat pixelFormat;
            switch (parsedImgData.type)
            {
                case 19u:
                    pixelFormat = PixelFormat.Format8bppIndexed;
                    break;
                case 20u:
                    pixelFormat = PixelFormat.Format4bppIndexed;
                    break;
                default:
                    throw new NotSupportedException("Unsupported type");
            }
            if (bmp.PixelFormat != pixelFormat)
            {
                ImageContainer.requestQuantize(ref bmp, pixelFormat);
            }
            byte[] data = this.BAR.fileList[parsedImgData.barFile].data;
            using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data, true)))
            {
                binaryReader.BaseStream.Position = parsedImgData.palBOffs;
                byte[] data2 = MDLX.getData(binaryReader, parsedImgData.baseP);
                byte[] array = new byte[1024];
                Array.Copy(data2, parsedImgData.palOffs, array, 0L, (long)array.Length);
                byte[] data3 = TexUt2.Encode(bmp, ref array, parsedImgData.palCs);
                Array.Copy(array, 0L, data2, parsedImgData.palOffs, (long)array.Length);
                binaryReader.BaseStream.Position = parsedImgData.palBOffs;
                MDLX.setData(binaryReader, parsedImgData.baseP, data2);
                binaryReader.BaseStream.Position = parsedImgData.imgBOffs;
                MDLX.setData(binaryReader, parsedImgData.baseP, data3);
            }
            this.BAR.fileList[parsedImgData.barFile].data = data;
            this.file.Position = 0L;
            this.BAR.save(new NonClosingStream(this.file));
        }
    }
}
