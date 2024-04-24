using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public abstract class Shape
    {
        protected readonly int _size;
        protected readonly Color color;

        public float Distance { get; protected set; }
        public float Theta { get; protected set; }
        public float Phi { get; protected set; }
        public float R { get; protected set; }

        public float AngleX { get; protected set; }
        public float AngleY { get; protected set; }
        public float AngleZ { get; protected set; }

        public bool IsAnimating { get; protected set; }

        public List<Vector> VertexBuffer { get; set; }

        public Shape(Color color, int size = 100)
        {
            _size = size;
            VertexBuffer = [];
            this.color = color;
            IsAnimating = false;
        }

        public virtual void ToggleAnimation(bool animate = false)
        {
            IsAnimating = animate;
        }

        public virtual void SetRotationTransformationVars(float d, float theta, float phi, float r)
        {
            Distance = d;
            Theta = theta;
            Phi = phi;
            R = r;
        }

        public abstract void Draw(Graphics graphics, List<Vector> vertexBuffer);

        // model transformation (translation -> rotation -> scale) -> projection transformation -> viewport

        public List<Vector> ModelTransformation(List<Vector> vertexBuffer, Vector translation, Vector rotation, float scale)
        {
            List<Vector> clone = new(vertexBuffer);

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                Translate(clone, translation);
                Rotate(clone, rotation);
                Scale(scale, clone);
                Project(clone);
            }

            return clone;
        }

        public static List<Vector> Scale(float scale, List<Vector> vertexBuffer)
        {
            List<Vector> clone = new(vertexBuffer);

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                clone[i] *= scale;
            }

            return clone;
        }

        public List<Vector> Rotate(List<Vector> vertexBuffer, Vector rotation)
        {
            List<Vector> clone = new(vertexBuffer);
            Matrix rotationMatrix = Matrix.Rotate(rotation);

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                clone[i] *= rotationMatrix;
            }

            AngleX += rotation.X;
            AngleY += rotation.Y;
            AngleZ += rotation.Z;

            return clone;
        }

        public List<Vector> Transform(List<Vector> vertexBuffer, float r, float theta, float phi)
        {
            SetRotationTransformationVars(Distance, r, theta, phi);

            List<Vector> clone = new(vertexBuffer);
            Matrix transformationMatrix = Matrix.Transformation(R, Theta, Phi);

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                clone[i] *= transformationMatrix;
            }

            return clone;
        }

        public static List<Vector> Translate(List<Vector> vertexBuffer, Vector translation)
        {
            List<Vector> clone = new(vertexBuffer);
            Matrix translationMatrix = Matrix.Translate(translation);

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                clone[i] *= translationMatrix;
            }

            return clone;
        }

        public List<Vector> Project(List<Vector> vertexBuffer)
        {
            List<Vector> result = [];

            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                result.Add(vertexBuffer[i] * Matrix.ProjectionMatrix(Distance, vertexBuffer[i]));
            }

            return result;
        }
    }
}
