using CG_Cube.Algebra;
using CG_Cube.Axes;
using MatrixTransformations;
using System.Timers;

namespace CG_Cube
{
    public partial class Form1 : Form
    {
        // Window dimensions
        const int WIDTH = 1600;
        const int HEIGHT = 900;
        readonly Vector CENTER = new(WIDTH * 0.5f, HEIGHT * 0.5f);

        // Axes
        readonly AxisX x_axis;
        readonly AxisY y_axis;
        readonly AxisZ z_axis;

        //// Camera Parameters
        float D = 800;
        float Theta = -100;
        float Phi = -10;
        const float R = 10;


        // Cube Parameters
        List<Vector> projection = [];
        float angleX = 0.0f;
        float angleY = 0.0f;
        float angleZ = 0.0f;
        float scale = 1.0f;

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
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isRotating)
            {
            }
        }

        private List<Vector> Projection(List<Vector> vertexBuffer)
        {
            List<Vector> result = [];
            foreach (Vector vertex in vertexBuffer)
            {
                result.Add(vertex * Matrix.ProjectionMatrix(D, vertex));
            }
            return result;
        }

        private void Viewport(Graphics graphics)
        {
            graphics.TranslateTransform(CENTER.X, CENTER.Y);
            graphics.ScaleTransform(1, -1, System.Drawing.Drawing2D.MatrixOrder.Append);
        }
        private List<Vector> Viewport(List<Vector> vertexBuffer)
        {
            List<Vector> result = [];
            foreach (Vector vertex in vertexBuffer)
            {
                result.Add(new(vertex.X + CENTER.X, CENTER.Y - vertex.Y, vertex.Z));
            }
            return result;
        }

        private static List<Vector> Scale(float scale, List<Vector> vertexBuffer)
        {
            Matrix scaleMatrix = Matrix.ScaleMatrix(scale);
            List<Vector> result = [];
            foreach (Vector vertex in vertexBuffer)
            {
                result.Add(vertex * scaleMatrix);
            }
            return result;
        }

        private static List<Vector> RotateCamera(float r, float theta, float phi, List<Vector> vertexBuffer)
        {
            Matrix rotateMatrix = Matrix.TransformationView(r, theta, phi);
            List<Vector> cache = vertexBuffer.ConvertAll(vector => vector.Clone());
            for (int i = 0; i < cache.Count; i++)
            {
                cache[i] = cache[i] * rotateMatrix;
            }
            return cache;
        }

        private void RotateX(float angle, List<Vector> vertexBuffer)
        {
            angleX += angle;
            Matrix rotateMatrix = Matrix.RotateX(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] *= rotateMatrix;
            }
        }

        private void RotateY(float angle, List<Vector> vertexBuffer)
        {
            angleY += angle;
            Matrix rotateMatrix = Matrix.RotateY(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] *= rotateMatrix;
            }
        }

        private void RotateZ(float angle, List<Vector> vertexBuffer)
        {
            angleZ += angle;
            Matrix rotateMatrix = Matrix.RotateZ(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] *= rotateMatrix;
            }
        }

        private static void Translate(Vector vector, List<Vector> vertexBuffer)
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
            NormalizeRotationAngles();

            //// Draw axes
            x_axis.Draw(e.Graphics, Viewport(Scale(200 * scale, Projection(RotateCamera(R, Theta, Phi, x_axis.VertexBuffer)))));
            y_axis.Draw(e.Graphics, Viewport(Scale(200 * scale, Projection(RotateCamera(R, Theta, Phi, y_axis.VertexBuffer)))));
            z_axis.Draw(e.Graphics, Viewport(Scale(200 * scale, Projection(RotateCamera(R, Theta, Phi, z_axis.VertexBuffer)))));

            // draw info
            Font font = new("Arial", 12, FontStyle.Bold);
            e.Graphics.DrawString("D: " + D, font, Brushes.Black, 0, 0);
            e.Graphics.DrawString("Theta: " + Theta, font, Brushes.Black, 0, 20);
            e.Graphics.DrawString("Phi: " + Phi, font, Brushes.Black, 0, 40);
            e.Graphics.DrawString("R: " + R, font, Brushes.Black, 0, 60);
            e.Graphics.DrawString("Scale: " + scale, font, Brushes.Black, 0, 80);
            e.Graphics.DrawString("AngleX: " + angleX, font, Brushes.Black, 0, 100);
            e.Graphics.DrawString("AngleY: " + angleY, font, Brushes.Black, 0, 120);
            e.Graphics.DrawString("AngleZ: " + angleZ, font, Brushes.Black, 0, 140);
            e.Graphics.DrawString("isRotating: " + isRotating, font, Brushes.Black, 0, 160);

            // Draw cube
            projection = Scale(200 * scale, Projection(RotateCamera(R, Theta, Phi, cube.VertexBuffer)));
            cube.Draw(e.Graphics, Viewport(projection));
        }

        private void NormalizeRotationAngles()
        {
            angleX = MathF.Atan2(MathF.Sin(angleX), MathF.Cos(angleX));
            angleY = MathF.Atan2(MathF.Sin(angleY), MathF.Cos(angleY));
            angleZ = MathF.Atan2(MathF.Sin(angleZ), MathF.Cos(angleZ));
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
                        Translate(new Vector(0.0f, 0.0f, -10.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector(0.0f, 0.0f, -10.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector(0.0f, 0.0f, -10.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector(0.0f, 0.0f, -10.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.E)
                    {
                        Translate(new Vector(0.0f, 0.0f, 10.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector(0.0f, 0.0f, 10.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector(0.0f, 0.0f, 10.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector(0.0f, 0.0f, 10.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.A)
                    {
                        Translate(new Vector(-10.0f, 0.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector(-10.0f, 0.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector(-10.0f, 0.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector(-10.0f, 0.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.D)
                    {
                        Translate(new Vector(10.0f, 0.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector(10.0f, 0.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector(10.0f, 0.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector(10.0f, 0.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.W)
                    {
                        Translate(new Vector(0.0f, 10.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector(0.0f, 10.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector(0.0f, 10.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector(0.0f, 10.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.S)
                    {
                        Translate(new Vector(0.0f, -10.0f, 0.0f, 1.0f), cube.VertexBuffer);
                        Translate(new Vector(0.0f, -10.0f, 0.0f, 1.0f), x_axis.VertexBuffer);
                        Translate(new Vector(0.0f, -10.0f, 0.0f, 1.0f), y_axis.VertexBuffer);
                        Translate(new Vector(0.0f, -10.0f, 0.0f, 1.0f), z_axis.VertexBuffer);
                    }
                    if (key == Keys.Oemplus)
                    {
                        RotateZ(-angle, cube.VertexBuffer);
                        //RotateZ(-angle, x_axis.VertexBuffer);
                        //RotateZ(-angle, y_axis.VertexBuffer);
                        //RotateZ(-angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.OemMinus)
                    {
                        RotateZ(angle, cube.VertexBuffer);
                        //RotateZ(angle, x_axis.VertexBuffer);
                        //RotateZ(angle, y_axis.VertexBuffer);
                        //RotateZ(angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Left)
                    {
                        RotateY(-angle, cube.VertexBuffer);
                        //RotateY(-angle, x_axis.VertexBuffer);
                        //RotateY(-angle, y_axis.VertexBuffer);
                        //RotateY(-angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Right)
                    {
                        RotateY(angle, cube.VertexBuffer);
                        //RotateY(angle, x_axis.VertexBuffer);
                        //RotateY(angle, y_axis.VertexBuffer);
                        //RotateY(angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Up)
                    {
                        RotateX(-angle, cube.VertexBuffer);
                        //RotateX(-angle, x_axis.VertexBuffer);
                        //RotateX(-angle, y_axis.VertexBuffer);
                        //RotateX(-angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.Down)
                    {
                        RotateX(angle, cube.VertexBuffer);
                        //RotateX(angle, x_axis.VertexBuffer);
                        //RotateX(angle, y_axis.VertexBuffer);
                        //RotateX(angle, z_axis.VertexBuffer);
                    }
                    if (key == Keys.PageUp)
                        scale += 0.1f;
                    if (key == Keys.PageDown)
                        scale -= 0.1f;
                }
            }
            Invalidate();
        }

        private void ResetCube()
        {
            isRotating = false;
            angleX = 0.0f;
            angleY = 0.0f;
            angleZ = 0.0f;
            scale = 1.0f;
            D = 800;
            Theta = -100;
            Phi = -10;

            x_axis.VertexBuffer = new AxisX(3).VertexBuffer.ConvertAll(vector => vector.Clone());
            y_axis.VertexBuffer = new AxisY(3).VertexBuffer.ConvertAll(vector => vector.Clone());
            z_axis.VertexBuffer = new AxisZ(3).VertexBuffer.ConvertAll(vector => vector.Clone());
            cube = new Cube(Color.Red);

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

            if (e.KeyCode == Keys.ShiftKey)
                D += 10;
            if (e.KeyCode == Keys.ControlKey)
                D -= 10;
            if (e.KeyCode == Keys.Space)
            {
                RotateCamera(R, Theta, Phi, cube.VertexBuffer);
                RotateCamera(R, Theta, Phi, x_axis.VertexBuffer);
                RotateCamera(R, Theta, Phi, y_axis.VertexBuffer);
                RotateCamera(R, Theta, Phi, z_axis.VertexBuffer);
            }

            if (cubeModifiers.ContainsKey(e.KeyCode))
            {
                cubeModifiers[e.KeyCode] = 0.1f;
                UpdateCube();
            }
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (cubeModifiers.ContainsKey(e.KeyCode))
            {
                cubeModifiers[e.KeyCode] = 0.0f;
                UpdateCube();
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            UpdateCube();
            CENTER.X = this.Width * 0.5f;
            CENTER.Y = this.Height * 0.5f;
            Invalidate();
        }
    }
}