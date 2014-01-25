namespace khiiMapv.Put
{
    using System;

    public class Putfragment
    {
        public int baseVertexIndex;
        public IPuteffect effect;
        public int minimumVertexIndex;
        public int primitiveCount;
        public int startIndex;
        public int vertexCount;

        public Putfragment(int baseVertexIndex, int minimumVertexIndex, int vertexCount, int startIndex, int primitiveCount)
        {
            base..ctor();
            this.baseVertexIndex = baseVertexIndex;
            this.minimumVertexIndex = minimumVertexIndex;
            this.vertexCount = vertexCount;
            this.startIndex = startIndex;
            this.primitiveCount = primitiveCount;
            return;
        }
    }
}

