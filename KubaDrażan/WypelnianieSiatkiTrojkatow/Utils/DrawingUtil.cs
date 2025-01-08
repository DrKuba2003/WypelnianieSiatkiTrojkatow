using FastBitmapLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static void FillMesh(
            Bitmap drawArea,
            Model model,
            DrawingParams drawingParams,
            Func<int, int, (int, int)> CanvasTranslate,
            BackgroundWorker bw)
        {
            using (var fastBitmap = drawArea.FastLock())
            {
                Triangle[] cp = new Triangle[model.Mesh.Count];
                model.Mesh.CopyTo(cp);
                Parallel.ForEach(cp, (triangle) =>
                {
                    if (bw.CancellationPending) return;
                    FillPolygon(fastBitmap,
                        triangle, model,drawingParams,
                        CanvasTranslate, bw);
                    if (bw.CancellationPending) return;
                });
            }
        }

        public static void FillPolygon(
            FastBitmap fastBitmap,
            IFillablePolygon poly,
            Model model,
            DrawingParams drawingParams,
            Func<int, int, (int, int)> CanvasTranslate,
            BackgroundWorker bw)
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
                if (bw.CancellationPending) break;

                Edge? e = AET.head;
                while (e is not null && e.next is not null)
                {
                    for (int x = (int)e.x; x <= (int)e.next.x; x++)
                    {
                        Color? c = GetIFillColor((Triangle)poly,
                            x, y, model, drawingParams);
                        if (c is null) continue;

                        (int xT, int yT) = CanvasTranslate(x, y);
                        if (xT >= 0 && xT < fastBitmap.Width &&
                            yT >= 0 && yT < fastBitmap.Height)
                            fastBitmap.SetPixel(xT, yT, (Color)c);
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
            } while ((!AET.IsEmpty() || !ET[ET.maxY].IsEmpty()) && !bw.CancellationPending);
        }

        public static Color? GetIFillColor(
            IFillablePolygon poly,
            int x, 
            int y,
            Model model,
            DrawingParams drawingParams)
        {
            float z = poly.CalculateZ(x, y);
            (float u, float v, float w) =
                poly.GetBarycentricCoords(new Vector3(x, y, z));
            //if (u < 0 || u > 1 ||
            //    v < 0 || v > 1 ||
            //    w < 0 || w > 1) return null; // pt outside triangle

            (float uG, float vG, float wG) =
                poly.GetBarycentricCoordsGlobal(u, v, w);
            if (uG is float.NaN) uG = 0;
            if (vG is float.NaN) vG = 0;
            if (wG is float.NaN) wG = 0;

            Vector3 IO;
            if (drawingParams.isSolidColor)
            {
                IO = drawingParams.objectColor;
            }
            else
            {
                int width = drawingParams.textureArr.GetLength(0);
                int height = drawingParams.textureArr.GetLength(1);

                Color c = drawingParams.textureArr[
                    uG < 0 || uG is float.NaN ? 0 :
                    uG >= 1 ? width - 1 :
                        (int)(uG * width),
                    vG < 0 || vG is float.NaN ? 0 :
                    vG >= 1 ? height - 1 :
                        (int)(vG * height)
                    ];
                IO = new Vector3(c.R, c.G, c.B) / 255F;
            }

            Vector3 V = new Vector3(0F, 0F, 1F);
            Vector3 N = Vector3.Normalize(poly.GetNVector(u, v, w));
            if (drawingParams.isModifyNormalVec)
            {

                int width = drawingParams.normalMapArr.GetLength(0);
                int height = drawingParams.normalMapArr.GetLength(1);

                Color c = drawingParams.normalMapArr[
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

                Vector3 Pu = Vector3.Normalize(poly.GetPuVector(u, v, w));
                Vector3 Pv = Vector3.Normalize(poly.GetPvVector(u, v, w));
                N = new Vector3(
                    Nt.X * Pu.X + Nt.Y * Pv.X + Nt.Z * N.X,
                    Nt.X * Pu.Y + Nt.Y * Pv.Y + Nt.Z * N.Y,
                    Nt.X * Pu.Z + Nt.Y * Pv.Z + Nt.Z * N.Z

                    );
            }
            Vector3 I = new Vector3(0, 0, 0);
            // przygotowane na pare zrodel swiatla
            for (int i = 0; i < model.lightPos.Count; i++)
            {
                Vector3 pos = model.lightPos[i];
                Vector3 L = Vector3.Normalize(pos - new Vector3(x, y, z));
                Vector3 R = Vector3.Normalize(2 * Vector3.Dot(N, L) * N - L);
                Vector3 IL = drawingParams.lightColor;
                if (drawingParams.isDrawLightReflektor)
                    IL *= (float)Math.Pow(Vector3.Dot(L, Vector3.Normalize(pos)), drawingParams.reflektorM);

                float cosNL = Vector3.Dot(N, L);
                float cosVR = Vector3.Dot(V, R);
                Vector3 ILXIO = IL * IO;
                I += drawingParams.kd * ILXIO * (cosNL >= 0 ? cosNL : 0) +
                    drawingParams.ks * ILXIO * (cosVR >= 0 ? (float)Math.Pow(cosVR, drawingParams.m) : 0);
            }

            return Color.FromArgb(
                Math.Clamp((int)(I.X * 255), 0, 255),
                Math.Clamp((int)(I.Y * 255), 0, 255),
                Math.Clamp((int)(I.Z * 255), 0, 255)
                );
        }

        public static void DrawTriangles(Graphics g, List<Triangle> mesh, BackgroundWorker bw)
        {
            for (int i = 0; i < mesh.Count; i++)
            {
                if (bw.CancellationPending) break;
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

        public static void DrawControlPts(Graphics g, Vertex[,] ControlVertexes, BackgroundWorker bw)
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (bw.CancellationPending) break;
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

        public static void DrawPrecisionPts(Graphics g, Vertex[,]? PrecisionVertexes, BackgroundWorker bw)
        {
            if (PrecisionVertexes == null) return;

            for (int j = 0; j < PrecisionVertexes.GetLength(0); j++)
                for (int i = 0; i < PrecisionVertexes.GetLength(1); i++)
                {
                    if (bw.CancellationPending) break;
                    var v = PrecisionVertexes[j, i];
                    g.FillEllipse(Brushes.Blue, v.X - 5, v.Y - 5, 10, 10);
                }
        }
    }
}
