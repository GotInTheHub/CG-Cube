using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public class Sphere : Shape
    {
        public Sphere(Color color, int size = 1) : base(color, size)
        {
        }

        public override void ApplyChange(List<Vector> vertexBuffer)
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            throw new NotImplementedException();
        }
    }
}
