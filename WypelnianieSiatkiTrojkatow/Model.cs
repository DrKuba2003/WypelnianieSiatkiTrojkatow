using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WypelnianieSiatkiTrojkatow.Utils;

namespace WypelnianieSiatkiTrojkatow
{
    public class Model
    {
        public Vertex[,] ControlVertexes = new Vertex[4, 4];
        public Vertex[,]? PrecisionVertexes;
        public List<Triangle> net = new();

        public Model(string path, int netP, int alfa, int beta)
        {
            CalculateModel(path, netP, alfa, beta);
        }

        public void LoadTriangles(int N)
        {
            if (ControlVertexes == null ||
                ControlVertexes.Length == 0) return;

            net.Clear();

            PrecisionVertexes = new Vertex[N + 1, N + 1];
            float step = 1.0f / N;
            for (int u = 0; u <= N; u++)
            {
                for (int v = 0; v <= N; v++)
                {
                    Vector3 pt = new Vector3(0, 0, 0),
                        pu = new Vector3(0, 0, 0),
                        pv = new Vector3(0, 0, 0);
                    for (int j = 0; j < 4; j++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Vertex V = ControlVertexes[j, i];
                            pt += V.Pbr *
                                (float)(MathUtil.B(j, 3, u * step) *
                                    MathUtil.B(i, 3, v * step));
                            if (j < 3)
                                pu += (ControlVertexes[j + 1, i].Pbr - V.Pbr) *
                                    (float)(MathUtil.B(j, 2, u * step) *
                                        MathUtil.B(i, 3, v * step));

                            if (i < 3)
                                pv += (ControlVertexes[j, i + 1].Pbr - V.Pbr) *
                                    (float)(MathUtil.B(j, 3, u * step) *
                                        MathUtil.B(i, 2, v * step));

                        }
                    }
                    PrecisionVertexes[u, v] = new Vertex(pt, 3 * pu, 3 * pv,
                        u * step, v * step);
                }
            }

            var pts = PrecisionVertexes; // shorter name
            for (int j = 0; j < pts.GetLength(0) - 1; j++)
            {
                for (int i = 0; i < pts.GetLength(1) - 1; i++)
                {
                    net.Add(new Triangle(pts[j, i], pts[j, i + 1], pts[j + 1, i + 1]));
                    net.Add(new Triangle(pts[j, i], pts[j + 1, i], pts[j + 1, i + 1]));
                }
            }
        }

        public void LoadControlPts(String path)
        {
            Array.Clear(ControlVertexes);

            int i = 0;
            foreach (string line in File.ReadLines(path))
            {
                var values = Array.ConvertAll<string, float>(line.Split(), float.Parse);
                ControlVertexes[i / 4, i % 4] = new Vertex(new Vector3(values));
                i++;
            }
        }
        public void RotateVertexes(int alfa, int beta)
        {
            if (PrecisionVertexes is null) return;

            foreach (var v in PrecisionVertexes)
            {
                v.RotateZAxis(alfa);
                v.RotateXAxis(beta, v.Par);
            }

            foreach (var v in ControlVertexes)
            {
                v.RotateZAxis(alfa);
                v.RotateXAxis(beta, v.Par);
            }
        }

        public void CalculateModel(string path, int netP, int alfa, int beta)
        {
            LoadControlPts(path);
            LoadTriangles(netP);
            RotateVertexes(alfa, beta);
        }

    }
}
