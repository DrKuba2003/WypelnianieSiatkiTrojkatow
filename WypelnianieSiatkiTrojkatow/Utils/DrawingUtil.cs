using FastBitmapLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WypelnianieSiatkiTrojkatow.Edges;
using WypelnianieSiatkiTrojkatow.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace WypelnianieSiatkiTrojkatow.Utils
{
    public static class DrawingUtil
    {
        private static readonly Pen CONTROL_NET_PEN = new Pen(Brushes.DarkBlue, 2);

        public static void FillMesh(Bitmap drawArea, List<Triangle> triangles,
            float kd, float ks, int m,
            Vector3 light, Vector3 lightColor,
            Func<float, float, float, Vector3> ObjectColor,
            Func<int, int, (int, int)> CanvasTranslate)
        {
            using (var fastBitmap = drawArea.FastLock())
                foreach (var t in triangles)
                    FillPolygon(fastBitmap, t, kd, ks, m,
                        light, lightColor,
                        ObjectColor, CanvasTranslate);
        }

        public static void FillPolygon(FastBitmap fastBitmap,
            IFillablePolygon poly, float kd, float ks, int m,
            Vector3 light, Vector3 lightColor,
            Func<float, float, float, Vector3> ObjectColor,
            Func<int, int, (int, int)> CanvasTranslate)
        {
            EdgesBucketSorted ET = poly.GetET();
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
                    for (int x = (int)e.x; x <= (int)e.next.x; x++)
                    {
                        Color c = (Color)GetIFillColor((Triangle)poly, x, y,
                            kd, ks, m, light, lightColor, ObjectColor)!;
                        (int xT, int yT) = CanvasTranslate(x, y);
                        if (xT >= 0 && xT < fastBitmap.Width &&
                            yT >= 0 && yT < fastBitmap.Height)
                            fastBitmap.SetPixel(xT, yT, c);
                    }

                    e = e.next.next;
                }

                e = AET.head;
                while (e is not null)
                {
                    if (e.ymax == y ||
                        (!ET[ET.maxY].IsEmpty() && e.ymax == y + 1))
                        AET.Delete(e);
                    else
                        e.x += e.delta;

                    e = e.next;
                }

                y++;
            } while (!AET.IsEmpty() || !ET[ET.maxY].IsEmpty());
        }

        public static Color? GetIFillColor(IFillablePolygon poly,
            int x, int y, float kd, float ks, int m,
            Vector3 light, Vector3 lightColor,
            Func<float, float, float, Vector3> ObjectColor)
        {
            float z = poly.CalculateZ(x, y);
            (float u, float v, float w) =
                poly.GetBarycentricCoordsGlobal(new Vector3(x, y, z));
            Vector3 IO = ObjectColor(u, v, w);

            Vector3 IL = lightColor;
            Vector3 V = new Vector3(0F, 0F, 1F);
            Vector3 N = poly.GetNVector(x, y, z);
            Vector3 L = light - new Vector3(x, y, z);
            Vector3 R = Vector3.Normalize(2 * Vector3.Dot(N, L) * N - L);
            N = Vector3.Normalize(N);
            L = Vector3.Normalize(L);

            float cosNL = (float)Math.Cos(MathUtil.GetAngle(N, L));
            double cosVR = Math.Cos(MathUtil.GetAngle(V, R));
            Vector3 I = kd * IL * IO * (cosNL >= 0 ? cosNL : 0) +
                ks * IL * IO * (cosVR >= 0 ? (float)Math.Pow(cosVR, m) : 0);

            return Color.FromArgb(
                I.X <= 1 ? (int)(I.X * 255) : 255,
                I.Y <= 1 ? (int)(I.Y * 255) : 255,
                I.Z <= 1 ? (int)(I.Z * 255) : 255
                );
        }

        public static void DrawTriangles(Graphics g, List<Triangle> mesh)
        {
            foreach (var t in mesh)
                DrawTriangle(g, t, Pens.Magenta);
        }

        public static void DrawTriangle(Graphics g, Triangle t, Pen p)
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

        public static void DrawControlPts(Graphics g, Vertex[,] ControlVertexes)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Vertex v = ControlVertexes[j, i];
                    if (j < 3)
                        g.DrawLine(CONTROL_NET_PEN,
                        new Point((int)v.X, (int)v.Y),
                            new Point((int)ControlVertexes[j + 1, i].X, (int)(ControlVertexes[j + 1, i].Y)));
                    if (i < 3)
                        g.DrawLine(CONTROL_NET_PEN,
                        new Point((int)v.X, (int)v.Y),
                            new Point((int)ControlVertexes[j, i + 1].X, (int)(ControlVertexes[j, i + 1].Y)));

                    g.FillEllipse(Brushes.DarkGreen, v.X - 5, v.Y - 5, 10, 10);
                }
            }
        }

        public static void DrawPrecisionPts(Graphics g, Vertex[,]? PrecisionVertexes)
        {
            if (PrecisionVertexes == null) return;

            foreach (var v in PrecisionVertexes)
                g.FillEllipse(Brushes.Blue, v.X - 5, v.Y - 5, 10, 10);
        }
    }
}
