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
        readonly Vector4D CENTER = new(WIDTH * 0.5f, HEIGHT * 0.5f, 0.0f);
        // Axes
        readonly AxisX x_axis;
        readonly AxisY y_axis;
        readonly AxisZ z_axis;

        // Camera Parameters
        const float D = 800;
        const float Theta = -100;
        const float Phi = -10;
        const float R = 10;

        // Objects
        Cube cube;

        // Tools
        readonly System.Timers.Timer timer = new();
        bool isRotating = false;
        private Dictionary<Keys, float> cubeModifiers = new()
        {
            { Keys.Q, 0.0f },
            { Keys.E, 0.0f },
            { Keys.A, 0.0f },
            { Keys.D, 0.0f },
            { Keys.W, 0.0f },
            { Keys.S, 0.0f },
            {Keys.Left, 0.0f },
            {Keys.Right, 0.0f },
            {Keys.Up, 0.0f },
            {Keys.Down, 0.0f },
            {Keys.OemMinus, 0.0f },
            {Keys.Oemplus, 0.0f },
            {Keys.PageUp, 0.0f },
            {Keys.PageDown, 0.0f },
        };

        public Form1()
        {
            InitializeComponent();

            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            // Timer
            timer.Interval = 333;
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            // Define axes
            x_axis = new AxisX(3);
            y_axis = new AxisY(3);
            z_axis = new AxisZ(3);

            // Create objects
            cube = new Cube(Color.Red);

            // Scale objects
            Scale(100, cube.VertexBuffer);
            Scale(100, x_axis.VertexBuffer);
            Scale(100, y_axis.VertexBuffer);
            Scale(100, z_axis.VertexBuffer);

            // Change location
            Viewport(cube.VertexBuffer);
            Viewport(x_axis.VertexBuffer);
            Viewport(y_axis.VertexBuffer);
            Viewport(z_axis.VertexBuffer);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isRotating)
            {
                RotateCamera(0.0f, 0.0f, 0.1f, cube.VertexBuffer);
                RotateCamera(0.0f, 0.0f, 0.1f, x_axis.VertexBuffer);
                RotateCamera(0.0f, 0.0f, 0.1f, y_axis.VertexBuffer);
                RotateCamera(0.0f, 0.0f, 0.1f, z_axis.VertexBuffer);

                Invalidate();
            }
        }

        private static void Project(Vector4D projection, List<Vector4D> vertexBuffer)
        {
            Matrix projectionMatrix = Matrix.Projection(0.01f, projection);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] *= projectionMatrix;
            }
        }

        private List<Vector4D> Viewport(List<Vector4D> vertexBuffer)
        {
            List<Vector4D> cache = [];
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                cache.Add(new(vertexBuffer[i].X + CENTER.X, CENTER.Y - vertexBuffer[i].Y, vertexBuffer[i].Z + CENTER.Z));
            }
            return cache;
        }

        private static void Scale(float scale, List<Vector4D> vertexBuffer)
        {
            Matrix scaleMatrix = Matrix.ScaleMatrix(scale);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * scaleMatrix;
            }
        }

        private static void RotateCamera(float r, float theta, float phi, List<Vector4D> vertexBuffer)
        {
            Matrix rotationMatrix = Matrix.TransformationView(R + r, Theta + theta, Phi + phi);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * rotationMatrix;
            }
        }

        private static void RotateX(float angle, List<Vector4D> vertexBuffer)
        {
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

        private static void Translate(Vector4D vector, List<Vector4D> vertexBuffer)
        {
            Matrix rotationMatrix = Matrix.TranslateWithVector(vector);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] *= rotationMatrix;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, Viewport(x_axis.VertexBuffer));
            y_axis.Draw(e.Graphics, Viewport(y_axis.VertexBuffer));
            z_axis.Draw(e.Graphics, Viewport(z_axis.VertexBuffer));

            // Draw cube
            cube.Draw(e.Graphics, Viewport(cube.VertexBuffer));
        }

        private void NormalizeRoationAngles()
        {
            foreach (var keyPair in cubeModifiers)
            {
                float angle = keyPair.Value;

                // Make sure the angle is between 0 and 2 * PI
                angle %= 2 * MathF.PI;

                // Make sure the angle is positive
                if (angle < 0)
                    angle += 2 * MathF.PI;

                // Update the dictionary
                cubeModifiers[keyPair.Key] = angle;
            }
        }

        private void UpdateCube()
        {
            foreach (var keyPair in cubeModifiers)
            {
                float angle = keyPair.Value;
                Keys key = keyPair.Key;

                if (angle != 0)
                {
                    if (key == Keys.Q)
                    {
                        Translate(new Vector4D(0.0f, 0.0f, -10.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector4D(0.0f, 0.0f, -10.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, 0.0f, -10.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, 0.0f, -10.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.E)
                    {
                        Translate(new Vector4D(0.0f, 0.0f, 10.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector4D(0.0f, 0.0f, 10.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, 0.0f, 10.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, 0.0f, 10.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.A)
                    {
                        Translate(new Vector4D(-10.0f, 0.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector4D(-10.0f, 0.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector4D(-10.0f, 0.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector4D(-10.0f, 0.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.D)
                    {
                        Translate(new Vector4D(10.0f, 0.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector4D(10.0f, 0.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector4D(10.0f, 0.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector4D(10.0f, 0.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.W)
                    {
                        Translate(new Vector4D(0.0f, 10.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector4D(0.0f, 10.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, 10.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, 10.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.S)
                    {
                        Translate(new Vector4D(0.0f, -10.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector4D(0.0f, -10.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, -10.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector4D(0.0f, -10.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.Oemplus)
                    {
                        RotateZ(-angle, cube.VertexBuffer);
                        RotateZ(-angle, x_axis.VertexBuffer);
                        RotateZ(-angle, y_axis.VertexBuffer);
                        RotateZ(-angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.OemMinus)
                    {
                        RotateZ(angle, cube.VertexBuffer);
                        RotateZ(angle, x_axis.VertexBuffer);
                        RotateZ(angle, y_axis.VertexBuffer);
                        RotateZ(angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Left)
                    {
                        RotateY(-angle, cube.VertexBuffer);
                        RotateY(-angle, x_axis.VertexBuffer);
                        RotateY(-angle, y_axis.VertexBuffer);
                        RotateY(-angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Right)
                    {
                        RotateY(angle, cube.VertexBuffer);
                        RotateY(angle, x_axis.VertexBuffer);
                        RotateY(angle, y_axis.VertexBuffer);
                        RotateY(angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Up)
                    {
                        RotateX(-angle, cube.VertexBuffer);
                        RotateX(-angle, x_axis.VertexBuffer);
                        RotateX(-angle, y_axis.VertexBuffer);
                        RotateX(-angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Down)
                    {
                        RotateX(angle, cube.VertexBuffer);
                        RotateX(angle, x_axis.VertexBuffer);
                        RotateX(angle, y_axis.VertexBuffer);
                        RotateX(angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.PageUp)
                        Scale(1.1f, cube.VertexBuffer);
                    if (key == Keys.PageDown)
                        Scale(0.9f, cube.VertexBuffer);
                }
            }
        }

        private void ResetCube()
        {
            isRotating = false;

            x_axis.VertexBuffer = (new AxisX(3)).VertexBuffer.ConvertAll(vector => vector.Clone());
            y_axis.VertexBuffer = (new AxisY(3)).VertexBuffer.ConvertAll(vector => vector.Clone());
            z_axis.VertexBuffer = new AxisZ(3).VertexBuffer.ConvertAll(vector => vector.Clone());
            cube = new Cube(Color.Red);

            Scale(100, cube.VertexBuffer);
            Scale(100, x_axis.VertexBuffer);
            Scale(100, y_axis.VertexBuffer);
            Scale(100, z_axis.VertexBuffer);

            cubeModifiers = cubeModifiers.ToDictionary(pair => pair.Key, pair => 0.0f);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            if (e.KeyCode == Keys.CapsLock)
                isRotating = !isRotating;

            if (e.KeyCode == Keys.Home)
                ResetCube();

            if (cubeModifiers.ContainsKey(e.KeyCode))
            {
                cubeModifiers[e.KeyCode] = 0.1f;
                NormalizeRoationAngles();
                UpdateCube();
            }
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (cubeModifiers.ContainsKey(e.KeyCode))
            {
                cubeModifiers[e.KeyCode] = 0.0f;
                NormalizeRoationAngles();
                UpdateCube();
            }
        }
    }
}