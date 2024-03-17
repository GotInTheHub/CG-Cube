using CG_Cube.Algebra;

namespace CG_Cube_Tests
{
    public class VectorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void VectorAddition()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(4, 5, 6);
            var c = a + b;
            Assert.Multiple(() =>
            {
                Assert.That(c.X, Is.EqualTo(5));
                Assert.That(c.Y, Is.EqualTo(7));
                Assert.That(c.Z, Is.EqualTo(9));
            });
        }

        [Test]
        public void VectorSubtraction()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(4, 5, 6);
            var c = a - b;
            Assert.Multiple(() =>
            {
                Assert.That(c.X, Is.EqualTo(-3));
                Assert.That(c.Y, Is.EqualTo(-3));
                Assert.That(c.Z, Is.EqualTo(-3));
            });
        }

        [Test]
        public void VectorScalarMultiplication()
        {
            var a = new Vector(1, 2, 3);
            var b = 2;
            var c = a * b;
            Assert.Multiple(() =>
            {
                Assert.That(c.X, Is.EqualTo(2));
                Assert.That(c.Y, Is.EqualTo(4));
                Assert.That(c.Z, Is.EqualTo(6));
            });
        }

        [Test]
        public void VectorScalarDivision()
        {
            var a = new Vector(1, 2, 3);
            var b = 2;
            var c = a / b;
            Assert.Multiple(() =>
            {
                Assert.That(c.X, Is.EqualTo(0.5));
                Assert.That(c.Y, Is.EqualTo(1));
                Assert.That(c.Z, Is.EqualTo(1.5));
            });
        }

        [Test]
        public void VectorDotProduct()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(4, 5, 6);
            var c = Vector.Dot(a, b);
            Assert.That(c, Is.EqualTo(33f));
        }

        [Test]
        public void VectorCrossProduct()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(4, 5, 6);
            var c = Vector.Cross(a, b);
            Assert.Multiple(() =>
            {
                Assert.That(c.X, Is.EqualTo(-3));
                Assert.That(c.Y, Is.EqualTo(6));
                Assert.That(c.Z, Is.EqualTo(-3));
            });
        }

        [Test]
        public void VectorEquality()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(1, 2, 3);
            Assert.Multiple(() =>
            {
                Assert.That(b.X, Is.EqualTo(a.X));
                Assert.That(b.Y, Is.EqualTo(a.Y));
                Assert.That(b.Z, Is.EqualTo(a.Z));
            });
        }

        [Test]
        public void VectorInequality()
        {
            var a = new Vector(1, 2, 3);
            var b = new Vector(4, 5, 6);
            Assert.That(a, Is.Not.EqualTo(b));
        }

        [Test]
        public void VectorToString()
        {
            var a = new Vector(1, 2, 3);
            var b = a.ToString();
            Assert.That(b, Is.EqualTo("(1, 2, 3, 1)"));
        }

        [Test]
        public void VectorMagnitude()
        {
            var a = new Vector(1, 2, 3);
            var b = Vector.Magnitude(a);
            Assert.That(b, Is.EqualTo(3.74).Within(0.3f));
        }

        [Test]
        public void VectorNormalization()
        {
            var a = new Vector(1, 2, 3);
            var b = Vector.Normalize(a);
            Assert.Multiple(() =>
            {
                Assert.That(b.X, Is.EqualTo(0.26726).Within(0.1f));
                Assert.That(b.Y, Is.EqualTo(0.53452).Within(0.1f));
                Assert.That(b.Z, Is.EqualTo(0.80178).Within(0.1f));
            });
        }

        [Test]
        public void VectorClone()
        {
            var a = new Vector(1, 2, 3);
            var b = a.Clone();
            Assert.Multiple(() =>
            {
                Assert.That(b.X, Is.EqualTo(a.X));
                Assert.That(b.Y, Is.EqualTo(a.Y));
                Assert.That(b.Z, Is.EqualTo(a.Z));
            });
        }
    }
}
