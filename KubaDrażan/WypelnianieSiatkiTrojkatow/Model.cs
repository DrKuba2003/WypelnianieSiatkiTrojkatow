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
        public Vector3[,] ControlVertexesOriginal = new Vector3[4, 4];
        public Vertex[,]? PrecisionVertexes;
        public List<Triangle> Mesh = new();
        public List<Vector3> lightPos;

        public int alfa, beta;

        private double radiuss;
        private double angles;
        private bool isAnimationRunnig = false;
        private BackgroundWorker animationBW;

        public bool isLightAnimation {  get; set; }
        public bool isControlPtsAnimation {  get; set; }
        public int netP;


        public Model(string path, int netP, int alfa, int beta, int lightZ,
            ProgressChangedEventHandler animationBw_ProgressChanged,
            bool isLightAnimation, bool isControlPtsAnimation)
        {
            this.alfa = alfa;
            this.beta = beta;
            this.netP = netP;

            CalculateModel(path, netP);

            lightPos = new List<Vector3>();
            // add multiple light sources
            for (int i = 0; i < 1; i++)
                lightPos.Add(new(0, 0, 0));
            ResetLightPos();
            SetLightZ(lightZ);
            animationBW = new BackgroundWorker();
            animationBW.DoWork += animation_DoWork;
            animationBW.ProgressChanged += animationBw_ProgressChanged;
            animationBW.WorkerReportsProgress = true;
            animationBW.WorkerSupportsCancellation = true;
            this.isLightAnimation = isLightAnimation;
            this.isControlPtsAnimation = isControlPtsAnimation;
        }
        private void animation_DoWork(Object sender, DoWorkEventArgs e)
        {
            double angleSpeed = 6,
                 rSpeed = 0.1;

            int t = 1;
            while (!animationBW.CancellationPending && isAnimationRunnig)
            {
                if (isLightAnimation)
                {
                    angles = (angles + angleSpeed) % 360;
                    radiuss += rSpeed;

                    for (int i = 0; i < lightPos.Count; i++)
                    {
                        Vector3 pos = lightPos[i];
                        double a = angles + (i / (float)lightPos.Count) * 360;
                        pos.X = (float)(radiuss * Math.Cos(MathUtil.ToRadians(a)));
                        pos.Y = (float)(radiuss * Math.Sin(MathUtil.ToRadians(a)));
                        lightPos[i] = pos;
                    }
                }

                if (isControlPtsAnimation)
                {
                    for (int j = 0; j < 4; j++) 
                        for (int i = 0; i < 4; i++)
                        {
                            if ((j + i)%2==0)
                            {
                                ControlVertexes[j, i] = new Vertex(new Vector3(
                                    ControlVertexesOriginal[j, i].X,
                                    ControlVertexesOriginal[j, i].Y,
                                    (float)(ControlVertexesOriginal[j, i].Z * ((Math.Sin(t / 5F) + 2)/2) )
                                    ));
                            }
                            else
                            {
                                ControlVertexes[j, i] = new Vertex(new Vector3(
                                    ControlVertexesOriginal[j, i].X,
                                    ControlVertexesOriginal[j, i].Y,
                                    (float)(ControlVertexesOriginal[j, i].Z * (((1-Math.Sin(t / 5F)) + 2)/2) )
                                    ));
                            }
                        }
                }

                if (isLightAnimation || isControlPtsAnimation) 
                    animationBW.ReportProgress(0);
                Thread.Sleep(200);
                t++;
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
            ControlVertexesOriginal = new Vector3[4, 4];

            int i = 0;
            foreach (string line in File.ReadLines(path))
            {
                var values = Array.ConvertAll<string, float>(line.Split(), float.Parse);
                ControlVertexes[i / 4, i % 4] = new Vertex(new Vector3(values));
                ControlVertexesOriginal[i / 4, i % 4] = new Vector3(values);
                i++;
            }
        }

        public void RotateVertexes()
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

        public void CalculateModel(string path, int netP)
        {
            this.netP = netP;
            LoadControlPts(path);
            LoadMesh(netP);
            RotateVertexes();
        }

        public void ResetLightPos()
        {
            radiuss = 200;
            angles = 0;
            for (int i = 0; i < lightPos.Count; i++)
            {
                Vector3 pos = lightPos[i];
                double a = angles + (i / (float)lightPos.Count) * 360;
                pos.X = (float)(radiuss * Math.Cos(MathUtil.ToRadians(a)));
                pos.Y = (float)(radiuss * Math.Sin(MathUtil.ToRadians(a)));
                lightPos[i] = pos;
            }

        }

        public void SetLightZ(int z)
        {
            for (int i = 0; i < lightPos.Count; i++)
            {
                Vector3 pos = lightPos[i];
                pos.Z = z;
                lightPos[i] = pos;
            }
        }

    }
}
