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
        public Vector3 PUbr { get; set; }
        public Vector3 PUar { get; set; }
        public Vector3 PVbr { get; set; }
        public Vector3 PVar { get; set; }
        public Vector3 Nbr { get; set; }
        public Vector3 Nar { get; set; }
        public float u { get; set; }
        public float v { get; set; }

        #region HelperFields
        public float X => Par.X;
        public float Y => Par.Y;
        public float Z => Par.Z;
        #endregion

        public Vertex(Vector3 vec, float u = -1, float v = -1)
        {
            this.Pbr = vec;
            this.Par = vec;
            this.u = u;
            this.v = v;
        }

        public Vertex(float x, float y, float z, float u = -1, float v = -1)
            : this(new Vector3(x, y, z), u, v) { }

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
        }
    }
}
