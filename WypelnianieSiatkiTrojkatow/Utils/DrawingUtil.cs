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

        public static void FillMesh(Bitmap drawArea, List<Triangle> mesh,
            float kd, float ks, int m,
            Vector3 light, Vector3 lightColor,
            Func<float, float, float, Vector3> ObjectColor,
            Func<int, int, (int, int)> CanvasTranslate,
            bool modifyNormal, Color[,] NormalMap,
            Func<bool> isCancelled)
        {
            using (var fastBitmap = drawArea.FastLock())
            {
                Triangle[] cp = new Triangle[mesh.Count];
                mesh.CopyTo(cp);
                Parallel.ForEach(cp, (triangle) =>
                {
                    if (isCancelled()) return;
                    FillPolygon(fastBitmap, triangle, kd, ks, m,
                        light, lightColor,
                        ObjectColor, CanvasTranslate,
                        modifyNormal, NormalMap,
                        isCancelled);
                    if (isCancelled()) return;
                });
                //for (int i = 0; i < mesh.Count; i++)  // todo parallel
                //{
                //    if (isCancelled()) break;
                //    FillPolygon(fastBitmap, mesh[i], kd, ks, m,
                //        light, lightColor,
                //        ObjectColor, CanvasTranslate,
                //        isCancelled);
                //    if (isCancelled()) break;
                //}
            }
        }

        public static void FillPolygon(FastBitmap fastBitmap,
            IFillablePolygon poly, float kd, float ks, int m,
            Vector3 light, Vector3 lightColor,
            Func<float, float, float, Vector3> ObjectColor,
            Func<int, int, (int, int)> CanvasTranslate,
            bool modifyNormal, Color[,] NormalMap,
            Func<bool> isCancelled)
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
                            kd, ks, m, light, lightColor, ObjectColor,
                            modifyNormal, NormalMap)!;
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
            Func<float, float, float, Vector3> ObjectColor,
            bool modifyNormal, Color[,] NormalMap)
        {
            float z = poly.CalculateZ(x, y);
            (float u, float v, float w) =
                poly.GetBarycentricCoords(new Vector3(x, y, z));
            (float uG, float vG, float wG) =
                poly.GetBarycentricCoordsGlobal(new Vector3(x, y, z));
            if (uG is float.NaN) uG = 0;
            if (vG is float.NaN) vG = 0;
            if (wG is float.NaN) wG = 0;

            Vector3 IO = ObjectColor(uG, vG, wG);

            Vector3 IL = lightColor;
            Vector3 V = new Vector3(0F, 0F, 1F);
            Vector3 N = poly.GetNVector(u, v, w);
            N = Vector3.Normalize(N);
            if (modifyNormal)
            {

                int width = NormalMap.GetLength(0);
                int height = NormalMap.GetLength(1);

                uG = 1 - uG;
                Color c = NormalMap[
                    uG <= 0 ? 0 : uG >= 1 ? width - 1 :
                        (int)(uG * width),
                    vG <= 0 ? 0 : vG >= 1 ? height - 1 :
                        (int)(vG * height)
                    ];
                Vector3 Nt = new Vector3(
                    c.R / 127.5F - 1,
                    c.G / 127.5F - 1,
                    c.B / 127.5F - 1
                    );
                Vector3 Pu = poly.GetPuVector(u, v, w);
                Vector3 Pv = poly.GetPvVector(u, v, w);
                Pu = Vector3.Normalize(Pu);
                Pv = Vector3.Normalize(Pv); //todo clean up normalization
                N = new Vector3(
                    Nt.X * Pu.X + Nt.Y * Pv.X + Nt.Z * N.X,
                    Nt.X * Pu.Y + Nt.Y * Pv.Y + Nt.Z * N.Y,
                    Nt.X * Pu.Z + Nt.Y * Pv.Z + Nt.Z * N.Z
                    );
            }

            Vector3 L = light - new Vector3(x, y, z);
            L = Vector3.Normalize(L);
            Vector3 R = Vector3.Normalize(2 * Vector3.Dot(N, L) * N - L);

            float cosNL = Vector3.Dot(N, L);
            float cosVR = Vector3.Dot(V, R);
            Vector3 I = kd * IL * IO * (cosNL >= 0 ? cosNL : 0) +
                ks * IL * IO * (cosVR >= 0 ? (float)Math.Pow(cosVR, m) : 0);

            return Color.FromArgb(
                I.X <= 1 ? (int)(I.X * 255) : 255,
                I.Y <= 1 ? (int)(I.Y * 255) : 255,
                I.Z <= 1 ? (int)(I.Z * 255) : 255
                );
        }

        public static void DrawTriangles(Graphics g, List<Triangle> mesh, Func<bool> isCancelled)
        {
            for (int i = 0; i < mesh.Count; i++)
            {
                if (isCancelled()) break;
                DrawTriangle(g, mesh[i], Pens.Magenta);
            }
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

        public static void DrawControlPts(Graphics g, Vertex[,] ControlVertexes, Func<bool> isCancelled)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (isCancelled()) break;
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

        public static void DrawPrecisionPts(Graphics g, Vertex[,]? PrecisionVertexes, Func<bool> isCancelled)
        {
            if (PrecisionVertexes == null) return;

            for (int j = 0; j < PrecisionVertexes.GetLength(0); j++)
                for (int i = 0; i < PrecisionVertexes.GetLength(1); i++)
                {
                    if (isCancelled()) break;
                    var v = PrecisionVertexes[j, i];
                    g.FillEllipse(Brushes.Blue, v.X - 5, v.Y - 5, 10, 10);
                }
        }
    }
}
