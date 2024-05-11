using CG_Cube.Algebra;

namespace CG_Cube.Axes
{
    public class AxisY : Axis
    {
        public AxisY(Color color, float size = 3) : base(color, size)
        {
            VertexBuffer =
            [
                new Vector(0, 0, 0),
                new Vector(0, _size, 0)
            ];
            Projection = new(VertexBuffer);
        }

        public override void Reset()
        {
            AxisY temp = new(color, _size);
            VertexBuffer = temp.VertexBuffer;
            Projection = temp.Projection;
            base.Reset();
        }

        public override void Draw(Graphics graphics, List<Vector> vertexBuffer)
        {
            base.Draw(graphics, vertexBuffer);

            Font font = new("Arial", 10);
            PointF point = new(vertexBuffer[1].X, vertexBuffer[1].Y);
            graphics.DrawString("Y", font, Brushes.Green, point);
        }

        public override string ToString()
        {
            return "AxisY";
        }
    }
}
