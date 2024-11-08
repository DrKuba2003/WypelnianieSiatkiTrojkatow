using System.Numerics;
using WypelnianieSiatkiTrojkatow.Utils;

namespace WypelnianieSiatkiTrojkatow
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_PTS = "Punkty\\punkty.txt";
        private static readonly Pen CONTROL_NET_PEN = new Pen(Brushes.DarkBlue, 2);

        private Bitmap drawArea;
        private Model model;

        public Form1()
        {
            InitializeComponent();

            // set labels text
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            betaValue.Text = betaAngleTrack.Value.ToString();
            punktyPathValue.Text = DEFAULT_PTS;

            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;

            model = new Model(DEFAULT_PTS, netPrecisionTrack.Value,
                alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        public void Draw()
        {
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.WhiteSmoke);

                // Zmiana ukladu wspolrzednych
                g.ScaleTransform(1, -1);
                g.TranslateTransform(Canvas.Width / 2, -Canvas.Height / 2);

                if (drawFillingCheck.Checked)
                    FillTriangles(g);

                if (drawTriangleNetCheck.Checked)
                    DrawNet(g);

                if (drawControlPtsCheck.Checked)
                    DrawControlPts(g);

                //DrawPrecisionPts(g);

            }
            Canvas.Refresh();
        }

        public void FillTriangles(Graphics g)
        {
            foreach (var t in model.net)
                FillPolygon(g, t);
        }

        public void FillPolygon(Graphics g, IPolygon poly)
        {
            EdgesTable ET = poly.GetET();
            if (ET.IsEmpty()) return;

            int y = ET.minY;
            EdgeList AET = new EdgeList();
            do
            {
                if (y <= ET.maxY && ET.ET.ContainsKey(y))
                {
                    AET.AddAtEnd(ET[y]);
                    ET[y].Clear();
                }
                AET.QSort();

                Edge? e = AET.head;
                while (e is not null && e.next is not null)
                {
                    g.DrawLine(Pens.Magenta,
                        new Point((int)e.x, y), new Point((int)e.next.x, y));
                    e = e.next.next;
                }

                e = AET.head;
                while (e is not null)
                {
                    if (e.ymax == y ||
                        (!ET[ET.maxY].IsEmpty() && e.ymax == y + 1)) // TODO investigate
                        AET.Delete(e);
                    else
                        e.x += e.delta;

                    e = e.next;
                }

                y++;
            } while (!AET.IsEmpty() || !ET[ET.maxY].IsEmpty());

            if (poly is Triangle)
                DrawTriangle(g, (Triangle)poly, Pens.Magenta);
        }

        private void DrawNet(Graphics g)
        {
            foreach (var t in model.net)
                DrawTriangle(g, t, Pens.DarkMagenta);
        }

        public void DrawTriangle(Graphics g, Triangle t, Pen p)
        {
            g.DrawLine(p,
                    new Point((int)t.V1.X, (int)t.V1.Y),
                    new Point((int)t.V2.X, (int)t.V2.Y));

            g.DrawLine(p,
                new Point((int)t.V1.X, (int)t.V1.Y),
                new Point((int)t.V3.X, (int)t.V3.Y));

            g.DrawLine(p,
                new Point((int)t.V3.X, (int)t.V3.Y),
                new Point((int)t.V2.X, (int)t.V2.Y));
        }

        private void DrawControlPts(Graphics g)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vertex v = model.ControlVertexes[j, i];

                    if (j < 3)
                        g.DrawLine(CONTROL_NET_PEN,
                            new Point((int)v.X, (int)v.Y),
                            new Point((int)model.ControlVertexes[j + 1, i].X, (int)(model.ControlVertexes[j + 1, i].Y)));

                    if (i < 3)
                        g.DrawLine(CONTROL_NET_PEN,
                            new Point((int)v.X, (int)v.Y),
                            new Point((int)model.ControlVertexes[j, i + 1].X, (int)(model.ControlVertexes[j, i + 1].Y)));

                    g.FillEllipse(Brushes.DarkGreen, v.X - 5, v.Y - 5, 10, 10);
                }
            }
        }

        private void DrawPrecisionPts(Graphics g)
        {
            if (model.PrecisionVertexes == null) return;

            foreach (var v in model.PrecisionVertexes)
                g.FillEllipse(Brushes.Blue, v.X - 5, v.Y - 5, 10, 10);
        }

        private void drawControlPtsCheck_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void drawTriangleNetCheck_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void drawFillingCheck_CheckedChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void netPrecisionTrack_Scroll(object sender, EventArgs e)
        {
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            model.LoadTriangles(netPrecisionTrack.Value);
            model.RotateVertexes(alfaAngleTrack.Value, betaAngleTrack.Value);
            Draw();
        }

        private void alfaAngleTrack_Scroll(object sender, EventArgs e)
        {
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            model.RotateVertexes(alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        private void betaAngleTrack_Scroll(object sender, EventArgs e)
        {
            betaValue.Text = betaAngleTrack.Value.ToString();
            model.RotateVertexes(alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        private void pointFileBtn_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.InitialDirectory = Directory.GetCurrentDirectory() + "\\Punkty";
                fileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                fileDialog.FilterIndex = 0;
                fileDialog.Multiselect = false;
                fileDialog.RestoreDirectory = true;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = fileDialog.FileName;
                    punktyPathValue.Text = path;

                    model.CalculateModel(path, netPrecisionTrack.Value,
                        alfaAngleTrack.Value, betaAngleTrack.Value);

                    Draw();
                }
            }
        }

    }
}
