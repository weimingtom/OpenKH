using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Kh
{
    /// <summary>
    /// This class helps the loading of TIM2 files.
    /// </summary>
    public sealed class TM2
    {
        /// <summary>
        /// it can be found at the beginning of file
        /// 4 byte, magic code
        /// 2 byte, version
        /// 2 byte, image's count
        /// 8 byte, dummy
        /// </summary>
        struct Header
        {
            public int magicCode;
            public int version;
            public int imageCount;
            public int dummy1;
            public int dummy2;

            public void Read(Stream stream)
            {
                BinaryReader reader = new BinaryReader(stream);
                magicCode = reader.ReadInt32();
                version = reader.ReadInt16();
                imageCount = reader.ReadInt16();
                dummy1 = reader.ReadInt32();
                dummy2 = reader.ReadInt32();
            }
            public void Write(BinaryWriter writer)
            {

            }
            public bool IsValid()
            {
                return magicCode == 0x324D4954;
            }
        }
        /// <summary>
        /// register for image
        /// 14 bit, texture buffer base pointer (address / 256)
        /// 6 bit, texture buffer width (texels / 64)
        /// 6 bit, pixel storage format (0 = 32bit RGBA)
        /// 4 bit, width 2^n
        /// 4 bit, height 2^n
        /// 1 bit, 0 = RGB, 1 = RGBA
        /// 2 bit, texture function (0=modulate, 1=decal, 2=hilight, 3=hilight2)
        /// 14 bit, CLUT buffer base pointer (address / 256)
        /// 4 bit, CLUT storage format
        /// 1 bit, storage mode
        /// 5 bit, offset
        /// 3 bit, load control
        /// </summary>
        struct GsTex0
        {
            long data;

            public int TBP0
            {
                get { return (int)(data >> 0) & 0x3FFF; }
                set { data = (long)(data & ~(0x3FFF << 0)) + (long)(value & 0x3FFF); }
            }
            public int TBW
            {
                get { return (int)(data >> 14) & 0x3F; }
                set { data = (long)(data & ~(0x3F << 14)) + (long)(value & 0x3F); }
            }
            public int PSM
            {
                get { return (int)(data >> 20) & 0x3F; }
                set { data = (long)(data & ~(0x3F << 20)) + (long)(value & 0x3F); }
            }
            public int TW
            {
                get { return (int)(data >> 26) & 0xF; }
                set { data = (long)(data & ~(0xF << 26)) + (long)(value & 0xF); }
            }
            public int TH
            {
                get { return (int)(data >> 30) & 0xF; }
                set { data = (long)(data & ~(0xF << 30)) + (long)(value & 0xF); }
            }
            public int TCC
            {
                get { return (int)(data >> 34) & 1; }
                set { data = (long)(data & ~(1 << 34)) + (long)(value & 1); }
            }
            public int TFX
            {
                get { return (int)(data >> 35) & 3; }
                set { data = (long)(data & ~(3 << 35)) + (long)(value & 3); }
            }
            public int CBP
            {
                get { return (int)(data >> 37) & 0x3FFF; }
                set { data = (long)(data & ~(0x3FFF << 37)) + (long)(value & 0x3FFF); }
            }
            public int CPSM
            {
                get { return (int)(data >> 51) & 0xF; }
                set { data = (long)(data & ~(0xF << 51)) + (long)(value & 0xF); }
            }
            public int CSM
            {
                get { return (int)(data >> 55) & 1; }
                set { data = (long)(data & ~(1 << 55)) + (long)(value & 1); }
            }
            public int CSA
            {
                get { return (int)(data >> 56) & 0x1F; }
                set { data = (long)(data & ~(0x1F << 56)) + (long)(value & 0x1F); }
            }
            public int CLD
            {
                get { return (int)(data >> 61) & 7; }
                set { data = (long)(data & ~(7 << 61)) + (long)(value & 7); }
            }
            public void Read(Stream stream)
            {
                BinaryReader reader = new BinaryReader(stream);
                data = reader.ReadInt64();
            }
        }
        /// <summary>
        /// description of image
        /// 4 byte, total size
        /// 4 byte, palette size
        /// 4 byte, image size
        /// 2 byte, head size
        /// 2 byte, how palettes there are
        /// 2 byte, how palettes are used
        /// 1 byte, palette format
        /// 1 byte, image format
        /// 2 byte, width
        /// 2 byte, height
        /// GsTex0, for two times
        /// 4 byte, gsreg
        /// 4 byte, gspal
        /// </summary>
        struct TM2Pic
        {
            public int size;
            public int palSize;
            public int imgSize;
            public int headSize;
            public int howPal;
            public int howPalUsed;
            public int palFormat;
            public int imgFormat;
            public int width;
            public int height;
            public GsTex0 gstex1;
            public GsTex0 gstex2;
            public int gsreg;
            public int gspal;

            public PixelFormat ImageFormat
            {
                get
                {
                    switch (imgFormat)
                    {
                        case 3:
                            return PixelFormat.Format32bppArgb;
                        case 4:
                            return PixelFormat.Format4bppIndexed;
                        case 5:
                            return PixelFormat.Format8bppIndexed;
                    }
                    return PixelFormat.Undefined;
                }
            }
            public int BitsPerPixel
            {
                get
                {
                    switch (imgFormat)
                    {
                        case 3:
                            return 32;
                        case 4:
                            return 4;
                        case 5:
                            return 8;
                    }
                    return 0;
                }
            }
            public PixelFormat PaletteFormat
            {
                get
                {
                    switch (palFormat)
                    {
                        case 0:
                            return PixelFormat.Undefined;
                        case 1:
                            return PixelFormat.Format16bppArgb1555;
                        case 2:
                            return PixelFormat.Format32bppRgb;
                        case 3:
                            return PixelFormat.Format32bppArgb;
                    }
                    return PixelFormat.Undefined;
                }
            }

            public void Read(Stream stream)
            {
                BinaryReader reader = new BinaryReader(stream);
                size = reader.ReadInt32();
                palSize = reader.ReadInt32();
                imgSize = reader.ReadInt32();
                headSize = reader.ReadInt16();
                howPal = reader.ReadInt16();
                howPalUsed = reader.ReadInt16();
                palFormat = reader.ReadByte();
                imgFormat = reader.ReadByte();
                width = reader.ReadInt16();
                height = reader.ReadInt16();
                gstex1.Read(stream);
                gstex2.Read(stream);
                gsreg = reader.ReadInt32();
                gspal = reader.ReadInt32();
            }
        };

        Image image = null;
        Header header;
        TM2Pic pic;
        int imgPos;
        int palPos;

        Color[] palette;

        public TM2()
        {

        }
        public TM2(string filename)
        {
            Open(filename);
        }
        public TM2(Stream stream)
        {
            Open(stream);
        }

        public void Open(string filename)
        {
            if (File.Exists(filename) == false)
                throw new FileNotFoundException(filename);
            FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Open(fStream);
            fStream.Close();
        }
        public void Open(Stream stream)
        {
            header.Read(stream);
            if (header.IsValid())
            {
                pic.Read(stream);
                imgPos = (int)stream.Position;
                palPos = imgPos = pic.imgSize;

                BinaryReader reader = new BinaryReader(stream);

                // Parse image
                byte[] imgData = new byte[pic.imgSize];
                stream.Read(imgData, 0, pic.imgSize);
                switch (pic.ImageFormat)
                {
                    case PixelFormat.Format32bppArgb:
                        for (int i = 0; i < pic.width * pic.height; i++)
                        {
                            byte tmp = imgData[i * 4 + 0];
                            imgData[i * 4 + 0] = imgData[i * 4 + 2];
                            imgData[i * 4 + 2] = tmp;
                        }
                        break;
                    case PixelFormat.Format4bppIndexed:
                        for (int i = 0; i < pic.width * pic.height / 2; i++)
                        {
                            imgData[i] = (byte)(((imgData[i] & 0x0F) << 4) | (imgData[i] >> 4));
                        }
                        break;
                }

                IntPtr ptr;
                unsafe
                {
                    fixed (byte* pData = imgData)
                    {
                        ptr = new IntPtr(pData);
                    }
                }
                image = new Bitmap(pic.width, pic.height, pic.width * pic.BitsPerPixel / 8, pic.ImageFormat, ptr);

                // Parse palette
                palette = new Color[pic.howPal];
                for (int i = 0; i < pic.howPal; i++)
                {
                    palette[i] = Color.FromArgb(reader.ReadInt32());
                }
                if (image.PixelFormat == PixelFormat.Format4bppIndexed ||
                    image.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    ColorPalette colorPalette = image.Palette;
                    for (int i = 0; i < colorPalette.Entries.Length; i++)
                    {
                        colorPalette.Entries[i] = palette[i];
                    }
                    image.Palette = colorPalette;
                }
            }
            else throw new InvalidDataException("Invalid magic code " + header.magicCode.ToString("X08"));
        }

        public Image Image
        {
            get { return image; }
        }
    }
}
