using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public class Sphere : Shape
    {
        public Sphere(Color color, int size = 1) : base(color, size)
        {
        }

        public override void Reset()
        {
            Projection = new Sphere(color, _size).Projection;
            base.Reset();
        }

        public override void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            throw new NotImplementedException();
        }
    }
}
