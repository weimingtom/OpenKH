using hex04BinTrack;
using SlimDX;
using System;
using System.IO;
namespace khkh_xldMii.V
{
	public class SimaVU1
	{
		public static Body1 Sima(VU1Mem vu1mem, Matrix[] Ma, int tops, int top2, int tsel, int[] alaxi, Matrix Mv)
		{
			MemoryStream memoryStream = new MemoryStream(vu1mem.vumem, true);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			memoryStream.Position = (long)(16 * tops);
			int num = binaryReader.ReadInt32();
			if (num != 1 && num != 2)
			{
				throw new ProtInvalidTypeException();
			}
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			if (num == 1)
			{
				binaryReader.ReadInt32();
			}
			if (num == 1)
			{
				binaryReader.ReadInt32();
			}
			int num5 = (num == 1) ? binaryReader.ReadInt32() : 0;
			int num6 = (num == 1) ? binaryReader.ReadInt32() : 0;
			int num7 = binaryReader.ReadInt32();
			int num8 = binaryReader.ReadInt32();
			binaryReader.ReadInt32();
			int num9 = binaryReader.ReadInt32();
			memoryStream.Position = (long)(16 * (tops + num4));
			int[] array = new int[num9];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = binaryReader.ReadInt32();
			}
			Body1 body = new Body1();
			body.t = tsel;
			body.alvert = new Vector3[num7];
			body.avail = (num5 == 0 && num == 1);
			Vector3[] array2 = new Vector3[num7];
			int num10 = 0;
			memoryStream.Position = (long)(16 * (tops + num8));
			for (int j = 0; j < array.Length; j++)
			{
				Matrix transformation = Ma[alaxi[j]] * Mv;
				int num11 = array[j];
				int k = 0;
				while (k < num11)
				{
					float x = binaryReader.ReadSingle();
					float y = binaryReader.ReadSingle();
					float z = binaryReader.ReadSingle();
					float w = binaryReader.ReadSingle();
					Vector3 coordinate = new Vector3(x, y, z);
					Vector3 vector = Vector3.TransformCoordinate(coordinate, transformation);
					body.alvert[num10] = vector;
					Vector4 vector2 = new Vector4(x, y, z, w);
					Vector4 vector3 = Vector4.Transform(vector2, transformation);
					array2[num10] = new Vector3(vector3.X, vector3.Y, vector3.Z);
					k++;
					num10++;
				}
			}
			body.aluv = new Vector2[num2];
			body.alvi = new int[num2];
			body.alfl = new int[num2];
			memoryStream.Position = (long)(16 * (tops + num3));
			for (int l = 0; l < num2; l++)
			{
				int num12 = (int)(binaryReader.ReadUInt16() / 16);
				binaryReader.ReadUInt16();
				int num13 = (int)(binaryReader.ReadUInt16() / 16);
				binaryReader.ReadUInt16();
				body.aluv[l] = new Vector2((float)num12 / 256f, (float)num13 / 256f);
				body.alvi[l] = (int)binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
				body.alfl[l] = (int)binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
			}
			if (num5 != 0)
			{
				memoryStream.Position = (long)(16 * (tops + num6));
				int num14 = binaryReader.ReadInt32();
				int num15 = binaryReader.ReadInt32();
				int num16 = binaryReader.ReadInt32();
				int num17 = binaryReader.ReadInt32();
				int num18 = 0;
				if (num5 >= 5)
				{
					num18 = binaryReader.ReadInt32();
					binaryReader.ReadInt32();
					binaryReader.ReadInt32();
					binaryReader.ReadInt32();
				}
				Vector3[] array3 = new Vector3[num7];
				int m;
				for (m = 0; m < num14; m++)
				{
					int num19 = binaryReader.ReadInt32();
					array3[m] = body.alvert[num19];
				}
				if (num5 >= 2)
				{
					memoryStream.Position = (memoryStream.Position + 15L & -16L);
					int n = 0;
					while (n < num15)
					{
						int num20 = binaryReader.ReadInt32();
						int num21 = binaryReader.ReadInt32();
						array3[m] = array2[num20] + array2[num21];
						n++;
						m++;
					}
				}
				if (num5 >= 3)
				{
					memoryStream.Position = (memoryStream.Position + 15L & -16L);
					int num22 = 0;
					while (num22 < num16)
					{
						int num23 = binaryReader.ReadInt32();
						int num24 = binaryReader.ReadInt32();
						int num25 = binaryReader.ReadInt32();
						array3[m] = array2[num23] + array2[num24] + array2[num25];
						num22++;
						m++;
					}
				}
				if (num5 >= 4)
				{
					memoryStream.Position = (memoryStream.Position + 15L & -16L);
					int num26 = 0;
					while (num26 < num17)
					{
						int num27 = binaryReader.ReadInt32();
						int num28 = binaryReader.ReadInt32();
						int num29 = binaryReader.ReadInt32();
						int num30 = binaryReader.ReadInt32();
						array3[m] = array2[num27] + array2[num28] + array2[num29] + array2[num30];
						num26++;
						m++;
					}
				}
				if (num5 >= 5)
				{
					memoryStream.Position = (memoryStream.Position + 15L & -16L);
					int num31 = 0;
					while (num31 < num18)
					{
						int num32 = binaryReader.ReadInt32();
						int num33 = binaryReader.ReadInt32();
						int num34 = binaryReader.ReadInt32();
						int num35 = binaryReader.ReadInt32();
						int num36 = binaryReader.ReadInt32();
						array3[m] = array2[num32] + array2[num33] + array2[num34] + array2[num35] + array2[num36];
						num31++;
						m++;
					}
				}
				body.alvert = array3;
			}
			return body;
		}
	}
}
