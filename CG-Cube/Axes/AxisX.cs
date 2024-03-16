
using CG_Cube.Algebra;

namespace CG_Cube.Axes
{
    public class AxisX
    {
        private readonly int _size;

        public List<Vector> VertexBuffer;

        public AxisX(int size = 100)
        {
            _size = size;
            VertexBuffer =
            [
                new Vector(0, 0, 0),
                new Vector(_size, 0, 0)
            ];
        }

        public static void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            Pen pen = new(Color.Red, 2f);
            graphics.DrawLine(pen, vertexBuffer[0].X, vertexBuffer[0].Y, vertexBuffer[1].X, vertexBuffer[1].Y);

            Font font = new("Arial", 10);
            PointF point = new(vertexBuffer[1].X, vertexBuffer[1].Y);
            graphics.DrawString("X", font, Brushes.Red, point);
        }
    }
}
