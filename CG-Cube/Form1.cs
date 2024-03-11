using CG_Cube.Algebra;
using CG_Cube.Axes;
using MatrixTransformations;
using System.Timers;

namespace CG_Cube
{
    public partial class Form1 : Form
    {
        // Window dimensions
        const int WIDTH = 1920;
        const int HEIGHT = 1080;
        readonly Vector2D CENTER = new(WIDTH * 0.5f, HEIGHT * 0.5f);
        // Axes
        readonly AxisX x_axis;
        readonly AxisY y_axis;

        // Objects
        Cube cube;
        List<Vector4D> cubeCache;

        // Tools
        System.Timers.Timer timer = new();
        bool isRotating = false;
        private Dictionary<Keys, float> rotationAngles = new()
        {
            { Keys.Q, 0.0f },
            { Keys.E, 0.0f },
            { Keys.A, 0.0f },
            { Keys.D, 0.0f },
            { Keys.W, 0.0f },
            { Keys.S, 0.0f }
        };

        public Form1()
        {
            InitializeComponent();

            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            Vector2D v1 = new Vector2D();
            Console.WriteLine(v1);
            Vector2D v2 = new Vector2D(1, 2);
            Console.WriteLine(v2);
            Vector2D v3 = new Vector2D(2, 6);
            Console.WriteLine(v3);
            Vector2D v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            Matrix2D m1 = Matrix2D.Identity();
            Console.WriteLine(m1); // 1, 0, 0, 1
            Matrix2D m2 = new Matrix2D(2, 4, -1, 3);
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16

            // Timer
            timer.Interval = 100;
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create objects
            cube = new Cube(Color.Red);
            cubeCache = cube.VertexBuffer.ConvertAll(vector => vector.Clone());

            // Transformations
            Scale(100, cube.VertexBuffer);
            Scale(100, cubeCache);

            // Change location
            ChangeLocation(WIDTH, HEIGHT, cube.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, x_axis.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, y_axis.VertexBuffer);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isRotating)
            {
                RotateX(0.1f, cubeCache);
                RotateY(0.1f, cubeCache);
                RotateZ(0.1f, cubeCache);
                cube.VertexBuffer = [];
                cube.VertexBuffer = cubeCache.ConvertAll(vector => vector.Clone());
                ChangeLocation(WIDTH, HEIGHT, cube.VertexBuffer);
                Invalidate();
            }
        }

        private void ChangeLocation(int width, int height, List<Vector4D> vertexBuffer)
        {
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i].X = vertexBuffer[i].X + CENTER.X;
                vertexBuffer[i].Y = -vertexBuffer[i].Y + CENTER.Y;
            }
        }
        private void ChangeLocation(int width, int height, List<Vector2D> vertexBuffer)
        {
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i].X = vertexBuffer[i].X + CENTER.X;
                vertexBuffer[i].Y = -vertexBuffer[i].Y + CENTER.Y;
            }
        }

        private static void Scale(float scale, List<Vector4D> vertexBuffer)
        {
            Matrix scaleMatrix = Matrix.ScaleMatrix(scale);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * scaleMatrix;
            }
        }

        private static void RotateX(float angle, List<Vector4D> vertexBuffer)
        {
            // vertexBuffer == cubeCache
            Matrix rotateMatrix = Matrix.RotateX(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * rotateMatrix;
            }
        }

        private static void RotateY(float angle, List<Vector4D> vertexBuffer)
        {
            Matrix rotateMatrix = Matrix.RotateY(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * rotateMatrix;
            }
        }

        private static void RotateZ(float angle, List<Vector4D> vertexBuffer)
        {
            Matrix rotateMatrix = Matrix.RotateZ(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * rotateMatrix;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, x_axis.VertexBuffer);
            y_axis.Draw(e.Graphics, y_axis.VertexBuffer);

            // Draw cube
            cube.Draw(e.Graphics, cube.VertexBuffer);
        }

        private void NormalizeRoationAngles()
        {
            foreach (var keyPair in rotationAngles)
            {
                float angle = keyPair.Value;

                // Make sure the angle is between 0 and 2 * PI
                angle %= 2 * MathF.PI;

                // Make sure the angle is positive
                if (angle < 0)
                    angle += 2 * MathF.PI;

                // Update the dictionary
                rotationAngles[keyPair.Key] = angle;
            }
        }

        private void UpdateCubeRotation()
        {
            foreach (var keyPair in rotationAngles)
            {
                float angle = keyPair.Value;
                Keys key = keyPair.Key;

                if (angle != 0)
                {
                    if (key == Keys.Q)
                        RotateZ(-angle, cubeCache);
                    if (key == Keys.E)
                        RotateZ(angle, cubeCache);
                    if (key == Keys.A)
                        RotateY(angle, cubeCache);
                    if (key == Keys.D)
                        RotateY(-angle, cubeCache);
                    if (key == Keys.W)
                        RotateX(-angle, cubeCache);
                    if (key == Keys.S)
                        RotateX(angle, cubeCache);
                }
            }

            cube.VertexBuffer = cubeCache.ConvertAll(vector => vector.Clone());
            ChangeLocation(WIDTH, HEIGHT, cube.VertexBuffer);
            Invalidate();
        }

        private void ResetCube()
        {
            cube = new Cube(Color.Red);
            Scale(100, cube.VertexBuffer);
            cubeCache = cube.VertexBuffer.ConvertAll(vector => vector.Clone());
            ChangeLocation(WIDTH, HEIGHT, cube.VertexBuffer);
            rotationAngles = rotationAngles.ToDictionary(pair => pair.Key, pair => 0.0f);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            if (e.KeyCode == Keys.PageUp)
                Scale(1.1f, cube.VertexBuffer);
            if (e.KeyCode == Keys.PageDown)
                Scale(0.9f, cube.VertexBuffer);
            if (e.KeyCode == Keys.Home)
                ResetCube();

            if (e.KeyCode == Keys.CapsLock)
                isRotating = !isRotating;

            if (rotationAngles.ContainsKey(e.KeyCode))
            {
                rotationAngles[e.KeyCode] = 0.1f;
                NormalizeRoationAngles();
                UpdateCubeRotation();
            }
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (rotationAngles.ContainsKey(e.KeyCode))
            {
                rotationAngles[e.KeyCode] = 0.0f;
                NormalizeRoationAngles();
                UpdateCubeRotation();
            }
        }
    }
}