using khiiMapv.CollTest;
using khiiMapv.Parse02;
using khiiMapv.Pax;
using khiiMapv.Properties;
using khkh_xldM;
using khkh_xldMii.Mc;
using khkh_xldMii.Mx;
using ParseAI;
using Readmset;
using SlimDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using vcBinTex4;
namespace khiiMapv
{
	public class RDForm : Form
	{
		private class Texctx
		{
			public byte[] gs = new byte[4194304];
			public int t0PSM;
			public int offBin;
			public void Do1(Stream si)
			{
				BinaryReader binaryReader = new BinaryReader(si);
				ulong num = binaryReader.ReadUInt64();
				binaryReader.ReadUInt64();
				int num2 = (int)num & 16383;
				int num3 = (int)(num >> 16) & 63;
				int num4 = (int)(num >> 24) & 63;
				int num5 = (int)(num >> 32) & 16383;
				int num6 = (int)(num >> 48) & 63;
				int num7 = (int)(num >> 56) & 63;
				Trace.Assert(num2 == 0);
				Trace.Assert(num3 == 0);
				Trace.Assert(num4 == 0);
				Trace.Assert(num7 == 0 || num7 == 19 || num7 == 20);
				ulong num8 = binaryReader.ReadUInt64();
				binaryReader.ReadUInt64();
				int num9 = (int)num8 & 2047;
				int num10 = (int)(num8 >> 16) & 2047;
				int num11 = (int)(num8 >> 32) & 2047;
				int num12 = (int)(num8 >> 48) & 2047;
				int num13 = (int)(num8 >> 59) & 3;
				Trace.Assert(num9 == 0);
				Trace.Assert(num10 == 0);
				Trace.Assert(num11 == 0);
				Trace.Assert(num12 == 0);
				Trace.Assert(num13 == 0);
				ulong num14 = binaryReader.ReadUInt64();
				binaryReader.ReadUInt64();
				ulong num15 = binaryReader.ReadUInt64();
				binaryReader.ReadUInt64();
				int num16 = (int)num15 & 2;
				Trace.Assert(num16 == 0);
				int num17 = (int)binaryReader.ReadUInt16();
				Trace.Assert(8 != (num17 & 32768));
				si.Position += 18L;
				this.offBin = binaryReader.ReadInt32();
				int num18 = (num17 & 32767) << 4;
				int bh = num18 / 8192 / num6;
				byte[] array = new byte[Math.Max(8192, num18)];
				si.Position = (long)this.offBin;
				si.Read(array, 0, num18);
				byte[] sourceArray;
				if (num7 == 0)
				{
					sourceArray = Reform32.Encode32(array, num6, bh);
				}
				else
				{
					if (num7 == 19)
					{
						sourceArray = Reform8.Encode8(array, num6 / 2, num18 / 8192 / (num6 / 2));
					}
					else
					{
						if (num7 != 20)
						{
							throw new NotSupportedException("DPSM = " + num7 + "?");
						}
						sourceArray = Reform4.Encode4(array, num6 / 2, num18 / 8192 / Math.Max(1, num6 / 2));
					}
				}
				Array.Copy(sourceArray, 0, this.gs, 256 * num5, num18);
			}
			public STim Do2(Stream si)
			{
				BinaryReader binaryReader = new BinaryReader(si);
				binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 63uL);
				binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 52uL);
				binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 54uL);
				ulong num = binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 22uL);
				int num2 = (int)(num >> 20) & 63;
				int num3 = (int)(num >> 51) & 15;
				int num4 = (int)(num >> 55) & 1;
				int num5 = (int)(num >> 56) & 31;
				int num6 = (int)(num >> 61) & 7;
				Trace.Assert(num2 == 19);
				Trace.Assert(num3 == 0);
				Trace.Assert(num4 == 0);
				Trace.Assert(num5 == 0);
				Trace.Assert(num6 == 4);
				ulong num7 = binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 20uL);
				ulong num8 = binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 6uL);
				int num9 = (int)num8 & 16383;
				int tbw = (int)(num8 >> 14) & 63;
				this.t0PSM = ((int)(num8 >> 20) & 63);
				int num10 = (int)(num8 >> 26) & 15;
				int num11 = (int)(num8 >> 30) & 15;
				int num12 = (int)(num8 >> 34) & 1;
				int tfx = (int)(num8 >> 35) & 3;
				int num13 = (int)(num8 >> 37) & 16383;
				int num14 = (int)(num8 >> 51) & 15;
				int num15 = (int)(num8 >> 55) & 1;
				int csa = (int)(num8 >> 56) & 31;
				int num16 = (int)(num8 >> 61) & 7;
				Trace.Assert(this.t0PSM == 19 || this.t0PSM == 20);
				Trace.Assert(num12 == 1);
				Trace.Assert(num14 == 0);
				Trace.Assert(num15 == 0);
				Trace.Assert(num16 == 0);
				ulong num17 = binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 8uL);
				int wms = (int)num17 & 3;
				int wmt = (int)(num17 >> 2) & 3;
				int minu = (int)(num17 >> 4) & 1023;
				int maxu = (int)(num17 >> 14) & 1023;
				int minv = (int)(num17 >> 24) & 1023;
				int maxv = (int)(num17 >> 34) & 1023;
				int val = (1 << num10) * (1 << num11);
				byte[] array = new byte[Math.Max(8192, val)];
				Array.Copy(this.gs, 256 * num9, array, 0, Math.Min(this.gs.Length - 256 * num9, Math.Min(array.Length, val)));
				byte[] array2 = new byte[8192];
				Array.Copy(this.gs, 256 * num13, array2, 0, array2.Length);
				STim sTim = null;
				if (this.t0PSM == 19)
				{
					sTim = TexUt2.Decode8(array, array2, tbw, 1 << num10, 1 << num11);
				}
				if (this.t0PSM == 20)
				{
					sTim = TexUt2.Decode4Ps(array, array2, tbw, 1 << num10, 1 << num11, csa);
				}
				if (sTim != null)
				{
					sTim.tfx = (TFX)tfx;
					sTim.tcc = (TCC)num12;
					sTim.wms = (WM)wms;
					sTim.wmt = (WM)wmt;
					sTim.minu = minu;
					sTim.maxu = maxu;
					sTim.minv = minv;
					sTim.maxv = maxv;
				}
				return sTim;
			}
		}
		private class Hexi
		{
			public int off;
			public int len;
			public RDForm.MI mi;
			public Hexi(int off)
			{
				this.off = off;
				this.len = 0;
			}
			public Hexi(int off, int len)
			{
				this.off = off;
				this.len = len;
			}
			public Hexi(int off, RDForm.MI mi)
			{
				this.off = off;
				this.mi = mi;
			}
		}
		private class Texi : RDForm.Hexi
		{
			public STim st;
			public Texi(int off, STim st) : base(off)
			{
				this.st = st;
			}
			public Texi(int off, RDForm.MI mi, STim st) : base(off, mi)
			{
				this.st = st;
			}
		}
		private class Vifi : RDForm.Hexi
		{
			public byte[] vifpkt;
			public Vifi(int off, byte[] vifpkt) : base(off)
			{
				this.vifpkt = vifpkt;
			}
			public Vifi(int off, RDForm.MI mi, byte[] vifpkt) : base(off, mi)
			{
				this.vifpkt = vifpkt;
			}
		}
		private class Btni : RDForm.Hexi
		{
			public EventHandler onClicked;
			public Btni(int off, EventHandler onClicked) : base(off)
			{
				this.onClicked = onClicked;
			}
		}
		private class IMGDi : RDForm.Hexi
		{
			public Bitmap p;
			public IMGDi(int off, Bitmap p) : base(off)
			{
				this.p = p;
			}
		}
		private class WAVi : RDForm.Hexi
		{
			public Wavo w;
			public WAVi(int off, Wavo w) : base(off)
			{
				this.off = off;
				this.w = w;
			}
		}
		private class Stri : RDForm.Hexi
		{
			public string s;
			public Stri(int off, string s) : base(off)
			{
				this.s = s;
			}
		}
		private class Strif : RDForm.Hexi
		{
			public string s;
			public Strif(int off, string s) : base(off)
			{
				this.s = s;
			}
		}
		private class MI
		{
			public SortedDictionary<string, int> col2off = new SortedDictionary<string, int>();
			public void Add(string col, int off)
			{
				this.col2off[col] = off;
			}
		}
		private class WUt
		{
			public static string Usebar(string fpld, int kid, string nid)
			{
				Directory.CreateDirectory("tmp");
				string fullPath = Path.GetFullPath("tmp\\" + Path.GetFileName(fpld) + ".bar");
				using (FileStream fileStream = File.Create(fullPath))
				{
					BinaryWriter binaryWriter = new BinaryWriter(fileStream, Encoding.ASCII);
					binaryWriter.Write(Encoding.ASCII.GetBytes("BAR\u0001"));
					binaryWriter.Write(1);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(kid);
					binaryWriter.Write(Encoding.ASCII.GetBytes(nid.PadRight(4, '\0').Substring(0, 4)));
					binaryWriter.Write(32);
					using (FileStream fileStream2 = File.OpenRead(fpld))
					{
						binaryWriter.Write((int)fileStream2.Length);
						binaryWriter.Write(new BinaryReader(fileStream2).ReadBytes((int)fileStream2.Length));
					}
				}
				return fullPath;
			}
		}
		private delegate void WavePlayerDelegate(RDForm.WAVi wi);
		private class Parseivif
		{
			private class RangeUtil
			{
				public uint min = 4294967295u;
				public uint max;
				public uint pass(uint val)
				{
					this.min = Math.Min(val, this.min);
					this.max = Math.Max(val, this.max);
					return val;
				}
				public ushort pass(ushort val)
				{
					this.min = Math.Min((uint)val, this.min);
					this.max = Math.Max((uint)val, this.max);
					return val;
				}
				public byte pass(byte val)
				{
					this.min = Math.Min((uint)val, this.min);
					this.max = Math.Max((uint)val, this.max);
					return val;
				}
			}
			public static string Parse(byte[] bin04)
			{
				MemoryStream memoryStream = new MemoryStream(bin04, false);
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				StringBuilder stringBuilder = new StringBuilder();
				while (memoryStream.Position < memoryStream.Length)
				{
					long position = memoryStream.Position;
					uint num = binaryReader.ReadUInt32();
					int num2 = (int)(num >> 24 & 127u);
					stringBuilder.AppendFormat("{0:X4} {1} ", position, ((num & 2147483648u) != 0u) ? 'I' : ' ');
					int num3 = num2;
					if (num3 <= 32)
					{
						switch (num3)
						{
						case 0:
							stringBuilder.AppendFormat("nop\n", new object[0]);
							continue;
						case 1:
						{
							int num4 = (int)(num & 255u);
							int num5 = (int)(num >> 8 & 255u);
							stringBuilder.AppendFormat("stcycl cl {0:x2} wl {1:x2}\n", num4, num5);
							continue;
						}
						case 2:
							stringBuilder.AppendFormat("offset\n", new object[0]);
							continue;
						case 3:
							stringBuilder.AppendFormat("base\n", new object[0]);
							continue;
						case 4:
							stringBuilder.AppendFormat("itop\n", new object[0]);
							continue;
						case 5:
							stringBuilder.AppendFormat("stmod\n", new object[0]);
							continue;
						case 6:
							stringBuilder.AppendFormat("mskpath3\n", new object[0]);
							continue;
						case 7:
							stringBuilder.AppendFormat("mark\n", new object[0]);
							continue;
						case 8:
						case 9:
						case 10:
						case 11:
						case 12:
						case 13:
						case 14:
						case 15:
						case 18:
						case 22:
							break;
						case 16:
							stringBuilder.AppendFormat("flushe\n", new object[0]);
							continue;
						case 17:
							stringBuilder.AppendFormat("flush\n", new object[0]);
							continue;
						case 19:
							stringBuilder.AppendFormat("flusha\n", new object[0]);
							continue;
						case 20:
							stringBuilder.AppendFormat("mscal\n", new object[0]);
							continue;
						case 21:
							stringBuilder.AppendFormat("mscalf\n", new object[0]);
							continue;
						case 23:
							stringBuilder.AppendFormat("mscnt\n", new object[0]);
							continue;
						default:
							if (num3 == 32)
							{
								uint num6 = binaryReader.ReadUInt32();
								string arg = "";
								for (int i = 0; i < 16; i++)
								{
									if ((i & 3) == 0)
									{
										arg += ' ';
									}
									arg = arg + (int)(num6 >> 2 * i & 3u) + " ";
								}
								stringBuilder.AppendFormat("stmask {0}\n", arg);
								continue;
							}
							break;
						}
					}
					else
					{
						switch (num3)
						{
						case 48:
						{
							uint num7 = binaryReader.ReadUInt32();
							uint num8 = binaryReader.ReadUInt32();
							uint num9 = binaryReader.ReadUInt32();
							uint num10 = binaryReader.ReadUInt32();
							stringBuilder.AppendFormat("strow {0:x8} {1:x8} {2:x8} {3:x8}\n", new object[]
							{
								num7,
								num8,
								num9,
								num10
							});
							continue;
						}
						case 49:
						{
							uint num11 = binaryReader.ReadUInt32();
							uint num12 = binaryReader.ReadUInt32();
							uint num13 = binaryReader.ReadUInt32();
							uint num14 = binaryReader.ReadUInt32();
							stringBuilder.AppendFormat("stcol {0:x8} {1:x8} {2:x8} {3:x8}\n", new object[]
							{
								num11,
								num12,
								num13,
								num14
							});
							continue;
						}
						default:
							if (num3 == 74)
							{
								stringBuilder.AppendFormat("mpg\n", new object[0]);
								continue;
							}
							switch (num3)
							{
							case 80:
							{
								stringBuilder.AppendFormat("direct\n", new object[0]);
								int num15 = (int)(num & 65535u);
								memoryStream.Position = (memoryStream.Position + 15L & -16L);
								memoryStream.Position += (long)(16 * num15);
								continue;
							}
							case 81:
							{
								stringBuilder.AppendFormat("directhl\n", new object[0]);
								int num16 = (int)(num & 65535u);
								memoryStream.Position = (memoryStream.Position + 15L & -16L);
								memoryStream.Position += (long)(16 * num16);
								continue;
							}
							}
							break;
						}
					}
					if (96 == (num2 & 96))
					{
						RDForm.Parseivif.RangeUtil rangeUtil = new RDForm.Parseivif.RangeUtil();
						int num17 = num2 >> 4 & 1;
						int num18 = num2 >> 2 & 3;
						int num19 = num2 & 3;
						int num20 = (int)(num >> 16 & 255u);
						int num21 = (int)(num >> 15 & 1u);
						int num22 = (int)(num >> 14 & 1u);
						int num23 = (int)(num & 1023u);
						int num24 = 0;
						int num25 = 1;
						string text = "";
						switch (num19)
						{
						case 0:
							num24 = 4;
							text = "32";
							break;
						case 1:
							num24 = 2;
							text = "16";
							break;
						case 2:
							num24 = 1;
							text = "8";
							break;
						case 3:
							num24 = 2;
							text = "5+5+5+1";
							break;
						}
						string text2 = "";
						switch (num18)
						{
						case 0:
							num25 = 1;
							text2 = "S";
							break;
						case 1:
							num25 = 2;
							text2 = "V2";
							break;
						case 2:
							num25 = 3;
							text2 = "V3";
							break;
						case 3:
							num25 = 4;
							text2 = "V4";
							break;
						}
						int num26 = num24 * num25 * num20 + 3 & -4;
						long position2 = memoryStream.Position + (long)num26;
						stringBuilder.AppendFormat("unpack {0}-{1} c {2} a {3:X3} usn {4} flg {5} m {6}\n", new object[]
						{
							text2,
							text,
							num20,
							num23,
							num22,
							num21,
							num17
						});
						if (num19 == 0 && (num18 == 2 || num18 == 3))
						{
							for (int j = 0; j < num20; j++)
							{
								stringBuilder.Append("    ");
								for (int k = 0; k < num25; k++)
								{
									stringBuilder.AppendFormat("{0:x8} ", rangeUtil.pass(binaryReader.ReadUInt32()));
								}
								stringBuilder.Append("\n");
							}
						}
						else
						{
							if (num19 == 1)
							{
								for (int l = 0; l < num20; l++)
								{
									stringBuilder.Append("    ");
									for (int m = 0; m < num25; m++)
									{
										stringBuilder.AppendFormat("{0:x4} ", rangeUtil.pass(binaryReader.ReadUInt16()));
									}
									stringBuilder.Append("\n");
								}
							}
							else
							{
								if (num19 == 2)
								{
									for (int n = 0; n < num20; n++)
									{
										stringBuilder.Append("    ");
										for (int num27 = 0; num27 < num25; num27++)
										{
											stringBuilder.AppendFormat("{0:x2} ", rangeUtil.pass(binaryReader.ReadByte()));
										}
										stringBuilder.Append("\n");
									}
								}
							}
						}
						memoryStream.Position = position2;
						stringBuilder.Append("    ");
						stringBuilder.AppendFormat("min({0}), max({1})\n", rangeUtil.min, rangeUtil.max);
					}
					else
					{
						stringBuilder.AppendFormat("{0:X2}\n", num2);
					}
				}
				stringBuilder.Replace("\n", "\r\n");
				return stringBuilder.ToString();
			}
		}
		private class PalC2s
		{
			public static string Guess(PicIMGD p)
			{
				return RDForm.PalC2s.Guess(p.pic);
			}
			public static string Guess(Bitmap p)
			{
				PixelFormat pixelFormat = p.PixelFormat;
				if (pixelFormat == PixelFormat.Format4bppIndexed)
				{
					return "4";
				}
				if (pixelFormat == PixelFormat.Format8bppIndexed)
				{
					return "8";
				}
				return "?";
			}
		}
		private class UniName
		{
			private SortedDictionary<string, string> dictUsed = new SortedDictionary<string, string>();
			public string Get(string fn)
			{
				for (int i = 0; i < 100; i++)
				{
					string text = Path.GetFileNameWithoutExtension(fn) + ((i != 0) ? ("~" + (1 + i)) : "") + Path.GetExtension(fn);
					if (!this.dictUsed.ContainsKey(text))
					{
						this.dictUsed[text] = null;
						return text;
					}
				}
				return fn;
			}
		}
		private const int BASE = 144;
		private string fpread;
		private byte[] rdbin = new byte[0];
		private List<DC> aldc = new List<DC>();
		private CollReader coll = new CollReader();
		private SortedDictionary<int, string> dictObjName = new SortedDictionary<int, string>();
		private ListDictionary errcol = new ListDictionary();
		private string fpld;
		private RDForm.WavePlayerDelegate WavePlayer;
		private Visf visf;
		private IContainer components;
		private HexVwer hv1;
		private TreeView treeView1;
		private LinkLabel linkLabel1;
		private Panel p1;
		private ListView lvMI;
		private Button button1;
		private Label label1;
		private Label label2;
		private Button bserr;
		private ImageList IL;
		private Label label3;
		private Label labelPixi;
		private Label labelHelpMore;
		private Button bExportBin;
		private Button bExportAll;
		private LinkLabel bBatExp;
		private string OpenedPath
		{
			get
			{
				return this.linkLabel1.Text;
			}
		}
		public RDForm()
		{
			this.InitializeComponent();
		}
		public RDForm(string fpread)
		{
			this.fpread = fpread;
			this.InitializeComponent();
		}
		private void RDForm_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}
		private void LoadMap(string fp)
		{
			this.linkLabel1.Text = fp;
			this.rdbin = File.ReadAllBytes(fp);
			this.hv1.SetBin(this.rdbin);
			this.treeView1.Nodes.Clear();
			this.errcol.Clear();
			TreeNode treeNode = this.treeView1.Nodes.Add(Path.GetExtension(fp));
			treeNode.Tag = new RDForm.Hexi(0);
			DC dC = new DC();
			this.aldc.Clear();
			int num = 0;
			ReadBar.Barent[] array = ReadBar.Explode(new MemoryStream(this.rdbin, false));
			for (int i = 0; i < array.Length; i++)
			{
				ReadBar.Barent barent = array[i];
				TreeNode treeNode2 = treeNode.Nodes.Add(string.Format("{0} ({1:x2})", barent.id, barent.k & 255));
				treeNode2.Tag = new RDForm.Hexi(barent.off, barent.len);
				if (barent.k < num)
				{
					if ((dC.o4Map != null || dC.o4Mdlx != null) && dC.o7 != null)
					{
						this.aldc.Add(dC);
					}
					dC = new DC();
				}
				num = barent.k;
				try
				{
					int k = barent.k;
					switch (k)
					{
					case 2:
						this.Parse02(treeNode2, barent.off, barent);
						break;
					case 3:
						this.Parse03(treeNode2, barent.off, barent);
						break;
					case 4:
						this.Parse4(treeNode2, barent.off, barent, dC);
						dC.name = barent.id;
						break;
					case 5:
					case 8:
					case 9:
					case 13:
					case 14:
					case 16:
					case 17:
					case 20:
					case 21:
					case 22:
					case 23:
						break;
					case 6:
						this.coll = this.Parse6(treeNode2, barent.off, barent);
						break;
					case 7:
						dC.o7 = this.ParseTex(treeNode2, barent.off, barent);
						break;
					case 10:
						this.Parse0a(treeNode2, barent.off, barent);
						break;
					case 11:
					case 15:
					case 19:
						this.Parse6(treeNode2, barent.off, barent);
						break;
					case 12:
						this.Parse0c(treeNode2, barent.off, barent);
						break;
					case 18:
						this.Parse12(treeNode2, barent.off, barent);
						break;
					case 24:
						this.Parse18(treeNode2, barent.off, barent);
						break;
					default:
						if (k != 29)
						{
							switch (k)
							{
							case 32:
								this.Parse20(treeNode2, barent.off, barent);
								break;
							case 34:
								this.Parse22(treeNode2, barent.off, barent);
								break;
							case 36:
								this.Parse24(treeNode2, barent.off, barent);
								break;
							}
						}
						else
						{
							this.Parse1D(treeNode2, barent.off, barent);
						}
						break;
					}
				}
				catch (NotSupportedException value)
				{
					this.errcol[treeNode2] = value;
					treeNode2.ImageKey = (treeNode2.SelectedImageKey = "RightsRestrictedHS.png");
				}
			}
			if ((dC.o4Map != null || dC.o4Mdlx != null) && dC.o7 != null)
			{
				this.aldc.Add(dC);
			}
			treeNode.Expand();
		}
		private string GetObjName(int modelId)
		{
			string result;
			if (this.dictObjName.TryGetValue(modelId, out result))
			{
				return result;
			}
			return null;
		}
		private void Parse0c(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			try
			{
				memoryStream.Position = 12L;
				int num = (int)binaryReader.ReadUInt16();
				int num2 = (int)binaryReader.ReadUInt16();
				int num3 = 52;
				StringWriter stringWriter = new StringWriter();
				for (int i = 0; i < num; i++)
				{
					memoryStream.Position = (long)(num3 + 64 * i);
					int num4 = binaryReader.ReadInt32();
					float num5 = binaryReader.ReadSingle();
					float num6 = binaryReader.ReadSingle();
					float num7 = binaryReader.ReadSingle();
					float num8 = binaryReader.ReadSingle();
					stringWriter.WriteLine("a.{0} {1:X4} ({2}, {3}, {4}, {5}) ; {6}", new object[]
					{
						i,
						num4,
						num5,
						num6,
						num7,
						num8,
						this.GetObjName(num4) ?? "?"
					});
				}
				num3 += 64 * num;
				stringWriter.WriteLine();
				for (int j = 0; j < num2; j++)
				{
					memoryStream.Position = (long)(num3 + 64 * j);
					int num9 = (int)binaryReader.ReadUInt16();
					int num10 = (int)binaryReader.ReadUInt16();
					float num11 = binaryReader.ReadSingle();
					float num12 = binaryReader.ReadSingle();
					float num13 = binaryReader.ReadSingle();
					float num14 = binaryReader.ReadSingle();
					float num15 = binaryReader.ReadSingle();
					float num16 = binaryReader.ReadSingle();
					float num17 = binaryReader.ReadSingle();
					float num18 = binaryReader.ReadSingle();
					stringWriter.WriteLine("b.{0} {1:X4} {10:X} ({2}, {3}, {4}, {5}) ({6}, {7}, {8}, {9})", new object[]
					{
						j,
						num9,
						num11,
						num12,
						num13,
						num14,
						num15,
						num16,
						num17,
						num18,
						num10
					});
				}
				num3 += 64 * num2;
				TreeNode treeNode = tn.Nodes.Add("appear");
				treeNode.Tag = new RDForm.Strif(xoff, stringWriter.ToString());
			}
			catch (EndOfStreamException innerException)
			{
				throw new NotSupportedException("EOF", innerException);
			}
		}
		private void Parse0a(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream si = new MemoryStream(ent.bin, false);
			ParseRADA parseRADA = new ParseRADA(si);
			parseRADA.Parse();
			TreeNode treeNode = tn.Nodes.Add("radar");
			RDForm.IMGDi tag = new RDForm.IMGDi(xoff, parseRADA.pic);
			treeNode.Tag = tag;
		}
		private void Parse03(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			try
			{
				StringWriter stringWriter = new StringWriter();
				new Parse03(stringWriter).Run(ent.bin);
				TreeNode treeNode = tn.Nodes.Add("A.I. code");
				treeNode.Tag = new RDForm.Strif(xoff, stringWriter.ToString());
			}
			catch (Exception innerException)
			{
				throw new NotSupportedException("Parser error.", innerException);
			}
		}
		private void Parse02(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			try
			{
				MemoryStream memoryStream = new MemoryStream(ent.bin, false);
				BinaryReader binaryReader = new BinaryReader(memoryStream);
				byte[] bin = ent.bin;
				int num = binaryReader.ReadInt32();
				if (num != 1)
				{
					throw new InvalidDataException("w0 != 1");
				}
				StringWriter stringWriter = new StringWriter();
				int num2 = binaryReader.ReadInt32();
				StrDec strDec = new StrDec();
				for (int i = 0; i < num2; i++)
				{
					memoryStream.Position = (long)(8 + 8 * i);
					int num3 = binaryReader.ReadInt32();
					int num4 = binaryReader.ReadInt32();
					memoryStream.Position = (long)num4;
					stringWriter.WriteLine("{0:x8} {1} --", num3, num4);
					foreach (StrCode current in strDec.DecodeEvt(bin, num4))
					{
						byte[] bin2 = current.bin;
						for (int j = 0; j < bin2.Length; j++)
						{
							byte b = bin2[j];
							stringWriter.Write("{0:x2} ", b);
						}
					}
					stringWriter.WriteLine();
					stringWriter.WriteLine(strDec.DecodeEvt(bin, num4));
					stringWriter.WriteLine();
				}
				TreeNode treeNode = tn.Nodes.Add("String table");
				treeNode.Tag = new RDForm.Strif(xoff, stringWriter.ToString());
			}
			catch (Exception innerException)
			{
				throw new NotSupportedException("Parser error.", innerException);
			}
		}
		private void Parse24(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			if (ent.id == "evt")
			{
				int num = ent.bin.Length / 256;
				Bitmap bitmap = new Bitmap(256, 2 * num);
				byte[] bin = ent.bin;
				for (int i = 0; i < num; i++)
				{
					int y = (i & 511) + 2 * (i & -512);
					for (int j = 0; j < 256; j++)
					{
						int num2 = (int)bin[j + 256 * i];
						num2 = ((num2 & 48) << 2 | (num2 & 3) << 4);
						bitmap.SetPixel(j, y, Color.FromArgb(num2, num2, num2));
					}
				}
				for (int k = 0; k < num; k++)
				{
					int y2 = (k & 511) + 2 * (k & -512) + 512;
					for (int l = 0; l < 256; l++)
					{
						int num3 = (int)bin[l + 256 * k];
						num3 = ((num3 & 192) | (num3 & 12) << 2);
						bitmap.SetPixel(l, y2, Color.FromArgb(num3, num3, num3));
					}
				}
				TreeNode treeNode = tn.Nodes.Add("font");
				treeNode.Tag = new RDForm.IMGDi(xoff, bitmap);
				return;
			}
			if (ent.id == "sys")
			{
				int num4 = ent.bin.Length / 256;
				Bitmap bitmap2 = new Bitmap(256, 2 * num4);
				byte[] bin2 = ent.bin;
				for (int m = 0; m < num4; m++)
				{
					int y3 = m;
					for (int n = 0; n < 256; n++)
					{
						int num5 = (int)bin2[n + 256 * m];
						num5 = ((num5 & 48) << 2 | (num5 & 3) << 4);
						bitmap2.SetPixel(n, y3, Color.FromArgb(num5, num5, num5));
					}
				}
				for (int num6 = 0; num6 < num4; num6++)
				{
					int y4 = num6 + num4;
					for (int num7 = 0; num7 < 256; num7++)
					{
						int num8 = (int)bin2[num7 + 256 * num6];
						num8 = ((num8 & 192) | (num8 & 12) << 2);
						bitmap2.SetPixel(num7, y4, Color.FromArgb(num8, num8, num8));
					}
				}
				TreeNode treeNode2 = tn.Nodes.Add("font");
				treeNode2.Tag = new RDForm.IMGDi(xoff, bitmap2);
				return;
			}
			if (ent.id == "icon")
			{
				Bitmap bitmap3 = new Bitmap(256, 160, PixelFormat.Format8bppIndexed);
				byte[] bin3 = ent.bin;
				BitmapData bitmapData = bitmap3.LockBits(new Rectangle(0, 0, bitmap3.Width, bitmap3.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
				try
				{
					Marshal.Copy(bin3, 0, bitmapData.Scan0, 40960);
				}
				finally
				{
					bitmap3.UnlockBits(bitmapData);
				}
				ColorPalette palette = bitmap3.Palette;
				byte[] array = new byte[8192];
				Buffer.BlockCopy(bin3, 40960, array, 0, 1024);
				Color[] entries = palette.Entries;
				for (int num9 = 0; num9 < 256; num9++)
				{
					int num10 = num9;
					int num11 = num9;
					int num12 = 4 * num10;
					entries[num11] = Color.FromArgb(Math.Min(255, (int)(array[num12 + 3] * 2)), (int)array[num12], (int)array[num12 + 1], (int)array[num12 + 2]);
				}
				bitmap3.Palette = palette;
				TreeNode treeNode3 = tn.Nodes.Add("font");
				treeNode3.Tag = new RDForm.IMGDi(xoff, bitmap3);
			}
		}
		private CollReader Parse6(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			new BinaryReader(memoryStream);
			CollReader collReader = new CollReader();
			collReader.Read(memoryStream);
			StringWriter stringWriter = new StringWriter();
			int num = 0;
			foreach (Co1 current in collReader.alCo1)
			{
				stringWriter.WriteLine("Co1[{0,2}] {1}", num, current);
				num++;
			}
			stringWriter.WriteLine("--");
			int num2 = 0;
			foreach (Co2 current2 in collReader.alCo2)
			{
				stringWriter.WriteLine("Co2[{0,3}] {1}", num2, current2);
				num2++;
			}
			stringWriter.WriteLine("--");
			int num3 = 0;
			foreach (Co3 current3 in collReader.alCo3)
			{
				stringWriter.WriteLine("Co3[{0,3}] {1}", num3, current3);
				num3++;
			}
			stringWriter.WriteLine("--");
			int num4 = 0;
			foreach (Vector4 current4 in collReader.alCo4)
			{
				stringWriter.WriteLine("Co4[{0,3}] {1}", num4, current4);
				num4++;
			}
			stringWriter.WriteLine("--");
			int num5 = 0;
			foreach (Plane current5 in collReader.alCo5)
			{
				stringWriter.WriteLine("Co5[{0,3}] {1}", num5, current5);
				num5++;
			}
			stringWriter.WriteLine("--");
			TreeNode treeNode = tn.Nodes.Add("collision");
			treeNode.Tag = new RDForm.Strif(xoff, stringWriter.ToString());
			return collReader;
		}
		private void Parse4(TreeNode tn, int xoff, ReadBar.Barent ent, DC curdc)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			memoryStream.Position = 144L;
			int num = binaryReader.ReadInt32();
			if (num == 2)
			{
				curdc.o4Map = this.Parse4_02(tn, xoff, ent);
				return;
			}
			if (num == 3)
			{
				curdc.o4Mdlx = this.Parse4_03(tn, xoff, ent);
				return;
			}
			throw new NotSupportedException("Unknown @90 .. " + num);
		}
		private Parse4Mdlx Parse4_03(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			Parse4Mdlx parse4Mdlx = new Parse4Mdlx(ent.bin);
			foreach (T31 current in parse4Mdlx.mdlx.alt31)
			{
				TreeNode treeNode = tn.Nodes.Add("Mdlx.T31");
				treeNode.Tag = new RDForm.Hexi(xoff + current.off, current.len);
				if (current.t21 != null)
				{
					TreeNode treeNode2 = treeNode.Nodes.Add("T21.alaxb");
					StringWriter stringWriter = new StringWriter();
					stringWriter.WriteLine("alaxb = Array of joints");
					stringWriter.WriteLine();
					stringWriter.WriteLine("current-joint-index,parent-joint-index");
					stringWriter.WriteLine(" scale x,y,z,w");
					stringWriter.WriteLine(" rotate x,y,z,w");
					stringWriter.WriteLine(" translate x,y,z,w");
					stringWriter.WriteLine();
					foreach (AxBone current2 in current.t21.alaxb)
					{
						stringWriter.WriteLine("{0},{1}", current2.cur, current2.parent);
						stringWriter.WriteLine(" {0},{1},{2},{3}", new object[]
						{
							current2.x1,
							current2.y1,
							current2.z1,
							current2.w1
						});
						stringWriter.WriteLine(" {0},{1},{2},{3}", new object[]
						{
							current2.x2,
							current2.y2,
							current2.z2,
							current2.w2
						});
						stringWriter.WriteLine(" {0},{1},{2},{3}", new object[]
						{
							current2.x3,
							current2.y3,
							current2.z3,
							current2.w3
						});
					}
					treeNode2.Tag = new RDForm.Strif(xoff + current.t21.off, stringWriter.ToString());
				}
				int num = 0;
				foreach (T13vif current3 in current.al13)
				{
					TreeNode treeNode3 = treeNode.Nodes.Add(string.Format("vifpkt{0} ({1})", num++, current3.texi));
					treeNode3.Tag = new RDForm.Vifi(xoff + current3.off, current3.bin);
				}
			}
			return parse4Mdlx;
		}
		private M4 Parse4_02(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			memoryStream.Position = 144L;
			int num = binaryReader.ReadInt32();
			if (num != 2)
			{
				throw new NotSupportedException("@90 != 2");
			}
			memoryStream.Position = 160L;
			int num2 = binaryReader.ReadInt32();
			binaryReader.ReadUInt16();
			int num3 = (int)binaryReader.ReadUInt16();
			int num4 = binaryReader.ReadInt32();
			int num5 = binaryReader.ReadInt32();
			List<int[]> list = new List<int[]>();
			for (int i = 0; i < num3; i++)
			{
				memoryStream.Position = (long)(144 + num4 + 4 * i);
				int num6 = binaryReader.ReadInt32();
				memoryStream.Position = (long)(144 + num6);
				List<int> list2 = new List<int>();
				while (true)
				{
					int num7 = (int)binaryReader.ReadUInt16();
					if (num7 == 65535)
					{
						break;
					}
					list2.Add(num7);
				}
				list.Add(list2.ToArray());
			}
			List<int> list3 = new List<int>();
			memoryStream.Position = (long)(144 + num5);
			for (int j = 0; j < 1; j++)
			{
				int num8 = binaryReader.ReadInt32();
				memoryStream.Position = (long)(144 + num8);
				for (int k = 0; k < num2; k++)
				{
					list3.Add((int)binaryReader.ReadUInt16());
				}
			}
			List<Vifpli> list4 = new List<Vifpli>();
			for (int l = 0; l < num2; l++)
			{
				memoryStream.Position = (long)(176 + 16 * l);
				int num9 = binaryReader.ReadInt32();
				int num10 = binaryReader.ReadInt32();
				MemoryStream memoryStream2 = new MemoryStream();
				while (true)
				{
					memoryStream.Position = (long)(144 + num9);
					int num11 = binaryReader.ReadInt32();
					int num12 = num11 & 65535;
					binaryReader.ReadInt32();
					byte[] array = binaryReader.ReadBytes(8 + 16 * num12);
					memoryStream2.Write(array, 0, array.Length);
					if (num11 >> 28 == 6)
					{
						break;
					}
					num9 += 16 + 16 * num12;
				}
				byte[] vifpkt = memoryStream2.ToArray();
				list4.Add(new Vifpli(vifpkt, num10));
				TreeNode treeNode = tn.Nodes.Add(string.Format("vifpkt{0} ({1})", l, num10));
				RDForm.MI mI = new RDForm.MI();
				mI.Add("vifpkt", xoff + 144 + num9);
				treeNode.Tag = new RDForm.Vifi(xoff + 144 + num9, mI, vifpkt);
			}
			return new M4
			{
				alb1t2 = list3,
				alb2 = list,
				alvifpkt = list4
			};
		}
		private MTex ParseTex(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			int num = binaryReader.ReadInt32();
			if (num == 0)
			{
				memoryStream.Position = 0L;
				return this.ParseTex_TIMf(tn, xoff, memoryStream, binaryReader);
			}
			if (num != -1)
			{
				throw new NotSupportedException("Unknown v00 .. " + num);
			}
			int num2 = binaryReader.ReadInt32();
			List<int> list = new List<int>();
			for (int i = 0; i < num2; i++)
			{
				list.Add(binaryReader.ReadInt32());
			}
			list.Add(ent.bin.Length);
			List<MTex> list2 = new List<MTex>();
			for (int j = 0; j < num2; j++)
			{
				memoryStream.Position = (long)list[j];
				byte[] buffer = binaryReader.ReadBytes(list[j + 1] - list[j]);
				TreeNode tn2 = tn.Nodes.Add("TIMc" + j);
				MemoryStream memoryStream2 = new MemoryStream(buffer, false);
				BinaryReader br = new BinaryReader(memoryStream2);
				list2.Add(this.ParseTex_TIMf(tn2, ent.off + list[j], memoryStream2, br));
			}
			if (list2.Count == 0)
			{
				return null;
			}
			return list2[0];
		}
		private MTex ParseTex_TIMf(TreeNode tn, int xoff, MemoryStream si, BinaryReader br)
		{
			br.ReadInt32();
			br.ReadInt32();
			int num = br.ReadInt32();
			int num2 = br.ReadInt32();
			int num3 = br.ReadInt32();
			int num4 = br.ReadInt32();
			int num5 = br.ReadInt32();
			br.ReadInt32();
			br.ReadInt32();
			SortedDictionary<int, int> sortedDictionary = new SortedDictionary<int, int>();
			si.Position = (long)num3;
			for (int i = 0; i < num2; i++)
			{
				sortedDictionary[i] = (int)br.ReadByte();
			}
			RDForm.Texctx texctx = new RDForm.Texctx();
			new List<int>();
			for (int j = 0; j < num + 1; j++)
			{
			}
			List<Bitmap> list = new List<Bitmap>();
			for (int k = 0; k < num2; k++)
			{
				int num6 = num4;
				si.Position = (long)(num6 + 32);
				texctx.Do1(si);
				int offBin = texctx.offBin;
				int num7 = num4 + 144 * (1 + sortedDictionary[k]);
				si.Position = (long)(num7 + 32);
				texctx.Do1(si);
				int offBin2 = texctx.offBin;
				int num8 = num5 + 160 * k + 32;
				si.Position = (long)num8;
				STim sTim = texctx.Do2(si);
				RDForm.MI mI = new RDForm.MI();
				mI.Add("offp1pal", xoff + num6);
				mI.Add("offp1tex", xoff + num7);
				mI.Add("offp2", xoff + num8);
				mI.Add("offTex", xoff + offBin2);
				mI.Add("offPal", xoff + offBin);
				TreeNode treeNode = tn.Nodes.Add(string.Format("tex{0} ({1})", k, texctx.t0PSM));
				treeNode.Tag = new RDForm.Texi(xoff + offBin2, mI, sTim);
				list.Add(sTim.Generate());
			}
			return new MTex(list);
		}
		private void RDForm_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (array != null)
			{
				if (array.Length >= 2)
				{
					switch (MessageBox.Show(this, "Would you try batch export instead of viewer?", Application.ProductName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
					{
					case DialogResult.Yes:
					{
						BEXForm bEXForm = new BEXForm(this);
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string fp = array2[i];
							bEXForm.Addfp(fp);
						}
						bEXForm.Show();
						return;
					}
					case DialogResult.No:
						break;
					default:
						return;
					}
				}
				string[] array3 = array;
				for (int j = 0; j < array3.Length; j++)
				{
					string path = array3[j];
					string key;
					switch (key = Path.GetExtension(path).ToLowerInvariant())
					{
					case ".map":
					case ".mdlx":
					case ".apdx":
					case ".fm":
					case ".2dd":
					case ".bar":
					case ".2ld":
					case ".mset":
					case ".pax":
					case ".wd":
					case ".vsb":
					case ".ard":
					case ".imd":
					case ".mag":
						this.fpld = path;
						Application.Idle += new EventHandler(this.Application_Idle);
						break;
					}
				}
			}
		}
		private void Application_Idle(object sender, EventArgs e)
		{
			if (this.fpld != null)
			{
				Application.Idle -= new EventHandler(this.Application_Idle);
				this.LoadMap(this.WraponDemand(this.fpld));
				this.fpld = null;
			}
		}
		public void LoadAny(string fpld)
		{
			this.LoadMap(this.WraponDemand(fpld));
		}
		private string WraponDemand(string fpld)
		{
			string a;
			if ((a = Path.GetExtension(fpld).ToLowerInvariant()) != null)
			{
				if (a == ".wd")
				{
					return RDForm.WUt.Usebar(fpld, 32, "wd");
				}
				if (a == ".vsb")
				{
					return RDForm.WUt.Usebar(fpld, 34, "vsb");
				}
				if (a == ".pax")
				{
					return RDForm.WUt.Usebar(fpld, 18, "pax");
				}
				if (a == ".imd")
				{
					return RDForm.WUt.Usebar(fpld, 24, "imd");
				}
			}
			return fpld;
		}
		private void treeView1_DoubleClick(object sender, EventArgs e)
		{
		}
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			switch (e.Action)
			{
			case TreeViewAction.ByKeyboard:
			case TreeViewAction.ByMouse:
			{
				TreeNode node = e.Node;
				this.bserr.Hide();
				Exception ex = (Exception)this.errcol[node];
				if (ex != null)
				{
					this.bserr.Show();
					this.bserr.Tag = ex;
				}
				if (node != null)
				{
					RDForm.Hexi hexi = node.Tag as RDForm.Hexi;
					if (hexi != null)
					{
						this.hv1.RangeMarkedList.Clear();
						if (hexi.off >= 0)
						{
							this.hv1.SetPos(hexi.off);
							if (hexi.len > 0)
							{
								this.hv1.AddRangeMarked(hexi.off, hexi.len, Color.FromArgb(50, Color.LimeGreen), Color.FromArgb(200, Color.LimeGreen));
							}
						}
						this.lvMI.Items.Clear();
						if (hexi.mi != null)
						{
							foreach (KeyValuePair<string, int> current in hexi.mi.col2off)
							{
								ListViewItem listViewItem = this.lvMI.Items.Add(current.Key);
								listViewItem.Tag = current.Value;
							}
						}
					}
					RDForm.Texi texi = hexi as RDForm.Texi;
					if (texi != null)
					{
						this.setPicPane(new Image[]
						{
							texi.st.pic,
							texi.st.Generate()
						});
						this.labelHelpMore.Text = string.Concat(new string[]
						{
							string.Format("tfx({0,-10}) tcc({1,-4}) wms({2,-7}) wmt({3,-7})\n", new object[]
							{
								texi.st.tfx,
								texi.st.tcc,
								texi.st.wms,
								texi.st.wmt
							}),
							(texi.st.wms == WM.RClamp) ? string.Format("Horz-clamp({0,4},{1,4}) ", texi.st.minu, texi.st.maxu) : "",
							(texi.st.wms == WM.RRepeat) ? string.Format("UMSK({0,4}) UFIX({1,4}) ", texi.st.minu, texi.st.maxu) : "",
							(texi.st.wmt == WM.RClamp) ? string.Format("Vert-clamp({0,4},{1,4}) ", texi.st.minv, texi.st.maxv) : "",
							(texi.st.wmt == WM.RRepeat) ? string.Format("VMSK({0,4}) VFIX({1,4}) ", texi.st.minv, texi.st.maxv) : ""
						});
					}
					RDForm.Vifi vifi = hexi as RDForm.Vifi;
					if (vifi != null)
					{
						string text = RDForm.Parseivif.Parse(vifi.vifpkt);
						this.setTextPane(text, false);
					}
					RDForm.IMGDi iMGDi = hexi as RDForm.IMGDi;
					if (iMGDi != null)
					{
						this.setPicPane(iMGDi.p);
					}
					RDForm.WAVi wAVi = hexi as RDForm.WAVi;
					if (wAVi != null)
					{
						if (this.WavePlayer != null)
						{
							this.WavePlayer(wAVi);
						}
						else
						{
							Directory.CreateDirectory("tmp");
							string text2 = "tmp\\_" + wAVi.w.fn;
							File.WriteAllBytes(text2, wAVi.w.bin);
							Process.Start(text2);
						}
					}
					RDForm.Stri stri = hexi as RDForm.Stri;
					if (stri != null)
					{
						this.setTextPane(stri.s, false);
					}
					RDForm.Strif strif = hexi as RDForm.Strif;
					if (strif != null)
					{
						this.setTextPane(strif.s, true);
					}
					RDForm.Btni btni = hexi as RDForm.Btni;
					if (btni != null)
					{
						while (this.p1.Controls.Count != 0)
						{
							this.p1.Controls[0].Dispose();
						}
						Button button = new Button();
						button.AutoSize = true;
						button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
						button.Text = "Click";
						button.Click += btni.onClicked;
						this.p1.Controls.Add(button);
					}
				}
				return;
			}
			default:
				return;
			}
		}
		private void setTextPane(string text, bool fixedfont)
		{
			while (this.p1.Controls.Count != 0)
			{
				this.p1.Controls[0].Dispose();
			}
			TextBox textBox = new TextBox();
			textBox.Multiline = true;
			textBox.ScrollBars = ScrollBars.Vertical;
			textBox.BorderStyle = BorderStyle.None;
			textBox.Dock = DockStyle.Fill;
			textBox.ReadOnly = true;
			textBox.Visible = true;
			textBox.Parent = this.p1;
			if (fixedfont)
			{
				textBox.Font = new Font(FontFamily.GenericMonospace, textBox.Font.SizeInPoints);
				textBox.WordWrap = false;
				textBox.ScrollBars = ScrollBars.Both;
			}
			textBox.Text = text;
			this.labelHelpMore.Text = "";
		}
		private void setPicPane(Image pic)
		{
			while (this.p1.Controls.Count != 0)
			{
				this.p1.Controls[0].Dispose();
			}
			new PictureBox
			{
				Parent = this.p1,
				Image = pic,
				SizeMode = PictureBoxSizeMode.AutoSize,
				Visible = true
			}.MouseMove += new MouseEventHandler(this.pb_MouseMove);
			this.labelHelpMore.Text = "";
		}
		private void setPicPane(Image[] pics)
		{
			while (this.p1.Controls.Count != 0)
			{
				this.p1.Controls[0].Dispose();
			}
			FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
			flowLayoutPanel.Parent = this.p1;
			flowLayoutPanel.Dock = DockStyle.Fill;
			for (int i = 0; i < pics.Length; i++)
			{
				Image image = pics[i];
				PictureBox pictureBox = new PictureBox();
				pictureBox.Image = image;
				pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
				pictureBox.Visible = true;
				pictureBox.MouseMove += new MouseEventHandler(this.pb_MouseMove);
				flowLayoutPanel.Controls.Add(pictureBox);
			}
			this.labelHelpMore.Text = "";
		}
		private void pb_MouseMove(object sender, MouseEventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			Bitmap bitmap = (Bitmap)pictureBox.Image;
			if (e.X < bitmap.Width && e.Y < bitmap.Height)
			{
				Color pixel = bitmap.GetPixel(e.X, e.Y);
				this.labelPixi.Text = string.Concat(new object[]
				{
					"x:",
					e.X,
					"\ny:",
					e.Y,
					"\n\nR:",
					pixel.R,
					"\nG:",
					pixel.G,
					"\nB:",
					pixel.B,
					"\nA:",
					pixel.A,
					"\n"
				});
			}
		}
		private void lvMI_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.lvMI.SelectedItems.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					this.hv1.SetPos((int)listViewItem.Tag);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		private void button1_Click(object sender, EventArgs e)
		{
			this.visf = new Visf(this.aldc, this.coll);
			this.visf.Show();
		}
		private void Parse1D(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			memoryStream.Position = 12L;
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int index = binaryReader.ReadInt32();
				int count = binaryReader.ReadInt32();
				MemoryStream si = new MemoryStream(ent.bin, index, count, false);
				PicIMGD picIMGD = ParseIMGD.TakeIMGD(si);
				TreeNode treeNode = tn.Nodes.Add("IMGD." + RDForm.PalC2s.Guess(picIMGD));
				treeNode.Tag = new RDForm.IMGDi(ent.off, picIMGD.pic);
			}
		}
		private void Parse12(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			new BinaryReader(memoryStream);
			PicPAX picPAX = ParsePAX.ReadPAX(memoryStream);
			int num = 0;
			foreach (R current in picPAX.alr)
			{
				num++;
				int num2 = 0;
				foreach (Bitmap current2 in current.pics)
				{
					num2++;
					TreeNode treeNode = tn.Nodes.Add(string.Concat(new object[]
					{
						num,
						".",
						num2,
						".p.",
						RDForm.PalC2s.Guess(current2)
					}));
					treeNode.Tag = new RDForm.IMGDi(ent.off, current2);
				}
			}
		}
		private void Parse22(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			new BinaryReader(memoryStream);
			Wavo[] array = ParseSD.ReadIV(memoryStream);
			Wavo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Wavo wavo = array2[i];
				TreeNode treeNode = tn.Nodes.Add(wavo.fn);
				treeNode.Tag = new RDForm.WAVi(ent.off, wavo);
				this.setWAVi(treeNode);
			}
		}
		private void Parse20(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			new BinaryReader(memoryStream);
			Wavo[] array = ParseSD.ReadWD(memoryStream);
			Wavo[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Wavo wavo = array2[i];
				TreeNode treeNode = tn.Nodes.Add(wavo.fn);
				treeNode.Tag = new RDForm.WAVi(ent.off, wavo);
				this.setWAVi(treeNode);
			}
		}
		private void setWAVi(TreeNode tnt)
		{
			tnt.ImageKey = (tnt.SelectedImageKey = "SpeechMicHS.png");
		}
		private void Parse18(TreeNode tn, int xoff, ReadBar.Barent ent)
		{
			MemoryStream memoryStream = new MemoryStream(ent.bin, false);
			new BinaryReader(memoryStream);
			PicIMGD picIMGD = ParseIMGD.TakeIMGD(memoryStream);
			TreeNode treeNode = tn.Nodes.Add("IMGD." + RDForm.PalC2s.Guess(picIMGD));
			treeNode.Tag = new RDForm.IMGDi(ent.off, picIMGD.pic);
		}
		private void bserr_Click(object sender, EventArgs e)
		{
			Form form = new Form();
			form.StartPosition = FormStartPosition.Manual;
			form.Width = base.Width;
			form.Height = base.Height / 4;
			form.Location = this.bserr.PointToScreen(new Point(0, this.bserr.Height));
			form.FormBorderStyle = FormBorderStyle.SizableToolWindow;
			form.Text = "Error message from analiser";
			form.Show(this);
			TextBox textBox = new TextBox();
			textBox.BorderStyle = BorderStyle.None;
			textBox.Multiline = true;
			textBox.Parent = form;
			textBox.Dock = DockStyle.Fill;
			textBox.ScrollBars = ScrollBars.Vertical;
			textBox.BackColor = Color.FromKnownColor(KnownColor.Info);
			textBox.ForeColor = Color.FromKnownColor(KnownColor.InfoText);
			textBox.Text = ((Exception)this.bserr.Tag).ToString();
			textBox.Show();
			Button button = new Button();
			button.Click += new EventHandler(this.buttonCloseMe_Click);
			button.Parent = form;
			button.Text = "Close!";
			form.AcceptButton = button;
			form.CancelButton = button;
			form.Activate();
			textBox.Select();
			textBox.SelectAll();
		}
		private void buttonCloseMe_Click(object sender, EventArgs e)
		{
			((Form)((Button)sender).Parent).Close();
		}
		private void RDForm_Load(object sender, EventArgs e)
		{
			foreach (Match match in Regex.Matches(File.ReadAllText(Path.Combine(Application.StartupPath, "objname.txt"), Encoding.UTF8).Replace("\r", "\n"), "^(?<d>[0-9A-F]{4})=(?<n>.+)$", RegexOptions.IgnoreCase | RegexOptions.Multiline))
			{
				this.dictObjName[Convert.ToInt32(match.Groups["d"].Value, 16)] = match.Groups["n"].Value;
			}
			if (this.fpread != null)
			{
				this.LoadMap(this.WraponDemand(this.fpread));
			}
		}
		private void bExportBin_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = this.treeView1.SelectedNode;
			if (selectedNode == null)
			{
				MessageBox.Show(this, "No item selected!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			RDForm.Hexi hexi = selectedNode.Tag as RDForm.Hexi;
			if (hexi == null)
			{
				MessageBox.Show(this, "Export not supported!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			if (hexi.len == 0)
			{
				MessageBox.Show(this, "It does not declare bin LENGTH!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = selectedNode.Text + ".bin";
			saveFileDialog.Filter = "*.bin|*.bin||";
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				using (FileStream fileStream = File.Create(saveFileDialog.FileName))
				{
					fileStream.Write(this.rdbin, hexi.off, hexi.len);
					fileStream.Close();
				}
				MessageBox.Show(this, "Saved!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		private void bExportAll_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you really explode all?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
			{
				return;
			}
			string text = Path.Combine(Application.StartupPath, "export\\" + Path.GetFileName(this.OpenedPath).Replace(".", "_"));
			this.Expall(this.treeView1.Nodes, text);
			Process.Start("explorer.exe", " \"" + text + "\"");
		}
		public void ExpallTo(string dirTo)
		{
			this.Expall(this.treeView1.Nodes, dirTo);
		}
		private void Expall(TreeNodeCollection tnc, string dirTo)
		{
			foreach (TreeNode treeNode in tnc)
			{
				RDForm.UniName un = new RDForm.UniName();
				foreach (TreeNode tn in treeNode.Nodes)
				{
					this.Expthem(dirTo, tn, un);
				}
			}
		}
		private void Expthem(string dirTo, TreeNode tn, RDForm.UniName un)
		{
			while (this.p1.Controls.Count != 0)
			{
				this.p1.Controls[0].Dispose();
			}
			string dirn = un.Get(tn.Text);
			this.WavePlayer = delegate(RDForm.WAVi wi)
			{
				Directory.CreateDirectory(dirTo);
				File.WriteAllBytes(Path.Combine(dirTo, un.Get(dirn)), wi.w.bin);
			};
			this.treeView1.SelectedNode = tn;
			this.treeView1_AfterSelect(this.treeView1, new TreeViewEventArgs(tn, TreeViewAction.ByKeyboard));
			base.Update();
			this.WavePlayer = null;
			foreach (Control control in this.p1.Controls)
			{
				if (control is PictureBox)
				{
					Directory.CreateDirectory(dirTo);
					PictureBox pictureBox = (PictureBox)control;
					pictureBox.Image.Save(Path.Combine(dirTo, un.Get(dirn + ".png")), ImageFormat.Png);
				}
				if (control is TextBox)
				{
					Directory.CreateDirectory(dirTo);
					TextBox textBox = (TextBox)control;
					File.WriteAllText(Path.Combine(dirTo, un.Get(dirn + ".txt")), textBox.Text, Encoding.Default);
				}
				if (control is FlowLayoutPanel)
				{
					int num = 1;
					foreach (Control control2 in control.Controls)
					{
						if (control2 is PictureBox)
						{
							Directory.CreateDirectory(dirTo);
							PictureBox pictureBox2 = (PictureBox)control2;
							pictureBox2.Image.Save(Path.Combine(dirTo, un.Get(string.Concat(new object[]
							{
								dirn,
								"_",
								num,
								".png"
							}))), ImageFormat.Png);
							num++;
						}
					}
				}
			}
			RDForm.Hexi hexi = tn.Tag as RDForm.Hexi;
			if (hexi.len != 0)
			{
				Directory.CreateDirectory(dirTo);
				using (FileStream fileStream = File.Create(Path.Combine(dirTo, un.Get(dirn + ".bin"))))
				{
					fileStream.Write(this.rdbin, hexi.off, hexi.len);
					fileStream.Close();
				}
			}
			RDForm.UniName uniName = new RDForm.UniName();
			string path = uniName.Get(dirn);
			foreach (TreeNode tn2 in tn.Nodes)
			{
				this.Expthem(Path.Combine(dirTo, path), tn2, uniName);
			}
		}
		private void bBatExp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			BEXForm bEXForm = new BEXForm(this);
			bEXForm.Show();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RDForm));
			this.treeView1 = new TreeView();
			this.IL = new ImageList(this.components);
			this.linkLabel1 = new LinkLabel();
			this.p1 = new Panel();
			this.lvMI = new ListView();
			this.button1 = new Button();
			this.label1 = new Label();
			this.label2 = new Label();
			this.bserr = new Button();
			this.label3 = new Label();
			this.labelPixi = new Label();
			this.labelHelpMore = new Label();
			this.bExportBin = new Button();
			this.hv1 = new HexVwer();
			this.bExportAll = new Button();
			this.bBatExp = new LinkLabel();
			base.SuspendLayout();
			this.treeView1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.treeView1.HideSelection = false;
			this.treeView1.ImageIndex = 0;
			this.treeView1.ImageList = this.IL;
			this.treeView1.Location = new Point(12, 23);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = 0;
			this.treeView1.Size = new Size(246, 263);
			this.treeView1.TabIndex = 1;
			this.treeView1.DoubleClick += new EventHandler(this.treeView1_DoubleClick);
			this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
			this.IL.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("IL.ImageStream");
			this.IL.TransparentColor = Color.Magenta;
			this.IL.Images.SetKeyName(0, "RightArrowHS.png");
			this.IL.Images.SetKeyName(1, "RightsRestrictedHS.png");
			this.IL.Images.SetKeyName(2, "SpeechMicHS.png");
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new Point(12, 9);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new Size(56, 12);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel1";
			this.p1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.p1.AutoScroll = true;
			this.p1.BorderStyle = BorderStyle.FixedSingle;
			this.p1.Location = new Point(264, 24);
			this.p1.Name = "p1";
			this.p1.Size = new Size(370, 262);
			this.p1.TabIndex = 2;
			this.lvMI.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
			this.lvMI.HideSelection = false;
			this.lvMI.Location = new Point(640, 322);
			this.lvMI.MultiSelect = false;
			this.lvMI.Name = "lvMI";
			this.lvMI.Size = new Size(53, 252);
			this.lvMI.TabIndex = 10;
			this.lvMI.UseCompatibleStateImageBehavior = false;
			this.lvMI.View = View.List;
			this.lvMI.SelectedIndexChanged += new EventHandler(this.lvMI_SelectedIndexChanged);
			this.button1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(640, 24);
			this.button1.Name = "button1";
			this.button1.Size = new Size(59, 56);
			this.button1.TabIndex = 3;
			this.button1.Text = "Map &viewer";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.label1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 578);
			this.label1.Name = "label1";
			this.label1.Size = new Size(295, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "* Drop a map file to window. Then click \"viewer\" button.";
			this.label2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 596);
			this.label2.Margin = new Padding(3, 6, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(423, 12);
			this.label2.TabIndex = 12;
			this.label2.Text = "* Currently accepts map mdlx apdx fm 2dd bar 2ld mset pax wd vsb ard imd mag";
			this.bserr.Image = Resources.ActualSizeHS;
			this.bserr.ImageAlign = ContentAlignment.MiddleRight;
			this.bserr.Location = new Point(12, 292);
			this.bserr.Name = "bserr";
			this.bserr.Size = new Size(131, 23);
			this.bserr.TabIndex = 7;
			this.bserr.Text = "Show error popup";
			this.bserr.TextAlign = ContentAlignment.MiddleLeft;
			this.bserr.UseVisualStyleBackColor = true;
			this.bserr.Visible = false;
			this.bserr.Click += new EventHandler(this.bserr_Click);
			this.label3.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(640, 83);
			this.label3.Name = "label3";
			this.label3.Size = new Size(55, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "Pixel info:";
			this.labelPixi.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.labelPixi.AutoSize = true;
			this.labelPixi.Location = new Point(640, 95);
			this.labelPixi.Name = "labelPixi";
			this.labelPixi.Size = new Size(11, 96);
			this.labelPixi.TabIndex = 5;
			this.labelPixi.Text = "...\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
			this.labelHelpMore.Font = new Font("ＭＳ ゴシック", 9f, FontStyle.Regular, GraphicsUnit.Point, 128);
			this.labelHelpMore.Location = new Point(149, 289);
			this.labelHelpMore.Name = "labelHelpMore";
			this.labelHelpMore.Size = new Size(544, 30);
			this.labelHelpMore.TabIndex = 8;
			this.labelHelpMore.Text = "...";
			this.bExportBin.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.bExportBin.FlatStyle = FlatStyle.Flat;
			this.bExportBin.Location = new Point(640, 194);
			this.bExportBin.Name = "bExportBin";
			this.bExportBin.Size = new Size(59, 40);
			this.bExportBin.TabIndex = 6;
			this.bExportBin.Text = "&Export bin";
			this.bExportBin.UseVisualStyleBackColor = true;
			this.bExportBin.Click += new EventHandler(this.bExportBin_Click);
			this.hv1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.hv1.AntiFlick = true;
			this.hv1.BorderStyle = BorderStyle.FixedSingle;
			this.hv1.ByteWidth = 16;
			this.hv1.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.hv1.Location = new Point(12, 322);
			this.hv1.Margin = new Padding(3, 4, 3, 4);
			this.hv1.Name = "hv1";
			this.hv1.OffDelta = 0;
			this.hv1.PgScroll = PgScrollType.ScreenSizeBased;
			this.hv1.Size = new Size(622, 252);
			this.hv1.TabIndex = 9;
			this.hv1.UnitPg = 512;
			this.bExportAll.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.bExportAll.FlatStyle = FlatStyle.Flat;
			this.bExportAll.Location = new Point(640, 240);
			this.bExportAll.Name = "bExportAll";
			this.bExportAll.Size = new Size(59, 46);
			this.bExportAll.TabIndex = 13;
			this.bExportAll.Text = "Export &all";
			this.bExportAll.UseVisualStyleBackColor = true;
			this.bExportAll.Click += new EventHandler(this.bExportAll_Click);
			this.bBatExp.AutoSize = true;
			this.bBatExp.LinkArea = new LinkArea(5, 14);
			this.bBatExp.Location = new Point(313, 578);
			this.bBatExp.Name = "bBatExp";
			this.bBatExp.Size = new Size(104, 17);
			this.bBatExp.TabIndex = 14;
			this.bBatExp.TabStop = true;
			this.bBatExp.Text = "New: &Batch Export!";
			this.bBatExp.UseCompatibleTextRendering = true;
			this.bBatExp.LinkClicked += new LinkLabelLinkClickedEventHandler(this.bBatExp_LinkClicked);
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(711, 632);
			base.Controls.Add(this.bBatExp);
			base.Controls.Add(this.bExportAll);
			base.Controls.Add(this.bExportBin);
			base.Controls.Add(this.labelHelpMore);
			base.Controls.Add(this.labelPixi);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.bserr);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.lvMI);
			base.Controls.Add(this.p1);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.treeView1);
			base.Controls.Add(this.hv1);
			base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "RDForm";
			this.Text = "map exploring";
			base.Load += new EventHandler(this.RDForm_Load);
			base.DragDrop += new DragEventHandler(this.RDForm_DragDrop);
			base.DragEnter += new DragEventHandler(this.RDForm_DragEnter);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
