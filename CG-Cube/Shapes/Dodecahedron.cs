using CG_Cube.Algebra;

namespace CG_Cube.Shapes
{
    public class Dodecahedron : Shape
    {
        private static readonly float Phi = (1.0f + MathF.Sqrt(5)) * 0.5f;
        public Dodecahedron(Color color, float size = 1) : base(color, size)
        {
            float phi = Phi;
            float invPhi = 1.0f / phi;

            Name = "Dodecahedron";
            VertexBuffer = [
                #region Vertices
                new Vector(1f, 1f, 1f) * size,
                new Vector(1f, 1f, -1f) * size,
                new Vector(1f, -1f, 1f) * size,
                new Vector(1f, -1f, -1f) * size,
                new Vector(-1f, 1f, 1f) * size,
                new Vector(-1f, 1f, -1f) * size,
                new Vector(-1f, -1f, 1f) * size,
                new Vector(-1f, -1f, -1f) * size,

                new Vector(0, phi, invPhi) * size,
                new Vector(0, phi, -invPhi) * size,
                new Vector(0, -phi, invPhi) * size,
                new Vector(0, -phi, -invPhi) * size,

                new Vector(phi, invPhi, 0) * size,
                new Vector(phi, -invPhi, 0) * size,
                new Vector(-phi, invPhi, 0) * size,
                new Vector(-phi, -invPhi, 0) * size,

                new Vector(invPhi, 0, phi) * size,
                new Vector(invPhi, 0, -phi) * size,
                new Vector(-invPhi, 0, phi) * size,
                new Vector(-invPhi, 0, -phi) * size,
                #endregion
                #region Numbers
                new Vector(1.2f, 1.2f, 1.2f) * size,
                new Vector(1.2f, 1.2f, -1.2f) * size,
                new Vector(1.2f, -1.2f, 1.2f) * size,
                new Vector(1.2f, -1.2f, -1.2f) * size,
                new Vector(-1.2f, 1.2f, 1.2f) * size,
                new Vector(-1.2f, 1.2f, -1.2f) * size,
                new Vector(-1.2f, -1.2f, 1.2f) * size,
                new Vector(-1.2f, -1.2f, -1.2f) * size,

                new Vector(0.2f, phi * 1.2f, invPhi * 1.2f) * size,
                new Vector(0.2f, phi * 1.2f, -invPhi * 1.2f) * size,
                new Vector(0.2f, -phi * 1.2f, invPhi * 1.2f) * size,
                new Vector(0.2f, -phi * 1.2f, -invPhi * 1.2f) * size,

                new Vector(phi * 1.2f, invPhi * 1.2f, 0) * size,
                new Vector(phi * 1.2f, -invPhi * 1.2f, 0) * size,
                new Vector(-phi * 1.2f, invPhi * 1.2f, 0) * size,
                new Vector(-phi * 1.2f, -invPhi * 1.2f, 0) * size,

                new Vector(invPhi * 1.2f, 0.2f, phi * 1.2f) * size,
                new Vector(invPhi * 1.2f, 0.2f, -phi * 1.2f) * size,
                new Vector(-invPhi * 1.2f, 0.2f, phi * 1.2f) * size,
                new Vector(-invPhi * 1.2f, 0.2f, -phi * 1.2f) * size
#endregion
            ];
            Projection = VertexBuffer.ConvertAll(v => v.Clone());
        }

        public override void Reset()
        {
            Dodecahedron temp = new(color, _size);
            VertexBuffer = temp.VertexBuffer;
            Projection = temp.Projection;

            base.Reset();
        }

        public override void Draw(Graphics g, List<Vector> vb)
        {
            Pen pen = new(color, 2f);
            Brush brush = new SolidBrush(Color.Black);

            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[8].X, vb[8].Y);        //0 -> 8
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[12].X, vb[12].Y);      //0 -> 12
            g.DrawLine(pen, vb[0].X, vb[0].Y, vb[16].X, vb[16].Y);      //0 -> 16
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[9].X, vb[9].Y);        //1 -> 9
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[12].X, vb[12].Y);      //1 -> 12
            g.DrawLine(pen, vb[1].X, vb[1].Y, vb[17].X, vb[17].Y);      //1 -> 17
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[10].X, vb[10].Y);      //2 -> 10
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[13].X, vb[13].Y);      //2 -> 13
            g.DrawLine(pen, vb[2].X, vb[2].Y, vb[16].X, vb[16].Y);      //2 -> 16
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[11].X, vb[11].Y);      //3 -> 11
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[13].X, vb[13].Y);      //3 -> 13
            g.DrawLine(pen, vb[3].X, vb[3].Y, vb[17].X, vb[17].Y);      //3 -> 17
            g.DrawLine(pen, vb[4].X, vb[4].Y, vb[8].X, vb[8].Y);        //4 -> 8
            g.DrawLine(pen, vb[4].X, vb[4].Y, vb[14].X, vb[14].Y);      //4 -> 14
            g.DrawLine(pen, vb[4].X, vb[4].Y, vb[18].X, vb[18].Y);      //4 -> 18
            g.DrawLine(pen, vb[5].X, vb[5].Y, vb[9].X, vb[9].Y);        //5 -> 9
            g.DrawLine(pen, vb[5].X, vb[5].Y, vb[14].X, vb[14].Y);      //5 -> 14
            g.DrawLine(pen, vb[5].X, vb[5].Y, vb[19].X, vb[19].Y);      //5 -> 19
            g.DrawLine(pen, vb[6].X, vb[6].Y, vb[10].X, vb[10].Y);      //6 -> 10
            g.DrawLine(pen, vb[6].X, vb[6].Y, vb[15].X, vb[15].Y);      //6 -> 15
            g.DrawLine(pen, vb[6].X, vb[6].Y, vb[18].X, vb[18].Y);      //6 -> 18
            g.DrawLine(pen, vb[7].X, vb[7].Y, vb[11].X, vb[11].Y);      //7 -> 11
            g.DrawLine(pen, vb[7].X, vb[7].Y, vb[11].X, vb[11].Y);      //7 -> 11
            g.DrawLine(pen, vb[7].X, vb[7].Y, vb[15].X, vb[15].Y);      //7 -> 15
            g.DrawLine(pen, vb[7].X, vb[7].Y, vb[19].X, vb[19].Y);      //7 -> 19
            g.DrawLine(pen, vb[8].X, vb[8].Y, vb[9].X, vb[9].Y);        //8 -> 9
            g.DrawLine(pen, vb[10].X, vb[10].Y, vb[11].X, vb[11].Y);    //10 -> 11
            g.DrawLine(pen, vb[12].X, vb[12].Y, vb[13].X, vb[13].Y);    //12 -> 13
            g.DrawLine(pen, vb[14].X, vb[14].Y, vb[15].X, vb[15].Y);    //14 -> 15
            g.DrawLine(pen, vb[17].X, vb[17].Y, vb[19].X, vb[19].Y);    //17 -> 19
            g.DrawLine(pen, vb[16].X, vb[16].Y, vb[18].X, vb[18].Y);    //16 -> 18

            Font font = new("Arial", 12, FontStyle.Bold);
            for (int i = 0; i < 20; i++)
            {
                PointF p = new(vb[i + 20].X, vb[i + 20].Y);
                g.DrawString(i.ToString(), font, brush, p);
            }
        }
    }
}
