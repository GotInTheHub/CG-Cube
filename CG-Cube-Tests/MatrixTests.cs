using CG_Cube.Algebra;
namespace CG_Cube_Tests
{
    public class MatrixTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMatrixMultiplication()
        {
            var matrix1 = new Matrix
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            var matrix2 = new Matrix
            (
                17, 18, 19, 20,
                21, 22, 23, 24,
                25, 26, 27, 28,
                29, 30, 31, 32
            );

            var result = matrix1 * matrix2;

            Assert.Multiple(() =>
            {
                Assert.That(result.matrix[0, 0], Is.EqualTo(250));
                Assert.That(result.matrix[0, 1], Is.EqualTo(260));
                Assert.That(result.matrix[0, 2], Is.EqualTo(270));
                Assert.That(result.matrix[0, 3], Is.EqualTo(280));
                Assert.That(result.matrix[1, 0], Is.EqualTo(618));
                Assert.That(result.matrix[1, 1], Is.EqualTo(644));
                Assert.That(result.matrix[1, 2], Is.EqualTo(670));
                Assert.That(result.matrix[1, 3], Is.EqualTo(696));
                Assert.That(result.matrix[2, 0], Is.EqualTo(986));
                Assert.That(result.matrix[2, 1], Is.EqualTo(1028));
                Assert.That(result.matrix[2, 2], Is.EqualTo(1070));
                Assert.That(result.matrix[2, 3], Is.EqualTo(1112));
                Assert.That(result.matrix[3, 0], Is.EqualTo(1354));
                Assert.That(result.matrix[3, 1], Is.EqualTo(1412));
                Assert.That(result.matrix[3, 2], Is.EqualTo(1470));
                Assert.That(result.matrix[3, 3], Is.EqualTo(1528));
            });
        }

