using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public class Square
    {
        private readonly Color _color;
        private readonly int _size;
        private readonly float _weight;

        public List<Vector2D> VertexBuffer;

        public Square(Color color, int size = 100, float weight = 3f)
        {
            _color = color;
            _size = size;
            _weight = weight;

            VertexBuffer =
            [
                new Vector2D(-_size, -_size),
                new Vector2D(_size, -_size),
                new Vector2D(_size, _size),
                new Vector2D(-_size, _size)
            ];
        }

        public void Draw(Graphics graphics, List<Vector2D> vertexBuffer)
        {
            Pen pen = new(_color, _weight);
            graphics.DrawLine(pen, vertexBuffer[0].X, vertexBuffer[0].Y, vertexBuffer[1].X, vertexBuffer[1].Y);
            graphics.DrawLine(pen, vertexBuffer[1].X, vertexBuffer[1].Y, vertexBuffer[2].X, vertexBuffer[2].Y);
            graphics.DrawLine(pen, vertexBuffer[2].X, vertexBuffer[2].Y, vertexBuffer[3].X, vertexBuffer[3].Y);
            graphics.DrawLine(pen, vertexBuffer[3].X, vertexBuffer[3].Y, vertexBuffer[0].X, vertexBuffer[0].Y);
        }
    }
}
