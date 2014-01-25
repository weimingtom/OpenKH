namespace khkh_xldMii.V
{
    using System;

    public class MJ1
    {
        public float factor;
        public int matrixIndex;
        public int vertexIndex;

        public MJ1(int matrixIndex, int vertexIndex, float factor)
        {
            base..ctor();
            this.matrixIndex = matrixIndex;
            this.vertexIndex = vertexIndex;
            this.factor = factor;
            return;
        }
    }
}

