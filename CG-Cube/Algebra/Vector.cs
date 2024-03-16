namespace CG_Cube.Algebra
{
    public class Vector(float x, float y, float z = 0, float w = 1)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Z { get; set; } = z;
        public float W { get; set; } = w;

        public Vector() : this(0, 0, 0) { }

        public Vector(Vector vector) : this(vector.X, vector.Y, vector.Z, vector.W) { }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector operator *(float a, Vector b)
        {
            return b * a;
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector operator /(Vector a, float b)
        {
            if (b == 0)
                throw new ArgumentException("b cannot be 0");

            return new Vector(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3D operator /(Vector a, Vector b)
        {
            if (a.W == 0 || b.W == 0)
                throw new ArgumentException("W cannot be 0");

            return new Vector3D(a.X / a.W, a.Y / a.W, a.Z / a.W)
                   / new Vector3D(b.X / b.W, b.Y / b.W, b.Z / b.W);
        }

        public static float Dot(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public static Vector Cross(Vector a, Vector b)
        {
            return new Vector
            (
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static float Magnitude(Vector a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z + a.W * a.W);
        }

        public static Vector Normalize(Vector a)
        {
            return a / Magnitude(a);
        }

        public static Vector3D ToCartesian(Vector a)
        {
            if (a.W == 0)
                throw new ArgumentException("W cannot be 0");

            return new Vector3D(a.X / a.W, a.Y / a.W, a.Z / a.W);
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z}, {W})";
        }

        public Vector Clone()
        {
            return new Vector(X, Y, Z, W);
        }
    }
}
