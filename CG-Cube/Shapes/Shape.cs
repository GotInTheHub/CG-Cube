using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public abstract class Shape
    {
        protected readonly int _size;
        protected readonly Color color;

        public Vector Rotation { get; protected set; }
        public Vector Translation { get; protected set; }
        public bool IsAnimating { get; protected set; }

        protected List<Vector> VertexBuffer { get; set; }
        public List<Vector> Projection { get; protected set; }

        public Shape(Color color, int size = 100)
        {
            _size = size;
            this.color = color;
            Rotation = new Vector();
            Translation = new Vector();
            IsAnimating = false;
            VertexBuffer = [];
            Projection = [];
        }

        public virtual void Reset()
        {
            Rotation = new();
            Translation = new Vector();
            IsAnimating = false;
        }
        public virtual void ToggleAnimation(bool animate = false)
        {
            IsAnimating = animate;
        }

        public abstract void Draw(Graphics graphics, List<Vector> vertexBuffer);

        public List<Vector> ModelTransformation(Vector translation, Vector rotation, float scale)
        {
            Projection = Translate(translation);
            Projection = Rotate(rotation);
            Projection = Scale(scale);

            return Projection;
        }

        public List<Vector> Translate(Vector translation)
        {
            Translation += translation;
            Matrix translationMatrix = Matrix.Translate(Translation);

            for (int i = 0; i < Projection.Count; i++)
            {
                Projection[i] = VertexBuffer[i] * translationMatrix;
            }

            return Projection;
        }

        public List<Vector> Rotate(Vector rotation)
        {
            Rotation += rotation;
            Matrix rotationMatrix = Matrix.Rotate(Rotation);

            for (int i = 0; i < VertexBuffer.Count; i++)
            {
                VertexBuffer[i] *= rotationMatrix;
            }

            Translate(new());

            return Projection;
        }

        public List<Vector> Scale(float scale)
        {
            Matrix scaleMatrix = Matrix.ScaleMatrix(scale);

            for (int i = 0; i < Projection.Count; i++)
            {
                Projection[i] = VertexBuffer[i] * scaleMatrix;
            }

            return Projection;
        }

        public static List<Vector> Transform(List<Vector> vertexBuffer, float r, float theta, float phi)
        {
            // theta = z, phi = x, r = y
            List<Vector> clone = vertexBuffer.ConvertAll(vector => vector.Clone());
            Matrix transformationMatrix = Matrix.ViewMatrix(r, theta, phi);

            for (int i = 0; i < clone.Count; i++)
            {
                clone[i] *= transformationMatrix;
            }

            return clone;
        }
    }
}
