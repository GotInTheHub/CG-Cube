namespace CG_Cube.Algebra
{
    public class Matrix
    {
        private readonly float[,] _matrix = new float[4, 4];

        public Matrix()
        {
            _matrix = new float[4, 4]
            {
                { 1.0f, 0.0f, 0.0f, 0.0f },
                { 0.0f, 1.0f, 0.0f, 0.0f },
                { 0.0f, 0.0f, 1.0f, 0.0f },
                { 0.0f, 0.0f, 0.0f, 1.0f }
            };
        }
        public Matrix(float[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            _matrix = new float[4, 4]
            {
                { m11, m12, m13, m14 },
                { m21, m22, m23, m24 },
                { m31, m32, m33, m34 },
                { m41, m42, m43, m44 }
            };
        }

        public Matrix(Vector vector)
        {
            _matrix = new float[4, 4]
            {
                { 1.0f, 0.0f, 0.0f, vector.X },
                { 0.0f, 1.0f, 0.0f, vector.Y },
                { 0.0f, 0.0f, 1.0f, vector.Z },
                { 0.0f, 0.0f, 0.0f, vector.W }
            };
        }

        public Vector ToVector()
        {
            return new Vector(_matrix[0, 3], _matrix[1, 3], _matrix[2, 3], _matrix[3, 3]);
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            return new Matrix
            (
                a._matrix[0, 0] + b._matrix[0, 0], a._matrix[0, 1] + b._matrix[0, 1], a._matrix[0, 2] + b._matrix[0, 2], a._matrix[0, 3] + b._matrix[0, 3],
                a._matrix[1, 0] + b._matrix[1, 0], a._matrix[1, 1] + b._matrix[1, 1], a._matrix[1, 2] + b._matrix[1, 2], a._matrix[1, 3] + b._matrix[1, 3],
                a._matrix[2, 0] + b._matrix[2, 0], a._matrix[2, 1] + b._matrix[2, 1], a._matrix[2, 2] + b._matrix[2, 2], a._matrix[2, 3] + b._matrix[2, 3],
                a._matrix[3, 0] + b._matrix[3, 0], a._matrix[3, 1] + b._matrix[3, 1], a._matrix[3, 2] + b._matrix[3, 2], a._matrix[3, 3] + b._matrix[3, 3]
            );
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            return new Matrix
            (
                a._matrix[0, 0] - b._matrix[0, 0], a._matrix[0, 1] - b._matrix[0, 1], a._matrix[0, 2] - b._matrix[0, 2], a._matrix[0, 3] - b._matrix[0, 3],
                a._matrix[1, 0] - b._matrix[1, 0], a._matrix[1, 1] - b._matrix[1, 1], a._matrix[1, 2] - b._matrix[1, 2], a._matrix[1, 3] - b._matrix[1, 3],
                a._matrix[2, 0] - b._matrix[2, 0], a._matrix[2, 1] - b._matrix[2, 1], a._matrix[2, 2] - b._matrix[2, 2], a._matrix[2, 3] - b._matrix[2, 3],
                a._matrix[3, 0] - b._matrix[3, 0], a._matrix[3, 1] - b._matrix[3, 1], a._matrix[3, 2] - b._matrix[3, 2], a._matrix[3, 3] - b._matrix[3, 3]
            );
        }

        public static Matrix operator *(Matrix a, float b)
        {
            return new Matrix
            (
                a._matrix[0, 0] * b, a._matrix[0, 1] * b, a._matrix[0, 2] * b, a._matrix[0, 3] * b,
                a._matrix[1, 0] * b, a._matrix[1, 1] * b, a._matrix[1, 2] * b, a._matrix[1, 3] * b,
                a._matrix[2, 0] * b, a._matrix[2, 1] * b, a._matrix[2, 2] * b, a._matrix[2, 3] * b,
                a._matrix[3, 0] * b, a._matrix[3, 1] * b, a._matrix[3, 2] * b, a._matrix[3, 3] * b
            );
        }

        public static Matrix operator *(float a, Matrix b)
        {
            return b * a;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            return new Matrix
            (
                a._matrix[0, 0] * b._matrix[0, 0] + a._matrix[0, 1] * b._matrix[1, 0] + a._matrix[0, 2] * b._matrix[2, 0] + a._matrix[0, 3] * b._matrix[3, 0],
                a._matrix[0, 0] * b._matrix[0, 1] + a._matrix[0, 1] * b._matrix[1, 1] + a._matrix[0, 2] * b._matrix[2, 1] + a._matrix[0, 3] * b._matrix[3, 1],
                a._matrix[0, 0] * b._matrix[0, 2] + a._matrix[0, 1] * b._matrix[1, 2] + a._matrix[0, 2] * b._matrix[2, 2] + a._matrix[0, 3] * b._matrix[3, 2],
                a._matrix[0, 0] * b._matrix[0, 3] + a._matrix[0, 1] * b._matrix[1, 3] + a._matrix[0, 2] * b._matrix[2, 3] + a._matrix[0, 3] * b._matrix[3, 3],

                a._matrix[1, 0] * b._matrix[0, 0] + a._matrix[1, 1] * b._matrix[1, 0] + a._matrix[1, 2] * b._matrix[2, 0] + a._matrix[1, 3] * b._matrix[3, 0],
                a._matrix[1, 0] * b._matrix[0, 1] + a._matrix[1, 1] * b._matrix[1, 1] + a._matrix[1, 2] * b._matrix[2, 1] + a._matrix[1, 3] * b._matrix[3, 1],
                a._matrix[1, 0] * b._matrix[0, 2] + a._matrix[1, 1] * b._matrix[1, 2] + a._matrix[1, 2] * b._matrix[2, 2] + a._matrix[1, 3] * b._matrix[3, 2],
                a._matrix[1, 0] * b._matrix[0, 3] + a._matrix[1, 1] * b._matrix[1, 3] + a._matrix[1, 2] * b._matrix[2, 3] + a._matrix[1, 3] * b._matrix[3, 3],

                a._matrix[2, 0] * b._matrix[0, 0] + a._matrix[2, 1] * b._matrix[1, 0] + a._matrix[2, 2] * b._matrix[2, 0] + a._matrix[2, 3] * b._matrix[3, 0],
                a._matrix[2, 0] * b._matrix[0, 1] + a._matrix[2, 1] * b._matrix[1, 1] + a._matrix[2, 2] * b._matrix[2, 1] + a._matrix[2, 3] * b._matrix[3, 1],
                a._matrix[2, 0] * b._matrix[0, 2] + a._matrix[2, 1] * b._matrix[1, 2] + a._matrix[2, 2] * b._matrix[2, 2] + a._matrix[2, 3] * b._matrix[3, 2],
                a._matrix[2, 0] * b._matrix[0, 3] + a._matrix[2, 1] * b._matrix[1, 3] + a._matrix[2, 2] * b._matrix[2, 3] + a._matrix[2, 3] * b._matrix[3, 3],

                a._matrix[3, 0] * b._matrix[0, 0] + a._matrix[3, 1] * b._matrix[1, 0] + a._matrix[3, 2] * b._matrix[2, 0] + a._matrix[3, 3] * b._matrix[3, 0],
                a._matrix[3, 0] * b._matrix[0, 1] + a._matrix[3, 1] * b._matrix[1, 1] + a._matrix[3, 2] * b._matrix[2, 1] + a._matrix[3, 3] * b._matrix[3, 1],
                a._matrix[3, 0] * b._matrix[0, 2] + a._matrix[3, 1] * b._matrix[1, 2] + a._matrix[3, 2] * b._matrix[2, 2] + a._matrix[3, 3] * b._matrix[3, 2],
                a._matrix[3, 0] * b._matrix[0, 3] + a._matrix[3, 1] * b._matrix[1, 3] + a._matrix[3, 2] * b._matrix[2, 3] + a._matrix[3, 3] * b._matrix[3, 3]
            );
        }

        public static Vector operator *(Matrix a, Vector b)
        {
            return new Vector
            (
                a._matrix[0, 0] * b.X + a._matrix[0, 1] * b.Y + a._matrix[0, 2] * b.Z + a._matrix[0, 3] * b.W,
                a._matrix[1, 0] * b.X + a._matrix[1, 1] * b.Y + a._matrix[1, 2] * b.Z + a._matrix[1, 3] * b.W,
                a._matrix[2, 0] * b.X + a._matrix[2, 1] * b.Y + a._matrix[2, 2] * b.Z + a._matrix[2, 3] * b.W,
                a._matrix[3, 0] * b.X + a._matrix[3, 1] * b.Y + a._matrix[3, 2] * b.Z + a._matrix[3, 3] * b.W
            );
        }

        public static Vector operator *(Vector a, Matrix b)
        {
            return b * a;
        }

        public static Vector3D operator *(Matrix a, Vector3D b)
        {
            return new Vector3D
            (
                a._matrix[0, 0] * b.X + a._matrix[0, 1] * b.Y + a._matrix[0, 2] * b.Z + a._matrix[0, 3],
                a._matrix[1, 0] * b.X + a._matrix[1, 1] * b.Y + a._matrix[1, 2] * b.Z + a._matrix[1, 3],
                a._matrix[2, 0] * b.X + a._matrix[2, 1] * b.Y + a._matrix[2, 2] * b.Z + a._matrix[2, 3]
            );
        }

        public static Vector3D operator *(Vector3D a, Matrix b)
        {
            return b * a;
        }

        public static Matrix Identity()
        {
            return new Matrix
            (
                1.0f, 0.0f, 0.0f, 0.0f,
                0.0f, 1.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }

        public static Matrix ScaleMatrix(float scale)
        {
            return new Matrix
            (
                scale, 0.0f, 0.0f, 0.0f,
                0.0f, scale, 0.0f, 0.0f,
                0.0f, 0.0f, scale, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
            );
        }

        /// <summary>
        /// Change base so y-axis is correct; Rotate around the x-axis
        /// </summary>
        /// <param name="angle">Angle between z-axis</param>
        /// <returns></returns>
        public static Matrix RotateX(float angle)
        {
            return new Matrix
                (
                    1.0f, 0.0f, 0.0f, 0.0f,
                    0.0f, (float)Math.Cos(angle), (float)-Math.Sin(angle), 0.0f,
                    0.0f, (float)Math.Sin(angle), (float)Math.Cos(angle), 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix RotateY(float angle)
        {
            return new Matrix
                (
                    (float)Math.Cos(angle), 0.0f, (float)Math.Sin(angle), 0.0f,
                    0.0f, 1.0f, 0.0f, 0.0f,
                    (float)-Math.Sin(angle), 0.0f, (float)Math.Cos(angle), 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        /// <summary>
        /// Change base so x-axis is correct; Rotate around z-axis untial the y-axis is towards world space, x-axis if from left-to-rigth
        /// </summary>
        /// <param name="angle">Angle between x-axis</param>
        /// <returns></returns>
        public static Matrix RotateZ(float angle)
        {
            return new Matrix
                (
                    (float)Math.Cos(angle), (float)-Math.Sin(angle), 0.0f, 0.0f,
                    (float)Math.Sin(angle), (float)Math.Cos(angle), 0.0f, 0.0f,
                    0.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        /// <summary>
        /// Translate to camera position
        /// </summary>
        /// <param name="r">Length of vector from world view to camera view</param>
        /// <param name="phi">Angle between z-axis</param>
        /// <param name="theta">Angle between the x-axis</param>
        /// <returns></returns>
        public static Matrix Translate(float r, float phi, float theta)
        {
            return new Matrix
                (
                    1, 0, 0, r * MathF.Sin(phi) * MathF.Cos(theta),
                    0, 1, 0, r * MathF.Sin(phi) * MathF.Sin(theta),
                    0, 0, 1, r * MathF.Cos(phi),
                    0, 0, 0, 1
                );
        }

        public static Matrix TranslateWithXYZ(float x, float y, float z)
        {
            return new Matrix
                (
                    1.0f, 0.0f, 0.0f, x,
                    0.0f, 1.0f, 0.0f, y,
                    0.0f, 0.0f, 1.0f, z,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix TranslateWithVector(Vector vector)
        {
            return new Matrix
                (
                    1.0f, 0.0f, 0.0f, vector.X,
                    0.0f, 1.0f, 0.0f, vector.Y,
                    0.0f, 0.0f, 1.0f, vector.Z,
                    0.0f, 0.0f, 0.0f, vector.W
                );
        }

        public static Matrix TransformationView(float r, float phi, float theta)
        {
            // theta z-axis, phi x-axis, r distance to camera
            return new Matrix
                (
                    -MathF.Sin(theta), MathF.Cos(theta), 0.0f, 0.0f,
                    -MathF.Cos(theta) * MathF.Cos(phi), -MathF.Cos(phi) * MathF.Sin(theta), MathF.Sin(phi), 0.0f,
                    MathF.Cos(theta) * MathF.Sin(phi), MathF.Sin(theta) * MathF.Sin(phi), MathF.Cos(phi), -r,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static Matrix ProjectionMatrix(float distance, Vector vector)
        {
            distance = 1 / (distance/100 - vector.Z);
            return new Matrix
                (
                    distance, 0.0f, 0.0f, 0.0f,
                    0.0f, distance, 0.0f, 0.0f,
                    0.0f, 0.0f, 1.0f, 0.0f,
                    0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        public static float RadiansToDegrees(float radians)
        {
            return 180 / MathF.PI * radians;
        }
    }
}
