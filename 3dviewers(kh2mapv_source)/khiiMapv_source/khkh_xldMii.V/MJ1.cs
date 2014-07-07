namespace khkh_xldMii.V
{
    public class MJ1
    {
        public float factor;
        public int matrixIndex;
        public int vertexIndex;

        public MJ1(int matrixIndex, int vertexIndex, float factor)
        {
            this.matrixIndex = matrixIndex;
            this.vertexIndex = vertexIndex;
            this.factor = factor;
        }
    }
}