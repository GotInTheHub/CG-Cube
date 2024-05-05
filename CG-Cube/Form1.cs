using CG_Cube.Algebra;
using CG_Cube.Axes;
using CG_Cube.Shapes;
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
        readonly Axis x_axis, y_axis, z_axis;

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
        float angleX = 0.0f;
        float angleY = 0.0f;
        float angleZ = 0.0f;
        float scale = 1.0f;
        int phase = 1;

        // Objects
        Cube cube;
        HashSet<Shape> shapes;

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
            x_axis = new AxisX(Color.Red, 100);
            y_axis = new AxisY(Color.Green, 100);
            z_axis = new AxisZ(Color.Blue, 100);

            // Create objects
            cube = new Cube(Color.Red, 100);
            //cube.ModelTransformation(new(), new(), scale);

            shapes = [cube, x_axis, y_axis, z_axis];

            DrawText(new(CreateGraphics(), ClientRectangle));
        }

        private void Animation1(Shape shape)
        {
            Theta -= 1.0f;

            if (scale >= 1.5f)
                AnimationForward = false;
            else if (scale < 1.0f)
            {
                AnimationForward = true;
                phase = 2;
            }

            if (AnimationForward)
                scale += 0.01f;
            else
                scale -= 0.01f;

            shape.Scale(scale);
        }

        private void Animation2(Shape shape)
        {
            Theta -= 1.0f;

            if (angleX >= Matrix.DegreesToRadians(45))
                AnimationForward = false;
            else if (angleX < 0.0f)
            {
                phase = 3;
                AnimationForward = true;
            }

            if (AnimationForward)
                Rotate(new(Matrix.DegreesToRadians(1), 0.0f, 0.0f), shape);
            else
                Rotate(new(-Matrix.DegreesToRadians(1), 0.0f, 0.0f), shape);
        }

        private void Animation3(Shape shape)
        {
            Phi += 1.0f;
            if (angleY >= Matrix.DegreesToRadians(45))
                AnimationForward = false;
            else if (angleY < 0.0f)
            {
                AnimationForward = true;
                phase = 4;
            }

            if (AnimationForward)
                Rotate(new(0.0f, Matrix.DegreesToRadians(1), 0.0f), shape);
            else
                Rotate(new(0.0f, -Matrix.DegreesToRadians(1), 0.0f), shape);
        }

        public void Animation4()
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

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (isRotating)
            {
                if (phase == 1)
                {
                    Animation1(cube);
                }
                if (phase == 2)
                {
                    Animation2(cube);
                }
                if (phase == 3)
                {
                    Animation3(cube);
                }
                if (phase == 4)
                {
                    Animation4();
                }

                Invalidate();
            }
        }

        private List<Vector> Projection(Shape shape)
        {
            return shape.Transform(shape.Projection, R, Theta, Phi);
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

        private static List<Vector> Scale(float scale, Shape shape)
        {
            return shape.Scale(scale);
        }

        private static void Rotate(Vector rotation, Shape shape)
        {
            shape.Rotate(rotation);
        }

        private static List<Vector> Translate(Vector vector, Shape shape)
        {
            return shape.Translate(vector);
        }

        private static void DrawText(PaintEventArgs e)
        {
            Font font = new("Arial", 12, FontStyle.Bold);
            int fromEdge = 170;

            e.Graphics.DrawString("s/S", font, Brushes.Black, fromEdge, 0);
            e.Graphics.DrawString("Left/Right", font, Brushes.Black, fromEdge, 20);
            e.Graphics.DrawString("Up/Down", font, Brushes.Black, fromEdge, 40);
            e.Graphics.DrawString("PgUp/PgDown", font, Brushes.Black, fromEdge, 60);
            e.Graphics.DrawString("x/X", font, Brushes.Black, fromEdge, 80);
            e.Graphics.DrawString("y/Y", font, Brushes.Black, fromEdge, 100);
            e.Graphics.DrawString("z/Z", font, Brushes.Black, fromEdge, 120);
            e.Graphics.DrawString("r/R", font, Brushes.Black, fromEdge, 160);
            e.Graphics.DrawString("d/D", font, Brushes.Black, fromEdge, 180);
            e.Graphics.DrawString("p/P", font, Brushes.Black, fromEdge, 200);
            e.Graphics.DrawString("t/T", font, Brushes.Black, fromEdge, 220);
            e.Graphics.DrawString("A", font, Brushes.Black, fromEdge, 240);

        }

        private void UpdateOnScreenValues(PaintEventArgs e, Shape shape)
        {
            Font font = new("Arial", 12, FontStyle.Bold);
            NormalizeRotationAngles(shape);

            e.Graphics.DrawString("Scale: " + MathF.Round(scale, 2), font, Brushes.Black, 0, 0);
            e.Graphics.DrawString("TransX: " + MathF.Round(shape.Translation.X, 1), font, Brushes.Black, 0, 20);
            e.Graphics.DrawString("TransY: " + MathF.Round(shape.Translation.Y, 1), font, Brushes.Black, 0, 40);
            e.Graphics.DrawString("TransZ: " + MathF.Round(shape.Translation.Z, 1), font, Brushes.Black, 0, 60);
            e.Graphics.DrawString("RotX: " + MathF.Round(Matrix.RadiansToDegrees(angleX), 0), font, Brushes.Black, 0, 80);
            e.Graphics.DrawString("RotY: " + MathF.Round(Matrix.RadiansToDegrees(angleY), 0), font, Brushes.Black, 0, 100);
            e.Graphics.DrawString("RotZ: " + MathF.Round(Matrix.RadiansToDegrees(angleZ), 0), font, Brushes.Black, 0, 120);
            e.Graphics.DrawString("R: " + MathF.Round(R, 1), font, Brushes.Black, 0, 160);
            e.Graphics.DrawString("D: " + D, font, Brushes.Black, 0, 180);
            e.Graphics.DrawString("Phi: " + MathF.Round(Phi, 1), font, Brushes.Black, 0, 200);
            e.Graphics.DrawString("Theta: " + MathF.Round(Theta, 1), font, Brushes.Black, 0, 220);
            e.Graphics.DrawString("isRotating: " + shape.IsAnimating, font, Brushes.Black, 0, 240);
            e.Graphics.DrawString("Phase: " + phase, font, Brushes.Black, 0, 280);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // draw info
            UpdateOnScreenValues(e, cube);

            // Draw axes
            foreach (Shape shape in shapes)
            {
                shape.Draw(e.Graphics, Viewport(Projection(shape)));
            }
        }

        private void NormalizeRotationAngles(Shape shape)
        {
            var x = shape.Rotation.X;
            var y = shape.Rotation.Y;
            var z = shape.Rotation.Z;
            angleX = MathF.Atan2(MathF.Sin(x), MathF.Cos(x));
            angleY = MathF.Atan2(MathF.Sin(y), MathF.Cos(y));
            angleZ = MathF.Atan2(MathF.Sin(z), MathF.Cos(z));
        }

        private void UpdateCube(KeyEventArgs e)
        {
            foreach (var keyPair in cubeModifiers)
            {
                float value = keyPair.Value;
                Keys key = keyPair.Key;

                if (value != 0)
                {
                    if (key == Keys.PageDown)
                        Translate(new Vector(0.0f, 0.0f, -value * 20, 1.0f), cube);

                    if (key == Keys.PageUp)
                        Translate(new Vector(0.0f, 0.0f, value * 20, 1.0f), cube);

                    if (key == Keys.Left)
                        Translate(new Vector(-value * 20, 0.0f, 0.0f, 1.0f), cube);

                    if (key == Keys.Right)
                        Translate(new Vector(value * 20, 0.0f, 0.0f, 1.0f), cube);

                    if (key == Keys.Up)
                        Translate(new Vector(0.0f, value * 20, 0.0f, 1.0f), cube);

                    if (key == Keys.Down)
                        Translate(new Vector(0.0f, -value * 20, 0.0f, 1.0f), cube);

                    if (key == Keys.Z)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Rotate(new(0.0f, 0.0f, -value), cube);
                        else
                            Rotate(new(0.0f, 0.0f, value), cube);
                    }

                    if (key == Keys.Y)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Rotate(new(0.0f, -value, 0.0f), cube);
                        else
                            Rotate(new(0.0f, value, 0.0f), cube);
                    }

                    if (key == Keys.X)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Rotate(new(-value, 0.0f, 0.0f), cube);
                        else
                            Rotate(new(value, 0.0f, 0.0f), cube);
                    }

                    if (key == Keys.S)
                    {
                        if (e.Modifiers == Keys.Shift)
                        {
                            scale += value;
                            Scale(scale, cube);
                        }
                        else
                        {
                            scale -= value;
                            Scale(scale, cube);
                        }
                    }
                }
            }
        }

        private void UpdateCamera(KeyEventArgs e)
        {
            foreach (var keyPair in cameraModifiers)
            {
                float angle = keyPair.Value;
                Keys key = keyPair.Key;

                if (angle != 0)
                {
                    if (key == Keys.R)
                    {
                        if (e.Modifiers == Keys.Shift)
                            R += angle;
                        else
                            R -= angle;
                    }

                    if (key == Keys.D)
                    {
                        if (e.Modifiers == Keys.Shift)
                            D += 10;
                        else
                            D -= 10;
                    }

                    if (key == Keys.T)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Theta += 10;
                        else
                            Theta -= 10;
                    }

                    if (key == Keys.P)
                    {
                        if (e.Modifiers == Keys.Shift)
                            Phi += 10;
                        else
                            Phi -= 10;
                    }
                }
            }
        }

        private void Reset()
        {
            isRotating = false;
            AnimationForward = true;
            phase = 1;

            scale = 1.0f;

            D = startD;
            Theta = startTheta;
            Phi = startPhi;
            R = startR;

            x_axis.Reset();
            y_axis.Reset();
            z_axis.Reset();

            cube.Reset();
            cubeModifiers = cubeModifiers.ToDictionary(pair => pair.Key, pair => 0.0f);
            Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            if (e.KeyCode == Keys.A && e.Modifiers == Keys.Shift)
                isRotating = !isRotating;

            if (e.KeyCode == Keys.C && e.Modifiers == Keys.Shift)
                Reset();

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