using System;
namespace khiiMapv.Put
{
	public class Putfragment
	{
		public int baseVertexIndex;
		public int minimumVertexIndex;
		public int vertexCount;
		public int startIndex;
		public int primitiveCount;
		public IPuteffect effect;
		public Putfragment(int baseVertexIndex, int minimumVertexIndex, int vertexCount, int startIndex, int primitiveCount)
		{
			this.baseVertexIndex = baseVertexIndex;
			this.minimumVertexIndex = minimumVertexIndex;
			this.vertexCount = vertexCount;
			this.startIndex = startIndex;
			this.primitiveCount = primitiveCount;
		}
	}
}
