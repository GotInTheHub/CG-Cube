using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public abstract class Shape
    {
        protected bool _firstMod;
        protected Tuple<Vector, Vector, float>? _startMods;

        protected readonly float _size;
        protected readonly Color color;
        protected float _scale;

        public float Enlargement { get => _scale; set => this.Scale(value); }
        public Vector Rotation { get; protected set; }
        public Vector Translation { get; protected set; }
        public bool IsAnimating { get; protected set; }

        protected List<Vector> VertexBuffer { get; set; }
        public List<Vector> Projection { get; protected set; }

        public Shape(Color color, float size = 1)
        {
            _firstMod = false;
            _size = size;
            _scale = 1;
            this.color = color;
            Rotation = new Vector();
            Translation = new Vector();
            IsAnimating = false;
            VertexBuffer = [];
            Projection = [];
        }

        public virtual void Reset()
        {
            if (!_firstMod)
            {
                Translation = new Vector();
                Rotation = new();
            }
            else
            {
                Translation = _startMods!.Item1;
                Rotation = _startMods.Item2;
                Projection = Scale(_startMods.Item3);
            }

            IsAnimating = false;
        }

        public void ToggleAnimation()
        {
            IsAnimating = !IsAnimating;
        }

        public abstract void Draw(Graphics graphics, List<Vector> vertexBuffer);

        public List<Vector> ModelTransformation(Vector translation, Vector rotation, float scale)
        {
            if (!_firstMod)
            {
                _firstMod = true;
                _startMods = Tuple.Create(translation, rotation, scale);
            }

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
            Matrix rotationMatrix = Matrix.Rotate(rotation);

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
            Matrix prevScaleMatrix = Matrix.ScaleMatrix(1.0f / _scale);
            for (int i = 0; i < Projection.Count; i++)
            {
                VertexBuffer[i] *= prevScaleMatrix * scaleMatrix;
            }

            Translate(new());
            _scale = scale;

            return Projection;
        }

        private static List<Vector> Transform(List<Vector> vertexBuffer, float r, float theta, float phi)
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

        public static List<Vector> Project(List<Vector> vertexBuffer, float r, float theta, float phi, float d)
        {
            vertexBuffer = Transform(vertexBuffer, r, theta, phi);

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] *= Matrix.ProjectionMatrix(d, vertexBuffer[i]);
            }

            return vertexBuffer;
        }

        public override string ToString()
        {
            return "Shape";
        }
    }
}
