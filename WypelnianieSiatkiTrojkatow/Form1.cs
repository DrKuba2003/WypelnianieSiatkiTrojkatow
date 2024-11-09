using System.Drawing;
using System.Numerics;
using System.Runtime.Serialization;
using WypelnianieSiatkiTrojkatow.Utils;

namespace WypelnianieSiatkiTrojkatow
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_PTS = "Punkty\\punkty4.txt";
        private static readonly Pen CONTROL_NET_PEN = new Pen(Brushes.DarkBlue, 2);

        private Bitmap drawArea;
        private Model model;

        public Form1()
        {
            InitializeComponent();

            // set labels text
            SetLabels();

            drawArea = new Bitmap(Canvas.Size.Width, Canvas.Size.Height);
            Canvas.Image = drawArea;

            model = new Model(DEFAULT_PTS, netPrecisionTrack.Value,
                alfaAngleTrack.Value, betaAngleTrack.Value);

            Draw();
        }

        private void SetLabels()
        {
            netPrecValue.Text = netPrecisionTrack.Value.ToString();
            alfaValue.Text = alfaAngleTrack.Value.ToString();
            betaValue.Text = betaAngleTrack.Value.ToString();
            punktyPathValue.Text = DEFAULT_PTS;
            kdValue.Text = $"{kdTrack.Value}%";
            ksValue.Text = $"{ksTrack.Value}%";
            mValue.Text = mTrack.Value.ToString();
            zValue.Text = zTrack.Value.ToString();
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

        public void FillPolygon(Graphics g, IFillablePolygon poly)
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
                    //g.DrawLine(Pens.Magenta,
                    //    new Point((int)e.x, y), new Point((int)e.next.x, y));
                    for (int x = (int)e.x; x <= (int)e.next.x; x++)
                    {
                        Color c = (Color)GetIFillColor((Triangle)poly, x, y)!;
                        var p = new Pen(c);
                        g.DrawRectangle(p, new Rectangle(x, y, 1, 1));
                        p.Dispose();
                    }
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
            
            //if (poly is Triangle)
            //    DrawTriangle(g, (Triangle)poly, Pens.Magenta);
        }

        private Color? GetIFillColor(Triangle t, int x, int y)
        {
            float kd = kdTrack.Value / 100F;
            float ks = ksTrack.Value / 100F;
            int m = mTrack.Value;
            Vector3 IL = new Vector3(1F, 1F, 1F);
            Vector3 IO = new Vector3(0.5F, 0.1F, 0.5F);
            Vector3 V = Vector3.Normalize(new Vector3(0F, 0F, 1F));
            Vector3 N = Vector3.Normalize((Vector3)V/*v.Nar*/);
            Vector3 L = new Vector3(0, 0, zTrack.Value);
            Vector3 R = Vector3.Normalize(2 * Vector3.Dot(N, L) * N - L);

            float cosNL = (float)Math.Cos(GetAngle(N, L));
            double cosVR = Math.Cos(GetAngle(V, R));
            Vector3 I = kd * IL * IO * (cosNL >= 0 ? cosNL : 0) +
                ks * IL * IO * (cosVR >= 0 ? (float)Math.Pow(cosVR, m) : 0);

            return Color.FromArgb(
                I.X <= 1 ? (int)(I.X * 255) : 255,
                I.Y <= 1 ? (int)(I.Y * 255) : 255,
                I.Z <= 1 ? (int)(I.Z * 255) : 255
                );
        }

        private float GetAngle(Vector3 v1, Vector3 v2)
            => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

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

        private void kdTrack_Scroll(object sender, EventArgs e)
        {
            kdValue.Text = $"{kdTrack.Value}%";
            Draw();
        }

        private void ksTrack_Scroll(object sender, EventArgs e)
        {
            ksValue.Text = $"{ksTrack.Value}%";
            Draw();
        }

        private void mTrack_Scroll(object sender, EventArgs e)
        {
            mValue.Text = mTrack.Value.ToString();
            Draw();
        }

        private void zTrack_Scroll(object sender, EventArgs e)
        {
            zValue.Text = zTrack.Value.ToString();
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

        private void PauseResumeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
