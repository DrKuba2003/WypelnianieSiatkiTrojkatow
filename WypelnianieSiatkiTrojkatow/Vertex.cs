using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WypelnianieSiatkiTrojkatow.Utils;

namespace WypelnianieSiatkiTrojkatow
{
    public class Vertex
    {

        public Vector3 Pbr { get; set; }
        public Vector3 Par { get; set; }
        public Vector3? PUbr { get; set; }
        public Vector3? PUar { get; set; }
        public Vector3? PVbr { get; set; }
        public Vector3? PVar { get; set; }
        public Vector3? Nbr { get; set; }
        public Vector3? Nar { get; set; }
        public float u { get; set; }
        public float v { get; set; }

        #region HelperFields
        public float X => Par.X;
        public float Y => Par.Y;
        public float Z => Par.Z;
        #endregion

        public Vertex(Vector3 P, Vector3? Pu = null, Vector3? Pv = null, float u = -1, float v = -1)
        {
            this.Pbr = P;
            this.Par = P;
            this.PUbr = Pu;
            this.PUar = Pu;
            this.PVbr = Pv;
            this.PVar = Pv;
            this.u = u;
            this.v = v;

            if (Pu is not null && Pv is not null)
            {
                Nbr = Pu * Pv;
                Nar = Nbr;
            }
        }

        public void RotateZAxis(float angle, Vector3? vec = null)
        {
            Vector3 v = vec is null ? Pbr : (Vector3)vec;
            var rad = MathUtil.ToRadians(angle);
            var m = new Matrix4x4(
                (float)Math.Cos(rad), (float)-Math.Sin(rad), 0, 0,
                (float)Math.Sin(rad), (float)Math.Cos(rad), 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 0);
            Par = Vector3.Transform(v, m);
            if (PUbr is not null) PUar = Vector3.Transform((Vector3)PUbr, m);
            if (PVbr is not null) PVar = Vector3.Transform((Vector3)PVbr, m);
            if (Nbr is not null) Nar = Vector3.Transform((Vector3)Nbr, m);
        }
        public void RotateXAxis(float angle, Vector3? vec = null)
        {
            Vector3 v = vec is null ? Pbr : (Vector3)vec;
            var rad = MathUtil.ToRadians(angle);
            var m = new Matrix4x4(
                1, 0, 0, 0,
                0, (float)Math.Cos(rad), (float)-Math.Sin(rad), 0,
                0, (float)Math.Sin(rad), (float)Math.Cos(rad), 0,
                0, 0, 0, 0);
            Par = Vector3.Transform(v, m);
            if (PUbr is not null) PUar = Vector3.Transform((Vector3)PUbr, m);
            if (PVbr is not null) PVar = Vector3.Transform((Vector3)PVbr, m);
            if (Nbr is not null) Nar = Vector3.Transform((Vector3)Nbr, m);
        }
    }
}
