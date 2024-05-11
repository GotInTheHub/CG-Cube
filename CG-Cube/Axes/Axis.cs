using CG_Cube.Algebra;
using CG_Cube.Shapes;

namespace CG_Cube.Axes
{
    public class Axis : Shape
    {
        public Axis(Color color, float size = 3) : base(color, size)
        {
            VertexBuffer =
            [
                new Vector(0, 0, 0),
                new Vector(_size, _size, _size)
            ];
            Projection = VertexBuffer.ConvertAll(v => v.Clone());
        }

        public override void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            Pen pen = new(color, 2f);
            graphics.DrawLine(pen, vertexBuffer[0].X, vertexBuffer[0].Y, vertexBuffer[1].X, vertexBuffer[1].Y);
        }

        public override string ToString()
        {
            return "Axis";
        }
    }
}
