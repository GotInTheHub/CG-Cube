namespace CG_Cube.Algebra
{
    public class Vector4D(float x, float y, float z, float w = 1)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Z { get; set; } = z;
        public float W { get; set; } = w;

        public Vector4D() : this(0, 0, 0) { }

        public Vector4D(Vector4D vector) : this(vector.X, vector.Y, vector.Z) { }

        public Vector4D(Vector4D vector, float w) : this(vector.X, vector.Y, vector.Z, w) { }

        public static Vector4D operator +(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector4D operator -(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector4D operator *(Vector4D a, float b)
        {
            return new Vector4D(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector4D operator *(float a, Vector4D b)
        {
            return b * a;
        }

        public static Vector4D operator *(Vector4D a, Vector4D b)
        {
            return new Vector4D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector4D operator /(Vector4D a, float b)
        {
            if (b == 0)
                throw new ArgumentException("b cannot be 0");

            return new Vector4D(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3D operator /(Vector4D a, Vector4D b)
        {
            if (a.W == 0 || b.W == 0)
                throw new ArgumentException("W cannot be 0");

            return new Vector3D(a.X / a.W, a.Y / a.W, a.Z / a.W)
                   / new Vector3D(b.X / b.W, b.Y / b.W, b.Z / b.W);
        }

        public static float Dot(Vector4D a, Vector4D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;
        }

        public static Vector4D Cross(Vector4D a, Vector4D b)
        {
            return new Vector4D
            (
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static float Magnitude(Vector4D a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z + a.W * a.W);
        }

        public static Vector4D Normalize(Vector4D a)
        {
            return a / Magnitude(a);
        }

        public static Vector3D ToCartesian(Vector4D a)
        {
            if (a.W == 0)
                throw new ArgumentException("W cannot be 0");

            return new Vector3D(a.X / a.W, a.Y / a.W, a.Z / a.W);
        }

        public Vector4D Clone()
        {
            return new Vector4D(X, Y, Z, W);
        }
    }
}
