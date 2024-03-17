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

        // Camera Parameters
        float D = 800;
        float Theta = -100;
        float Phi = -10;
        float R = 10;
        const float startD = 800;
        const float startTheta = -100;
        const float startPhi = -10;
        const float startR = 10;

        // Animation variables
        bool isRotating = false;
        bool AnimationForward = true;

        // Cube Parameters
        List<Vector> projection = [];
        Vector translation = new();
        float angleX = 0.0f;
        float angleY = 0.0f;
        float angleZ = 0.0f;
        float scale = 1.0f;
        int phase = 1;

        // Objects
        Cube cube;

        // Tools
        readonly System.Timers.Timer timer = new();

        // Controls
        private Dictionary<Keys, float> cubeModifiers = new()
        {
            { Keys.X, 0.0f },
            { Keys.Y, 0.0f },
            { Keys.Z, 0.0f },
            { Keys.S, 0.0f },
            { Keys.Left, 0.0f },
            { Keys.Right, 0.0f },
            { Keys.Up, 0.0f },
            { Keys.Down, 0.0f },
            { Keys.PageUp, 0.0f },
            { Keys.PageDown, 0.0f },
        };
        private readonly Dictionary<Keys, float> cameraModifiers = new()
        {
            { Keys.T, 0.0f },
            { Keys.P, 0.0f },
            { Keys.R, 0.0f },
            { Keys.D, 0.0f }
        };

        public Form1()
        {
            InitializeComponent();

            Width = WIDTH;
            Height = HEIGHT;
            DoubleBuffered = true;

            // Timer
            timer.Interval = 50;
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
                if (phase == 1)
                {
                    Theta -= 1.0f;
                    if (scale >= 1.5f)
                    {
                        AnimationForward = false;
                    }
                    if (AnimationForward)
                    {
                        scale += 0.01f;
                    }
                    else
                    {
                        scale -= 0.01f;
                    }
                    if (scale <= 1.0f)
                    {
                        phase = 2;
                        AnimationForward = true;
                    }
                }
                if (phase == 2)
                {
                    Theta -= 1.0f;
                    if (angleX >= Matrix.DegreesToRadians(45))
                    {
                        AnimationForward = false;
                    }
                    if (AnimationForward)
                    {
                        RotateX(Matrix.DegreesToRadians(1), cube.VertexBuffer);
                    }
                    else
                    {
                        RotateX(-Matrix.DegreesToRadians(1), cube.VertexBuffer);
                    }
                    if (angleX <= 0.0f)
                    {
                        AnimationForward = true;
                        phase = 3;
                    }
                }
                if (phase == 3)
                {
                    Phi += 1.0f;
                    if (angleY >= Matrix.DegreesToRadians(45))
                    {
                        AnimationForward = false;
                    }
                    if (AnimationForward)
                    {
                        RotateY(Matrix.DegreesToRadians(1), cube.VertexBuffer);
                    }
                    else
                    {
                        RotateY(-Matrix.DegreesToRadians(1), cube.VertexBuffer);
                    }
                    if (angleY <= 0.0f)
                    {
                        AnimationForward = true;
                        phase = 4;
                    }
                }
                if (phase == 4)
                {
                    if (Theta != startTheta)
                    {
                        if (Theta < startTheta)
                            Theta += 1.0f;
                        else
                            Theta -= 1.0f;
                    }
                    if (Phi != startPhi)
                    {
                        if (Phi < startPhi)
                            Phi += 1.0f;
                        else
                            Phi -= 1.0f;
                    }
                    else if (Phi == startPhi && Theta == startTheta)
                    {
                        phase = 1;
                    }
                }

                Invalidate();
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

        private void TranslateAll(Vector vector)
        {
            translation += vector;
            Translate(vector, cube.VertexBuffer);
            Translate(vector, x_axis.VertexBuffer);
            Translate(vector, y_axis.VertexBuffer);
            Translate(vector, z_axis.VertexBuffer);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int fromEdge = 170;
            base.OnPaint(e);
            NormalizeRotationAngles();

            // Draw axes
            AxisX.Draw(e.Graphics, Viewport(Scale(scale, Projection(RotateCamera(R, Theta, Phi, x_axis.VertexBuffer)))));
            AxisY.Draw(e.Graphics, Viewport(Scale(scale, Projection(RotateCamera(R, Theta, Phi, y_axis.VertexBuffer)))));
            AxisZ.Draw(e.Graphics, Viewport(Scale(scale, Projection(RotateCamera(R, Theta, Phi, z_axis.VertexBuffer)))));

            // draw info
            Font font = new("Arial", 12, FontStyle.Bold);

            e.Graphics.DrawString("Scale: " + MathF.Round(scale, 2), font, Brushes.Black, 0, 0);
            e.Graphics.DrawString("s/S", font, Brushes.Black, fromEdge, 0);

            e.Graphics.DrawString("TransX: " + MathF.Round(translation.X, 1), font, Brushes.Black, 0, 20);
            e.Graphics.DrawString("Left/Right", font, Brushes.Black, fromEdge, 20);

            e.Graphics.DrawString("TransY: " + MathF.Round(translation.Y, 1), font, Brushes.Black, 0, 40);
            e.Graphics.DrawString("Up/Down", font, Brushes.Black, fromEdge, 40);

            e.Graphics.DrawString("TransZ: " + MathF.Round(translation.Z, 1), font, Brushes.Black, 0, 60);
            e.Graphics.DrawString("PgUp/PgDown", font, Brushes.Black, fromEdge, 60);

            e.Graphics.DrawString("RotX: " + MathF.Round(Matrix.RadiansToDegrees(angleX), 0), font, Brushes.Black, 0, 80);
            e.Graphics.DrawString("x/X", font, Brushes.Black, fromEdge, 80);

            e.Graphics.DrawString("RotY: " + MathF.Round(Matrix.RadiansToDegrees(angleY), 0), font, Brushes.Black, 0, 100);
            e.Graphics.DrawString("y/Y", font, Brushes.Black, fromEdge, 100);

            e.Graphics.DrawString("RotZ: " + MathF.Round(Matrix.RadiansToDegrees(angleZ), 0), font, Brushes.Black, 0, 120);
            e.Graphics.DrawString("z/Z", font, Brushes.Black, fromEdge, 120);

            e.Graphics.DrawString("R: " + MathF.Round(R, 1), font, Brushes.Black, 0, 160);
            e.Graphics.DrawString("r/R", font, Brushes.Black, fromEdge, 160);

            e.Graphics.DrawString("D: " + D, font, Brushes.Black, 0, 180);
            e.Graphics.DrawString("d/D", font, Brushes.Black, fromEdge, 180);

            e.Graphics.DrawString("Phi: " + MathF.Round(Phi, 1), font, Brushes.Black, 0, 200);
            e.Graphics.DrawString("p/P", font, Brushes.Black, fromEdge, 200);

            e.Graphics.DrawString("Theta: " + MathF.Round(Theta, 1), font, Brushes.Black, 0, 220);
            e.Graphics.DrawString("t/T", font, Brushes.Black, fromEdge, 220);

            e.Graphics.DrawString("isRotating: " + isRotating, font, Brushes.Black, 0, 240);
            e.Graphics.DrawString("A", font, Brushes.Black, fromEdge, 240);

            e.Graphics.DrawString("Phase: " + phase, font, Brushes.Black, 0, 280);

            // Draw cube
            projection = Scale(scale, Projection(RotateCamera(R, Theta, Phi, cube.VertexBuffer)));
            cube.Draw(e.Graphics, Viewport(projection));
        }

        private void NormalizeRotationAngles()
        {
            angleX = MathF.Atan2(MathF.Sin(angleX), MathF.Cos(angleX));
            angleY = MathF.Atan2(MathF.Sin(angleY), MathF.Cos(angleY));
            angleZ = MathF.Atan2(MathF.Sin(angleZ), MathF.Cos(angleZ));
        }

        private void UpdateCube(KeyEventArgs e)
        {
            foreach (var keyPair in cubeModifiers)
            {
                float angle = keyPair.Value;
                Keys key = keyPair.Key;

                if (angle != 0)
                {
                    if (key == Keys.PageDown)
                    {
                        TranslateAll(new Vector(0.0f, 0.0f, -angle, 1.0f));
                    }
                    if (key == Keys.PageUp)
                    {
                        TranslateAll(new Vector(0.0f, 0.0f, angle, 1.0f));
                    }
                    if (key == Keys.Left)
                    {
                        TranslateAll(new Vector(-angle, 0.0f, 0.0f, 1.0f));
                    }
                    if (key == Keys.Right)
                    {
                        TranslateAll(new Vector(angle, 0.0f, 0.0f, 1.0f));
                    }
                    if (key == Keys.Up)
                    {
                        TranslateAll(new Vector(0.0f, angle, 0.0f, 1.0f));
                    }
                    if (key == Keys.Down)
                    {
                        TranslateAll(new Vector(0.0f, -angle, 0.0f, 1.0f));
                    }
                    if (key == Keys.Z && e.Modifiers == Keys.Shift)
                    {
                        RotateZ(-angle*10, cube.VertexBuffer);
                    }
                    if (key == Keys.Z && e.Modifiers != Keys.Shift)
                    {
                        RotateZ(angle*10, cube.VertexBuffer);
                    }
                    if (key == Keys.Y && e.Modifiers == Keys.Shift)
                    {
                        RotateY(-angle*10, cube.VertexBuffer);
                    }
                    if (key == Keys.Y && e.Modifiers != Keys.Shift)
                    {
                        RotateY(angle*10, cube.VertexBuffer);
                    }
                    if (key == Keys.X && e.Modifiers == Keys.Shift)
                    {
                        RotateX(-angle*10, cube.VertexBuffer);
                    }
                    if (key == Keys.X && e.Modifiers != Keys.Shift)
                    {
                        RotateX(angle*10, cube.VertexBuffer);
                    }
                    if (key == Keys.S && e.Modifiers == Keys.Shift)
                    {
                        scale += angle;
                    }
                    if (key == Keys.S && e.Modifiers != Keys.Shift)
                    {
                        scale -= angle;
                    }
                }
            }
            Invalidate();
        }

        private void UpdateCamera(KeyEventArgs e)
        {
            foreach (var keyPair in cameraModifiers)
            {
                float angle = keyPair.Value;
                Keys key = keyPair.Key;

                if (angle != 0)
                {
                    if (key == Keys.R && e.Modifiers == Keys.Shift)
                    {
                        R += angle;
                    }
                    if (key == Keys.R && e.Modifiers != Keys.Shift)
                    {
                        R -= angle;
                    }
                    if (key == Keys.D && e.Modifiers == Keys.Shift)
                    {
                        D += 10;
                    }
                    if (key == Keys.D && e.Modifiers != Keys.Shift)
                    {
                        D-= 10;
                    }
                    if (key == Keys.T && e.Modifiers == Keys.Shift)
                    {
                        Theta += 10;
                    }
                    if (key == Keys.T && e.Modifiers != Keys.Shift)
                    {
                        Theta -= 10;
                    }
                    if (key == Keys.P && e.Modifiers == Keys.Shift)
                    {
                        Phi += 10;
                    }
                    if (key == Keys.P && e.Modifiers != Keys.Shift)
                    {
                        Phi -= 10;
                    }
                }
            }
        }

        private void ResetCube()
        {
            isRotating = false;
            AnimationForward = true;
            phase = 1;

            angleX = 0.0f;
            angleY = 0.0f;
            angleZ = 0.0f;

            translation = new();
            scale = 1.0f;

            D = startD;
            Theta = startTheta;
            Phi = startPhi;
            R = startR;

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

            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Shift)
                isRotating = !isRotating;

            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Shift)
                ResetCube();

            if (cubeModifiers.ContainsKey(e.KeyCode))
            {
                cubeModifiers[e.KeyCode] = 0.1f;
                UpdateCube(e);
            }
            if (cameraModifiers.ContainsKey(e.KeyCode))
            {
                cameraModifiers[e.KeyCode] = 1.0f;
                UpdateCamera(e);
            }
            Invalidate();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (cubeModifiers.ContainsKey(e.KeyCode))
            {
                cubeModifiers[e.KeyCode] = 0.0f;
                UpdateCube(e);
            }
            if (cameraModifiers.ContainsKey(e.KeyCode))
            {
                cameraModifiers[e.KeyCode] = 0.0f;
                UpdateCamera(e);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CENTER.X = this.Width * 0.5f;
            CENTER.Y = this.Height * 0.5f;
            Invalidate();
        }
    }
}