using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
namespace IMGZ_Editor
{
	public class IMGZ : ImageContainer
	{
		private Stream file;
		private uint magic;
		private int internalCount;
		public IMGZ(Stream file)
		{
			if (!file.CanRead || !file.CanSeek)
			{
				throw new NotSupportedException("Cannot read or seek in stream");
			}
			this.file = file;
			using (BinaryReader binaryReader = new BinaryReader(new NonClosingStream(file)))
			{
				uint num = this.magic = binaryReader.ReadUInt32();
				if (num != 1145523529u)
				{
					if (num != 1514622281u)
					{
						throw new InvalidDataException("Invalid signature");
					}
					file.Position += 8L;
					this.internalCount = binaryReader.ReadInt32();
				}
				else
				{
					this.internalCount = 1;
				}
			}
		}
		private long getIMGDOffset(int index = 0)
		{
			long result;
			using (BinaryReader binaryReader = new BinaryReader(new NonClosingStream(this.file)))
			{
				uint num = this.magic;
				if (num != 1145523529u)
				{
					if (num != 1514622281u)
					{
						throw new NotSupportedException("Invalid signature");
					}
					if (index < 0 || index >= this.internalCount)
					{
						result = -1L;
					}
					else
					{
						this.file.Position = (long)(16 + 8 * index);
						result = (long)((ulong)binaryReader.ReadUInt32());
					}
				}
				else
				{
					result = 0L;
				}
			}
			return result;
		}
		private void parseD(long pos, long len, int index)
		{
			if (pos + len > this.file.Length)
			{
				throw new IndexOutOfRangeException("IMGD goes past file bounds");
			}
			this.file.Position = pos;
			using (BinaryReader binaryReader = new BinaryReader(new NonClosingStream(this.file)))
			{
				if (binaryReader.ReadUInt32() != 1145523529u)
				{
					throw new InvalidDataException("IMGD has bad signature");
				}
				this.file.Position += 4L;
				uint num = binaryReader.ReadUInt32();
				uint num2 = binaryReader.ReadUInt32();
				uint num3 = binaryReader.ReadUInt32();
				uint num4 = binaryReader.ReadUInt32();
				this.file.Position += 4L;
				ushort num5 = binaryReader.ReadUInt16();
				ushort num6 = binaryReader.ReadUInt16();
				this.file.Position += 6L;
				ushort num7 = binaryReader.ReadUInt16();
				this.file.Position += 20L;
				byte b = binaryReader.ReadByte();
				if ((ulong)(num + num2) > (ulong)len)
				{
					throw new IndexOutOfRangeException("IMGD pixel data goes past file bounds");
				}
				if ((ulong)(num3 + num4) > (ulong)len)
				{
					throw new IndexOutOfRangeException("IMGD palette data goes past file bounds");
				}
				PixelFormat pixelFormat;
				switch (num7)
				{
				case 19:
					pixelFormat = PixelFormat.Format8bppIndexed;
					break;
				case 20:
					pixelFormat = PixelFormat.Format4bppIndexed;
					break;
				default:
					throw new NotSupportedException("Unsupported IMGD type");
				}
				Bitmap bitmap = new Bitmap((int)num5, (int)num6, pixelFormat);
				this.file.Position = pos + (long)((ulong)num);
				byte[] array = binaryReader.ReadBytes((int)num2);
				PixelFormat pixelFormat2 = pixelFormat;
				if (pixelFormat2 != PixelFormat.Format4bppIndexed)
				{
					if (pixelFormat2 == PixelFormat.Format8bppIndexed && b == 7)
					{
						array = Reform.Decode8(Reform.Encode32(array, (int)(num5 / 128), (int)(num6 / 64)), (int)(num5 / 128), (int)(num6 / 64));
					}
				}
				else
				{
					if (b == 7)
					{
						array = Reform.Decode4(Reform.Encode32(array, (int)(num5 / 128), (int)(num6 / 128)), (int)(num5 / 128), (int)(num6 / 128));
					}
					else
					{
						Reform.swapHLUT(array);
					}
				}
				BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, (int)num5, (int)num6), ImageLockMode.WriteOnly, pixelFormat);
				try
				{
					Marshal.Copy(array, 0, bitmapData.Scan0, (int)num2);
				}
				finally
				{
					bitmap.UnlockBits(bitmapData);
				}
				this.file.Position = pos + (long)((ulong)num3);
				byte[] array2 = binaryReader.ReadBytes((int)num4);
				ColorPalette palette = bitmap.Palette;
				PixelFormat pixelFormat3 = pixelFormat;
				if (pixelFormat3 != PixelFormat.Format4bppIndexed)
				{
					if (pixelFormat3 == PixelFormat.Format8bppIndexed)
					{
						for (int i = 0; i < 256; i++)
						{
							palette.Entries[Reform.paletteSwap34(i)] = Color.FromArgb(Math.Min((int)(array2[i * 4 + 3] * 2), 255), (int)array2[i * 4], (int)array2[i * 4 + 1], (int)array2[i * 4 + 2]);
						}
					}
				}
				else
				{
					for (int j = 0; j < 16; j++)
					{
						palette.Entries[j] = Color.FromArgb(Math.Min((int)(array2[j * 4 + 3] * 2), 255), (int)array2[j * 4], (int)array2[j * 4 + 1], (int)array2[j * 4 + 2]);
					}
				}
				bitmap.Palette = palette;
				this.bmps.Add(bitmap);
			}
		}
		protected override void Dispose(bool disposing)
		{
			this.file.Dispose();
			base.Dispose(disposing);
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
			this.bmps.Capacity = this.internalCount;
			uint num = this.magic;
			if (num != 1145523529u)
			{
				if (num != 1514622281u)
				{
					throw new NotSupportedException("Invalid signature");
				}
				this.file.Position = 16L;
				using (BinaryReader binaryReader = new BinaryReader(new NonClosingStream(this.file)))
				{
					int i = 0;
					int num2 = this.internalCount;
					while (i < num2)
					{
						long position = this.file.Position + 8L;
						this.parseD((long)((ulong)binaryReader.ReadUInt32()), (long)((ulong)binaryReader.ReadUInt32()), i);
						this.file.Position = position;
						i++;
					}
					return;
				}
			}
			this.parseD(0L, this.file.Length, 0);
		}
		protected override void setBMPInternal(int index, ref Bitmap bmp)
		{
			if (!this.file.CanWrite)
			{
				throw new NotSupportedException("Stream is readonly");
			}
			using (BinaryReader binaryReader = new BinaryReader(new NonClosingStream(this.file)))
			{
				long num = this.file.Position = this.getIMGDOffset(index);
				if (binaryReader.ReadUInt32() != 1145523529u)
				{
					throw new InvalidDataException("IMGD has bad signature");
				}
				this.file.Position += 4L;
				uint num2 = binaryReader.ReadUInt32();
				uint num3 = binaryReader.ReadUInt32();
				uint num4 = binaryReader.ReadUInt32();
				uint num5 = binaryReader.ReadUInt32();
				this.file.Position += 4L;
				ushort num6 = binaryReader.ReadUInt16();
				ushort num7 = binaryReader.ReadUInt16();
				this.file.Position += 6L;
				ushort num8 = binaryReader.ReadUInt16();
				this.file.Position += 20L;
				byte b = binaryReader.ReadByte();
				if (bmp.Width != (int)num6 || bmp.Height != (int)num7)
				{
					throw new NotSupportedException("New image has different dimensions");
				}
				PixelFormat pixelFormat;
				switch (num8)
				{
				case 19:
					pixelFormat = PixelFormat.Format8bppIndexed;
					break;
				case 20:
					pixelFormat = PixelFormat.Format4bppIndexed;
					break;
				default:
					throw new NotSupportedException("Unsupported IMGD type");
				}
				if (bmp.PixelFormat != pixelFormat)
				{
					ImageContainer.requestQuantize(ref bmp, pixelFormat);
				}
				BitmapData bitmapData = bmp.LockBits(Rectangle.FromLTRB(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, pixelFormat);
				byte[] array = new byte[num3];
				try
				{
					Marshal.Copy(bitmapData.Scan0, array, 0, (int)num3);
				}
				finally
				{
					bmp.UnlockBits(bitmapData);
				}
				PixelFormat pixelFormat2 = pixelFormat;
				if (pixelFormat2 != PixelFormat.Format4bppIndexed)
				{
					if (pixelFormat2 == PixelFormat.Format8bppIndexed && b == 7)
					{
						array = Reform.Decode32(Reform.Encode8(array, (int)(num6 / 128), (int)(num7 / 64)), (int)(num6 / 128), (int)(num7 / 64));
					}
				}
				else
				{
					if (b == 7)
					{
						array = Reform.Decode32(Reform.Encode4(array, (int)(num6 / 128), (int)(num7 / 128)), (int)(num6 / 128), (int)(num7 / 128));
					}
					else
					{
						Reform.swapHLUT(array);
					}
				}
				this.file.Position = num + (long)((ulong)num2);
				this.file.Write(array, 0, (int)num3);
				ColorPalette palette = bmp.Palette;
				byte[] array2 = new byte[num5];
				PixelFormat pixelFormat3 = pixelFormat;
				if (pixelFormat3 != PixelFormat.Format4bppIndexed)
				{
					if (pixelFormat3 == PixelFormat.Format8bppIndexed)
					{
						for (int i = 0; i < 256; i++)
						{
							uint num9 = (uint)palette.Entries[Reform.paletteSwap34(i)].ToArgb();
							array2[i * 4 + 3] = (byte)Math.Ceiling((num9 >> 24) / 2.0);
							array2[i * 4] = (byte)(num9 >> 16);
							array2[i * 4 + 1] = (byte)(num9 >> 8);
							array2[i * 4 + 2] = (byte)num9;
						}
					}
				}
				else
				{
					for (int j = 0; j < 16; j++)
					{
						uint num10 = (uint)palette.Entries[j].ToArgb();
						array2[j * 4 + 3] = (byte)Math.Ceiling((num10 >> 24) / 2.0);
						array2[j * 4] = (byte)(num10 >> 16);
						array2[j * 4 + 1] = (byte)(num10 >> 8);
						array2[j * 4 + 2] = (byte)num10;
					}
				}
				this.file.Position = num + (long)((ulong)num4);
				this.file.Write(array2, 0, (int)num5);
			}
		}
	}
}
