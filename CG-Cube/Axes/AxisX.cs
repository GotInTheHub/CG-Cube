
using CG_Cube.Algebra;

namespace CG_Cube.Axes
{
    public class AxisX : Axis
    {

        public AxisX(Color color, int size = 100) : base(color, size)
        {
            VertexBuffer =
            [
                new Vector(0, 0, 0),
                new Vector(_size, 0, 0)
            ];
        }

        public override void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            base.Draw(graphics, vertexBuffer);

            Font font = new("Arial", 10);
            PointF point = new(vertexBuffer[1].X, vertexBuffer[1].Y);
            graphics.DrawString("X", font, Brushes.Red, point);
        }
    }
}
