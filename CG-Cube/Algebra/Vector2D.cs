namespace CG_Cube.Algebra
{
    public class Vector2D(float x, float y)
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;

        public Vector2D() : this(0, 0) { }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2D operator *(Vector2D a, float b)
        {
            return new Vector2D(a.X * b, a.Y * b);
        }

        public static Vector2D operator *(float a, Vector2D b)
        {
            return b * a;
        }

        public static Vector2D operator *(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2D operator /(Vector2D a, float b)
        {
            return new Vector2D(a.X / b, a.Y / b);
        }

        public static Vector2D operator /(Vector2D a, Vector2D b)
        {
            return new Vector2D(a.X / b.X, a.Y / b.Y);
        }

        public static float Dot(Vector2D a, Vector2D b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Cross(Vector2D a, Vector2D b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static float Magnitude(Vector2D a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static Vector2D Normalize(Vector2D a)
        {
            return a / Magnitude(a);
        }

        public Vector2D Clone()
        {
            return new Vector2D(X, Y);
        }
    }
}
