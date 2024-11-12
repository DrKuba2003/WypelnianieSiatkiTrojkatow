using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<Triangle> Mesh = new();
        public Vector3 lightPos;

        private double radiuss;
        private double angles;
        private bool isAnimationRunnig = false;
        private BackgroundWorker animationBW;

        // todo faster animation

        public Model(string path, int netP, int alfa, int beta, int lightZ,
            ProgressChangedEventHandler animationBw_ProgressChanged)
        {
            CalculateModel(path, netP, alfa, beta);

            lightPos = new Vector3();
            ResetLightPos();
            SetLightZ(lightZ);
            animationBW = new BackgroundWorker();
            animationBW.DoWork += animation_DoWork;
            animationBW.ProgressChanged += animationBw_ProgressChanged;
            animationBW.WorkerReportsProgress = true;
            animationBW.WorkerSupportsCancellation = true;

        }
        private void animation_DoWork(Object sender, DoWorkEventArgs e)
        {
            double angleSpeed = 2,
                 rSpeed = 0.2;

            while (!animationBW.CancellationPending && isAnimationRunnig)
            {
                angles = (angles + angleSpeed) % 360;
                radiuss += rSpeed;

                lightPos.X = (float)(radiuss * Math.Cos(MathUtil.ToRadians(angles)));
                lightPos.Y = (float)(radiuss * Math.Sin(MathUtil.ToRadians(angles)));

                animationBW.ReportProgress(0);
                Thread.Sleep(200);
            }
        }

        public bool PauseResumeAnimation()
        {
            if (isAnimationRunnig)
            {
                isAnimationRunnig = false;
                animationBW.CancelAsync();
            }
            else
            {
                isAnimationRunnig = true;
                animationBW.RunWorkerAsync();
            }
            return isAnimationRunnig;
        }

        public void StopAnimation()
        {
            isAnimationRunnig = false;
            animationBW.CancelAsync();
        }

        public void LoadMesh(int N)
        {
            if (ControlVertexes == null ||
                ControlVertexes.Length == 0) return;

            Mesh.Clear();

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
                    Mesh.Add(new Triangle(pts[j, i], pts[j, i + 1], pts[j + 1, i + 1]));
                    Mesh.Add(new Triangle(pts[j, i], pts[j + 1, i], pts[j + 1, i + 1]));
                }
            }
        }

        public void LoadControlPts(string path)
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
            LoadMesh(netP);
            RotateVertexes(alfa, beta);
        }

        public void ResetLightPos()
        {
            lightPos.X = 50;
            lightPos.Y = 0;
            radiuss = 50;
            angles = 0;
        }

        public void SetLightZ(int z) => lightPos.Z = z;

    }
}
