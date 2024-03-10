namespace CG_Cube.Algebra
{
    public class Matrix2D
    {
        float[,] _matrix = new float[2, 2];

        public Matrix2D(float[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix2D(float m11, float m12, float m21, float m22)
        {
            _matrix[0, 0] = m11;
            _matrix[0, 1] = m12;
            _matrix[1, 0] = m21;
            _matrix[1, 1] = m22;
        }

        public Vector2D ToVector2D()
        {
            return new Vector2D(_matrix[0, 0], _matrix[1, 0]);
        }

        public static Matrix2D operator +(Matrix2D a, Matrix2D b)
        {
            return new Matrix2D
            (
                a._matrix[0, 0] + b._matrix[0, 0],
                a._matrix[0, 1] + b._matrix[0, 1],
                a._matrix[1, 0] + b._matrix[1, 0],
                a._matrix[1, 1] + b._matrix[1, 1]
            );
        }

        public static Matrix2D operator -(Matrix2D a, Matrix2D b)
        {
            return new Matrix2D
            (
                a._matrix[0, 0] - b._matrix[0, 0],
                a._matrix[0, 1] - b._matrix[0, 1],
                a._matrix[1, 0] - b._matrix[1, 0],
                a._matrix[1, 1] - b._matrix[1, 1]
            );
        }

        public static Matrix2D operator *(Matrix2D a, float b)
        {
            return new Matrix2D
            (
                a._matrix[0, 0] * b,
                a._matrix[0, 1] * b,
                a._matrix[1, 0] * b,
                a._matrix[1, 1] * b
            );
        }

        public static Matrix2D operator *(float a, Matrix2D b)
        {
            return b * a;
        }

        public static Matrix2D operator *(Matrix2D a, Matrix2D b)
        {
            return new Matrix2D
            (
                a._matrix[0, 0] * b._matrix[0, 0] + a._matrix[0, 1] * b._matrix[1, 0],
                a._matrix[0, 0] * b._matrix[0, 1] + a._matrix[0, 1] * b._matrix[1, 1],
                a._matrix[1, 0] * b._matrix[0, 0] + a._matrix[1, 1] * b._matrix[1, 0],
                a._matrix[1, 0] * b._matrix[0, 1] + a._matrix[1, 1] * b._matrix[1, 1]
            );
        }

        public static Vector2D operator *(Matrix2D a, Vector2D b)
        {
            return new Vector2D
            (
                a._matrix[0, 0] * b.X + a._matrix[0, 1] * b.Y,
                a._matrix[1, 0] * b.X + a._matrix[1, 1] * b.Y
            );
        }

        public static Vector2D operator *(Vector2D a, Matrix2D b)
        {
            return b * a;
        }

        public static Matrix2D operator /(Matrix2D a, float b)
        {
            return new Matrix2D
            (
                a._matrix[0, 0] / b,
                a._matrix[0, 1] / b,
                a._matrix[1, 0] / b,
                a._matrix[1, 1] / b
            );
        }

        public static Matrix2D operator /(float a, Matrix2D b)
        {
            return b / a;
        }

        public static Matrix2D operator /(Matrix2D a, Matrix2D b)
        {
            return new Matrix2D
            (
                a._matrix[0, 0] / b._matrix[0, 0],
                a._matrix[0, 1] / b._matrix[0, 1],
                a._matrix[1, 0] / b._matrix[1, 0],
                a._matrix[1, 1] / b._matrix[1, 1]
            );
        }

        public static Matrix2D Identity()
        {
            return new Matrix2D
            (
                1, 0,
                0, 1
            );
        }
    }
}
