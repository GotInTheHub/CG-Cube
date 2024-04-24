using CG_Cube.Algebra;

namespace CG_Cube.Axes
{
    public class AxisZ : Axis
    {
        public AxisZ(Color color, int size = 100) : base(color, size)
        {
            VertexBuffer =
            [
                new Vector(0, 0, 0),
                new Vector(0, 0, _size)
            ];
        }

        public override void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            base.Draw(graphics, vertexBuffer);

            Font font = new("Arial", 10);
            PointF point = new(vertexBuffer[1].X, vertexBuffer[1].Y);
            graphics.DrawString("Z", font, Brushes.Blue, point);
        }
    }
}