        [Test]
        public void MatrixFromVector()
        {
            Vector vector = new(1, 2, 3, 4);
            Matrix matrix = new(vector);

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(1));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(2));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(3));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(4));
            });
        }

        [Test]
        public void MatrixToVector()
        {
            Matrix matrix = new
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Assert.Multiple(() =>
            {
                Assert.That(matrix.ToVector().X, Is.EqualTo(4));
                Assert.That(matrix.ToVector().Y, Is.EqualTo(8));
                Assert.That(matrix.ToVector().Z, Is.EqualTo(12));
                Assert.That(matrix.ToVector().W, Is.EqualTo(16));
            });
        }

        [Test]
        public void MatrixAddition()
        {
            Matrix matrix1 = new
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Matrix matrix2 = new
            (
                17, 18, 19, 20,
                21, 22, 23, 24,
                25, 26, 27, 28,
                29, 30, 31, 32
            );

            Matrix result = matrix1 + matrix2;

            Assert.Multiple(() =>
            {
                Assert.That(result.matrix[0, 0], Is.EqualTo(18));
                Assert.That(result.matrix[0, 1], Is.EqualTo(20));
                Assert.That(result.matrix[0, 2], Is.EqualTo(22));
                Assert.That(result.matrix[0, 3], Is.EqualTo(24));
                Assert.That(result.matrix[1, 0], Is.EqualTo(26));
                Assert.That(result.matrix[1, 1], Is.EqualTo(28));
                Assert.That(result.matrix[1, 2], Is.EqualTo(30));
                Assert.That(result.matrix[1, 3], Is.EqualTo(32));
                Assert.That(result.matrix[2, 0], Is.EqualTo(34));
                Assert.That(result.matrix[2, 1], Is.EqualTo(36));
                Assert.That(result.matrix[2, 2], Is.EqualTo(38));
                Assert.That(result.matrix[2, 3], Is.EqualTo(40));
                Assert.That(result.matrix[3, 0], Is.EqualTo(42));
                Assert.That(result.matrix[3, 1], Is.EqualTo(44));
                Assert.That(result.matrix[3, 2], Is.EqualTo(46));
                Assert.That(result.matrix[3, 3], Is.EqualTo(48));
            });
        }

        [Test]
        public void MatrixSubtraction()
        {
            Matrix matrix1 = new
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Matrix matrix2 = new
            (
                17, 18, 19, 20,
                21, 22, 23, 24,
                25, 26, 27, 28,
                29, 30, 31, 32
            );

            Matrix result = matrix1 - matrix2;

            Assert.Multiple(() =>
            {
                Assert.That(result.matrix[0, 0], Is.EqualTo(-16));
                Assert.That(result.matrix[0, 1], Is.EqualTo(-16));
                Assert.That(result.matrix[0, 2], Is.EqualTo(-16));
                Assert.That(result.matrix[0, 3], Is.EqualTo(-16));
                Assert.That(result.matrix[1, 0], Is.EqualTo(-16));
                Assert.That(result.matrix[1, 1], Is.EqualTo(-16));
                Assert.That(result.matrix[1, 2], Is.EqualTo(-16));
                Assert.That(result.matrix[1, 3], Is.EqualTo(-16));
                Assert.That(result.matrix[2, 0], Is.EqualTo(-16));
                Assert.That(result.matrix[2, 1], Is.EqualTo(-16));
                Assert.That(result.matrix[2, 2], Is.EqualTo(-16));
                Assert.That(result.matrix[2, 3], Is.EqualTo(-16));
                Assert.That(result.matrix[3, 0], Is.EqualTo(-16));
                Assert.That(result.matrix[3, 1], Is.EqualTo(-16));
                Assert.That(result.matrix[3, 2], Is.EqualTo(-16));
                Assert.That(result.matrix[3, 3], Is.EqualTo(-16));
            });
        }

        [Test]
        public void MatrixMultiplicationByScalar()
        {
            Matrix matrix = new
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Matrix result = matrix * 2;

            Assert.Multiple(() =>
            {
                Assert.That(result.matrix[0, 0], Is.EqualTo(2));
                Assert.That(result.matrix[0, 1], Is.EqualTo(4));
                Assert.That(result.matrix[0, 2], Is.EqualTo(6));
                Assert.That(result.matrix[0, 3], Is.EqualTo(8));
                Assert.That(result.matrix[1, 0], Is.EqualTo(10));
                Assert.That(result.matrix[1, 1], Is.EqualTo(12));
                Assert.That(result.matrix[1, 2], Is.EqualTo(14));
                Assert.That(result.matrix[1, 3], Is.EqualTo(16));
                Assert.That(result.matrix[2, 0], Is.EqualTo(18));
                Assert.That(result.matrix[2, 1], Is.EqualTo(20));
                Assert.That(result.matrix[2, 2], Is.EqualTo(22));
                Assert.That(result.matrix[2, 3], Is.EqualTo(24));
                Assert.That(result.matrix[3, 0], Is.EqualTo(26));
                Assert.That(result.matrix[3, 1], Is.EqualTo(28));
                Assert.That(result.matrix[3, 2], Is.EqualTo(30));
                Assert.That(result.matrix[3, 3], Is.EqualTo(32));
            });
        }

        [Test]
        public void ScalarMultiplicationrByMatrix()
        {
            Matrix matrix = new
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Matrix result = 2 * matrix;

            Assert.Multiple(() =>
            {
                Assert.That(result.matrix[0, 0], Is.EqualTo(2));
                Assert.That(result.matrix[0, 1], Is.EqualTo(4));
                Assert.That(result.matrix[0, 2], Is.EqualTo(6));
                Assert.That(result.matrix[0, 3], Is.EqualTo(8));
                Assert.That(result.matrix[1, 0], Is.EqualTo(10));
                Assert.That(result.matrix[1, 1], Is.EqualTo(12));
                Assert.That(result.matrix[1, 2], Is.EqualTo(14));
                Assert.That(result.matrix[1, 3], Is.EqualTo(16));
                Assert.That(result.matrix[2, 0], Is.EqualTo(18));
                Assert.That(result.matrix[2, 1], Is.EqualTo(20));
                Assert.That(result.matrix[2, 2], Is.EqualTo(22));
                Assert.That(result.matrix[2, 3], Is.EqualTo(24));
                Assert.That(result.matrix[3, 0], Is.EqualTo(26));
                Assert.That(result.matrix[3, 1], Is.EqualTo(28));
                Assert.That(result.matrix[3, 2], Is.EqualTo(30));
                Assert.That(result.matrix[3, 3], Is.EqualTo(32));
            });
        }

        [Test]
        public void MatrixMultiplicationByVector()
        {
            Matrix matrix = new
            (
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12,
                13, 14, 15, 16
            );

            Vector vector = new(1, 2, 3, 4);

            Vector result = matrix * vector;

            Assert.Multiple(() =>
            {
                Assert.That(result.X, Is.EqualTo(30));
                Assert.That(result.Y, Is.EqualTo(70));
                Assert.That(result.Z, Is.EqualTo(110));
                Assert.That(result.W, Is.EqualTo(150));
            });
        }

        [Test]
        public void MatrixIdentity()
        {
            Matrix matrix = Matrix.Identity();

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(1));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(1));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(1));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void ScaleMatrix()
        {
            Matrix matrix = Matrix.ScaleMatrix(5);

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(5));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(5));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(5));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void RotateX()
        {
            Matrix matrix = Matrix.RotateX(0.25f * MathF.PI);

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(1));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(-0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void RotateY()
        {
            Matrix matrix = Matrix.RotateY(0.25f * MathF.PI);

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(1));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(-0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void RotateZ()
        {
            Matrix matrix = Matrix.RotateZ(0.25f * MathF.PI);

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(-0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(0.707106769f).Within(0.000001f));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(1));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void TranslateMatrix()
        {
            Matrix matrix = Matrix.Translate(new(1, 2, 3));

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(1));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(1));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(1));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(2));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(1));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(3));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void MatrixTransformationProjection()
        {
            const float d = 800;
            Vector vector = new(1, 2, 3, 4);

            Matrix matrix = Matrix.ProjectionMatrix(d, vector);

            Assert.Multiple(() =>
            {
                Assert.That(matrix.matrix[0, 0], Is.EqualTo(-266.666656).Within(0.00001f));
                Assert.That(matrix.matrix[0, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[0, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 1], Is.EqualTo(-266.666656).Within(0.00001f));
                Assert.That(matrix.matrix[1, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[1, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[2, 2], Is.EqualTo(1));
                Assert.That(matrix.matrix[2, 3], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 0], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 1], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 2], Is.EqualTo(0));
                Assert.That(matrix.matrix[3, 3], Is.EqualTo(1));
            });
        }

        [Test]
        public void RadiansToDegrees()
        {
            float radians = 0.25f * MathF.PI;
            float degrees = Matrix.RadiansToDegrees(radians);

            Assert.That(degrees, Is.EqualTo(45));
        }

        [Test]
        public void DegreesToRadians()
        {
            float degrees = 45;
            float radians = Matrix.DegreesToRadians(degrees);

            Assert.That(radians, Is.EqualTo(0.25f * MathF.PI));
        }
    }
}