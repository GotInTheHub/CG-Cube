using CG_Cube.Algebra;
using CG_Cube.Axes;
using CG_Cube.Shapes;
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
        readonly Square square;
        readonly Square square2;
        Square square3;
        List<Vector2D> squareCache;
        List<Vector2D> squareCache2;

        // Tools
        System.Timers.Timer timer = new();

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
            timer.Interval = 34;
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create objects
            square = new Square(Color.Purple, 100);
            square2 = new Square(Color.Chartreuse, 100);
            square3 = new Square(Color.Peru, 100);
            squareCache = square3.VertexBuffer.ConvertAll(vector => vector.Clone());
            squareCache2 = square.VertexBuffer.ConvertAll(vector => vector.Clone());

            //// Transformations
            Scale(2.0f, square2.VertexBuffer);
            RotateX((float)Math.PI / 4, square2.VertexBuffer);

            //// Change location
            ChangeLocation(WIDTH, HEIGHT, x_axis.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, y_axis.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, square.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, square2.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, square3.VertexBuffer);
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RotateX(0.1f, squareCache);
            square3.VertexBuffer = [];
            square3.VertexBuffer = squareCache.ConvertAll(vector => vector.Clone());
            ChangeLocation(WIDTH, HEIGHT, square3.VertexBuffer);
            Invalidate();
        }

        private void Timer_Elapsed_Manual(float rotation)
        {
            RotateX(rotation, squareCache2);
            square.VertexBuffer = [];
            square.VertexBuffer = squareCache2.ConvertAll(vector => vector.Clone());
            ChangeLocation(WIDTH, HEIGHT, square.VertexBuffer);
            Invalidate();
        }

        private void ChangeLocation(int width, int height, List<Vector2D> vertexBuffer)
        {
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i].X = vertexBuffer[i].X + CENTER.X;
                vertexBuffer[i].Y = -vertexBuffer[i].Y + CENTER.Y;
            }
        }

        private static void Scale(float scale, List<Vector2D> vertexBuffer)
        {
            Matrix2D scaleMatrix = Matrix2D.ScaleMatrix(scale);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = vertexBuffer[i] * scaleMatrix;
            }
        }

        private static void RotateX(float angle, List<Vector2D> vertexBuffer)
        {
            Matrix2D rotation = Matrix2D.RotateMatrix(angle);
            for (int i = 0; i < vertexBuffer.Count; i++)
            {
                vertexBuffer[i] = rotation * vertexBuffer[i];
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, x_axis.VertexBuffer);
            y_axis.Draw(e.Graphics, y_axis.VertexBuffer);

            // Draw square
            square.Draw(e.Graphics, square.VertexBuffer);
            square2.Draw(e.Graphics, square2.VertexBuffer);
            square3.Draw(e.Graphics, square3.VertexBuffer);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            if (e.KeyCode == Keys.PageUp)
                Scale(1.1f, square2.VertexBuffer);
            if (e.KeyCode == Keys.PageDown)
                Scale(0.9f, square2.VertexBuffer);
            if (e.KeyCode == Keys.Left)
                Timer_Elapsed_Manual(0.1f);
            if (e.KeyCode == Keys.Right)
                Timer_Elapsed_Manual(-0.1f);

            //Invalidate();
        }
    }
}