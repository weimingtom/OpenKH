using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
namespace khiiMapv
{
	public class ParseSD
	{
		private class Wavi
		{
			public int off;
			public int gakki;
			public int ontei;
			public int sps;
		}
		private class BER
		{
			private BinaryReader br;
			public BER(BinaryReader br)
			{
				this.br = br;
			}
			public int ReadInt32()
			{
				byte[] array = this.br.ReadBytes(4);
				return (int)array[0] << 24 | (int)array[1] << 16 | (int)array[2] << 8 | (int)array[3];
			}
			public short ReadUInt16()
			{
				int num = (int)this.br.ReadByte() << 8;
				return (short)(num | (int)this.br.ReadByte());
			}
		}
		private class SPUConv
		{
			private static int[,] f = new int[,]
			{

				{
					0,
					0
				},

				{
					60,
					0
				},

				{
					115,
					-52
				},

				{
					98,
					-55
				},

				{
					122,
					-60
				}
			};
			public static byte[] ToWave(MemoryStream fs, int nSamplesPerSec)
			{
				int num = 0;
				int num2 = 0;
				List<int> list = new List<int>();
				while (fs.Position + 16L <= fs.Length)
				{
					byte[] array = new byte[16];
					Trace.Assert(16 == fs.Read(array, 0, 16));
					int num3 = (int)array[0];
					int num4 = num3 & 15;
					num3 >>= 4;
					byte arg_3F_0 = array[1];
					for (int i = 0; i < 14; i++)
					{
						int num5 = (int)array[2 + i];
						int num6 = (num5 & 15) << 12;
						if ((num6 & 32768) != 0)
						{
							num6 |= -65536;
						}
						int num7 = num6 >> num4;
						num7 = num7 + (num * ParseSD.SPUConv.f[num3, 0] >> 6) + (num2 * ParseSD.SPUConv.f[num3, 1] >> 6);
						num2 = num;
						num = num7;
						num6 = (num5 & 240) << 8;
						list.Add(num7);
						if ((num6 & 32768) != 0)
						{
							num6 |= -65536;
						}
						num7 = num6 >> num4;
						num7 = num7 + (num * ParseSD.SPUConv.f[num3, 0] >> 6) + (num2 * ParseSD.SPUConv.f[num3, 1] >> 6);
						num2 = num;
						num = num7;
						list.Add(num7);
					}
				}
				MemoryStream memoryStream = new MemoryStream();
				int count = list.Count;
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Encoding.ASCII.GetBytes("RIFF"));
				binaryWriter.Write(50 + 2 * count);
				binaryWriter.Write(Encoding.ASCII.GetBytes("WAVE"));
				binaryWriter.Write(Encoding.ASCII.GetBytes("fmt "));
				binaryWriter.Write(18);
				binaryWriter.Write(1);
				binaryWriter.Write(1);
				binaryWriter.Write(nSamplesPerSec);
				binaryWriter.Write(nSamplesPerSec * 2);
				binaryWriter.Write(2);
				binaryWriter.Write(16);
				binaryWriter.Write(0);
				binaryWriter.Write(Encoding.ASCII.GetBytes("fact"));
				binaryWriter.Write(4);
				binaryWriter.Write(count);
				binaryWriter.Write(Encoding.ASCII.GetBytes("data"));
				binaryWriter.Write(2 * count);
				for (int j = 0; j < count; j++)
				{
					int num8 = Math.Max(-32768, Math.Min(32767, list[j]));
					binaryWriter.Write((ushort)num8);
				}
				return memoryStream.ToArray();
			}
			public static byte[] ToWave2ch(MemoryStream fs, int nSamplesPerSec)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				List<int> list = new List<int>();
				List<int> list2 = new List<int>();
				while (fs.Position + 32L <= fs.Length)
				{
					byte[] array = new byte[16];
					Trace.Assert(16 == fs.Read(array, 0, 16));
					int num5 = (int)array[0];
					int num6 = num5 & 15;
					num5 >>= 4;
					byte arg_4F_0 = array[1];
					for (int i = 0; i < 14; i++)
					{
						int num7 = (int)array[2 + i];
						int num8 = (num7 & 15) << 12;
						if ((num8 & 32768) != 0)
						{
							num8 |= -65536;
						}
						int num9 = num8 >> num6;
						num9 = num9 + (num * ParseSD.SPUConv.f[num5, 0] >> 6) + (num2 * ParseSD.SPUConv.f[num5, 1] >> 6);
						num2 = num;
						num = num9;
						num8 = (num7 & 240) << 8;
						list.Add(num9);
						if ((num8 & 32768) != 0)
						{
							num8 |= -65536;
						}
						num9 = num8 >> num6;
						num9 = num9 + (num * ParseSD.SPUConv.f[num5, 0] >> 6) + (num2 * ParseSD.SPUConv.f[num5, 1] >> 6);
						num2 = num;
						num = num9;
						list.Add(num9);
					}
					Trace.Assert(16 == fs.Read(array, 0, 16));
					int num10 = (int)array[0];
					int num11 = num10 & 15;
					num10 >>= 4;
					byte arg_159_0 = array[1];
					for (int j = 0; j < 14; j++)
					{
						int num12 = (int)array[2 + j];
						int num13 = (num12 & 15) << 12;
						if ((num13 & 32768) != 0)
						{
							num13 |= -65536;
						}
						int num14 = num13 >> num11;
						num14 = num14 + (num3 * ParseSD.SPUConv.f[num10, 0] >> 6) + (num4 * ParseSD.SPUConv.f[num10, 1] >> 6);
						num4 = num3;
						num3 = num14;
						num13 = (num12 & 240) << 8;
						list2.Add(num14);
						if ((num13 & 32768) != 0)
						{
							num13 |= -65536;
						}
						num14 = num13 >> num11;
						num14 = num14 + (num3 * ParseSD.SPUConv.f[num10, 0] >> 6) + (num4 * ParseSD.SPUConv.f[num10, 1] >> 6);
						num4 = num3;
						num3 = num14;
						list2.Add(num14);
					}
				}
				MemoryStream memoryStream = new MemoryStream();
				int count = list.Count;
				BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
				binaryWriter.Write(Encoding.ASCII.GetBytes("RIFF"));
				binaryWriter.Write(50 + 4 * count);
				binaryWriter.Write(Encoding.ASCII.GetBytes("WAVE"));
				binaryWriter.Write(Encoding.ASCII.GetBytes("fmt "));
				binaryWriter.Write(18);
				binaryWriter.Write(1);
				binaryWriter.Write(2);
				binaryWriter.Write(nSamplesPerSec);
				binaryWriter.Write(nSamplesPerSec * 4);
				binaryWriter.Write(4);
				binaryWriter.Write(16);
				binaryWriter.Write(0);
				binaryWriter.Write(Encoding.ASCII.GetBytes("fact"));
				binaryWriter.Write(4);
				binaryWriter.Write(count);
				binaryWriter.Write(Encoding.ASCII.GetBytes("data"));
				binaryWriter.Write(4 * count);
				for (int k = 0; k < count; k++)
				{
					int num15 = Math.Max(-32768, Math.Min(32767, list[k]));
					binaryWriter.Write((ushort)num15);
					int num16 = Math.Max(-32768, Math.Min(32767, list2[k]));
					binaryWriter.Write((ushort)num16);
				}
				return memoryStream.ToArray();
			}
		}
		public static Wavo[] ReadIV(Stream fs)
		{
			BinaryReader binaryReader = new BinaryReader(fs);
			ParseSD.BER bER = new ParseSD.BER(binaryReader);
			fs.Position = 12L;
			int num = binaryReader.ReadInt32();
			KeyValuePair<int, int>[] array = new KeyValuePair<int, int>[num];
			fs.Position = 16L;
			for (int i = 0; i < num; i++)
			{
				int num2 = binaryReader.ReadInt32();
				if (num2 >= 0)
				{
					num2 += 16 + 8 * num;
				}
				int value = binaryReader.ReadInt32();
				array[i] = new KeyValuePair<int, int>(num2, value);
			}
			List<Wavo> list = new List<Wavo>();
			for (int j = 0; j < array.Length; j++)
			{
				int key = array[j].Key;
				if (key >= 0)
				{
					fs.Position = (long)(key + 12);
					int count = bER.ReadInt32() - 32;
					fs.Position = (long)(key + 16);
					int nSamplesPerSec = bER.ReadInt32();
					fs.Position = (long)(key + 64);
					list.Add(new Wavo(j.ToString("00") + ".wav", ParseSD.SPUConv.ToWave(new MemoryStream(binaryReader.ReadBytes(count)), nSamplesPerSec)));
				}
			}
			return list.ToArray();
		}
		public static Wavo[] ReadWD(Stream fs)
		{
			BinaryReader binaryReader = new BinaryReader(fs);
			fs.Position = 8L;
			int num = binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			int[] array = new int[num];
			fs.Position = 32L;
			for (int i = 0; i < num; i++)
			{
				array[i] = binaryReader.ReadInt32();
			}
			int gakki = 0;
			int num3 = 0;
			List<ParseSD.Wavi> list = new List<ParseSD.Wavi>();
			for (int j = 0; j < num2; j++)
			{
				int num4 = 32 + 4 * (num + 3 & -4) + 32 * j;
				int num5 = Array.IndexOf<int>(array, num4);
				if (num5 >= 0)
				{
					gakki = num5;
					num3 = 0;
				}
				fs.Position = (long)(num4 + 16);
				if (binaryReader.ReadInt64() != 0L || binaryReader.ReadInt64() != 0L)
				{
					fs.Position = (long)(num4 + 4);
					ParseSD.Wavi wavi = new ParseSD.Wavi();
					wavi.off = binaryReader.ReadInt32();
					wavi.gakki = gakki;
					wavi.ontei = num3;
					num3++;
					fs.Position = (long)(num4 + 22);
					wavi.sps = (int)binaryReader.ReadUInt16();
					list.Add(wavi);
				}
			}
			List<Wavo> list2 = new List<Wavo>();
			for (int k = 0; k < list.Count; k++)
			{
				ParseSD.Wavi wavi2 = list[k];
				int num6 = 32 + 16 * (num + 3 & -4) + 32 * num2 + wavi2.off;
				fs.Position = (long)num6;
				while (fs.Position < fs.Length)
				{
					byte[] array2 = binaryReader.ReadBytes(16);
					int num7 = 0;
					while (num7 < 16 && array2[num7] == 0)
					{
						num7++;
					}
					if (num7 == 16)
					{
						break;
					}
				}
				int sps = wavi2.sps;
				int count = Convert.ToInt32(fs.Position) - num6 - 32;
				fs.Position = (long)num6;
				list2.Add(new Wavo(wavi2.gakki.ToString("000") + "." + wavi2.ontei.ToString("00") + ".wav", ParseSD.SPUConv.ToWave(new MemoryStream(binaryReader.ReadBytes(count)), sps)));
			}
			return list2.ToArray();
		}
	}
}
