namespace CG_Cube.Algebra
{
    public class Vector3D(float x, float y, float z)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public float Z { get; set; } = z;

        public Vector3D() : this(0, 0, 0) { }

        public Vector3D(Vector3D vector) : this(vector.X, vector.Y, vector.Z) { }

        public static Vector3D operator +(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3D operator -(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3D operator *(Vector3D a, float b)
        {
            return new Vector3D(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3D operator *(float a, Vector3D b)
        {
            return b * a;
        }

        public static Vector3D operator *(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        }

        public static Vector3D operator /(Vector3D a, float b)
        {
            return new Vector3D(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3D operator /(Vector3D a, Vector3D b)
        {
            return new Vector3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }

        public static float Dot(Vector3D a, Vector3D b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static Vector3D Cross(Vector3D a, Vector3D b)
        {
            return new Vector3D
            (
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        public static float Magnitude(Vector3D a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
        }

        public static Vector3D Normalize(Vector3D a)
        {
            return a / Magnitude(a);
        }

        public Vector3D Clone()
        {
            return new Vector3D(this);
        }
    }
}
