using CG_Cube.Algebra;
using CG_Cube.Axes;
using CG_Cube.Shapes;

namespace CG_Cube
{
    public partial class Form1 : Form
    {
        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        // Axes
        readonly AxisX x_axis;
        readonly AxisY y_axis;

        // Objects
        readonly Square square;

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
            Matrix2D m2 = new Matrix2D(
                2, 4,
                -1, 3);
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create objects
            square = new Square(Color.Purple, 100);
            ChangeLocation(WIDTH, HEIGHT, x_axis.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, y_axis.VertexBuffer);
            ChangeLocation(WIDTH, HEIGHT, square.VertexBuffer);
        }

        private static void ChangeLocation(int width, int height, List<Vector2D> vertexBuffer)
        {
            foreach (var vertex in vertexBuffer)
            {
                vertex.X += width * 0.5f;
                vertex.Y = height * 0.5f - vertex.Y;
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
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}