using CG_Cube.Algebra;

namespace CG_Cube.Axes
{
    public class AxisY
    {
        private int _size;

        public List<Vector2D> VertexBuffer;

        public AxisY(int size = 100)
        {
            _size = size;
            VertexBuffer =
            [
                new Vector2D(0, 0),
                new Vector2D(0, _size)
            ];
        }

        public void Draw(Graphics graphics, List<Vector2D> vertexBuffer)
        {
            Pen pen = new(Color.Green, 2f);
            graphics.DrawLine(pen, vertexBuffer[0].X, vertexBuffer[0].Y, vertexBuffer[1].X, vertexBuffer[1].Y);

            Font font = new("Arial", 10);
            PointF point = new(vertexBuffer[1].X, vertexBuffer[1].Y);
            graphics.DrawString("Y", font, Brushes.Green, point);
        }
    }
}
