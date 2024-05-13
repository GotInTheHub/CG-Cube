using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public class Pyramid : Shape
    {
        //
        // 
        //            0
        //          3/-\-------4
        //         //   \     /
        //        //     \   /
        //       //       \ /
        //      2----------1

        public Pyramid(Color color, float size = 1) : base(color, size)
        {
            Name = "Pyramid";
            VertexBuffer =
            [
                new Vector( 0.0f,  1.0f, 0.0f) * size,     //0
                new Vector( 1.0f, -1.0f, 1.0f) * size,     //1
                new Vector(-1.0f, -1.0f, 1.0f) * size,     //2
                new Vector(-1.0f, -1.0f, -1.0f) * size,    //3
                new Vector( 1.0f, -1.0f, -1.0f) * size,    //4

                new Vector( 0.2f,  1.2f, 0.2f) * size,     //0
                new Vector( 1.2f, -1.2f, 1.2f) * size,     //1
                new Vector(-1.2f, -1.2f, 1.2f) * size,     //2
                new Vector(-1.2f, -1.2f, -1.2f) * size,    //3
                new Vector( 1.2f, -1.2f, -1.2f) * size,    //4


            ];
            Projection = VertexBuffer.ConvertAll(v => v.Clone());
        }

        public override void Reset()
        {
            Pyramid temp = new(color, _size);
            VertexBuffer = temp.VertexBuffer;
            Projection = temp.Projection;

            base.Reset();
        }

        public override void Draw(Graphics g, List<Vector> vb)
        {
            Pen pen = new(color, 2f);
            Brush brush = new SolidBrush(Color.Black);

            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[1].X, vb[1].Y);    //0 -> 1
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[2].X, vb[2].Y);    //0 -> 2
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[3].X, vb[3].Y);    //0 -> 3
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[4].X, vb[4].Y);    //0 -> 4

            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[2].X, vb[2].Y);    //1 -> 2
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[3].X, vb[3].Y);    //2 -> 3
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[4].X, vb[4].Y);    //3 -> 4
            g.DrawLine(pen, vb[4].X, vb[4].Y, vb[1].X, vb[1].Y);    //4 -> 1

            Font font = new("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < 5; i++)
            {
                PointF p = new(vb[i + 5].X, vb[i + 5].Y);
                g.DrawString(i.ToString(), font, brush, p);
            }
        }
    }
}
