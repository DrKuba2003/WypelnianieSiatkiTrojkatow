using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using WypelnianieSiatkiTrojkatow.Edges;
using WypelnianieSiatkiTrojkatow.Interfaces;

namespace WypelnianieSiatkiTrojkatow
{
    public class Triangle : IFillablePolygon
    {
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
        public Vertex V3 { get; set; }

        public Triangle(Vertex V1, Vertex V2, Vertex V3)
        {
            this.V1 = V1;
            this.V2 = V2;
            this.V3 = V3;
        }

        public (float, float) GetMinMaxY()
        {
            float min = float.MaxValue, max = float.MinValue;
            foreach (float y in new float[] { V1.Y, V2.Y, V3.Y })
            {
                if (y < min) min = y;
                if (y > max) max = y;
            }

            return (min, max);
        }

        public EdgesBucketSorted GetET()
        {
            EdgesBucketSorted ET = new();

            ET.Add(GetEdgeET(V1, V2));
            ET.Add(GetEdgeET(V2, V3));
            ET.Add(GetEdgeET(V3, V1));

            return ET;
        }

        private (int, Edge)? GetEdgeET(Vertex v1, Vertex v2)
        {
            if (Math.Abs(v1.Y - v2.Y) < 1) return null;

            float delta = (v2.X - v1.X) / (v2.Y - v1.Y);

            return v1.Y > v2.Y ?
                ((int)v2.Y, new Edge((int)v1.Y, v2.X, delta)) :
                ((int)v1.Y, new Edge((int)v2.Y, v1.X, delta));
        }

        public float CalculateZ(float x, float y)
        {
            float det = (V2.Y - V3.Y) * (V1.X - V3.X) + (V3.X - V2.X) * (V1.Y - V3.Y);

            float l1 = ((V2.Y - V3.Y) * (x - V3.X) + (V3.X - V2.X) * (y - V3.Y)) / det;
            float l2 = ((V3.Y - V1.Y) * (x - V3.X) + (V1.X - V3.X) * (y - V3.Y)) / det;
            float l3 = 1.0f - l1 - l2;

            return l1 * V1.Z + l2 * V2.Z + l3 * V3.Z;
        }



        public (float, float, float) GetBarycentricCoords(Vector3 P)
        {
            double area = GetArea();
            float u = (float)(Triangle.GetTriangleArea(V2.Par, V3.Par, P) / area);
            float v = (float)(Triangle.GetTriangleArea(V1.Par, V3.Par, P) / area);
            float w = 1 - u - v;
            return (u, v, w);
        }
        public (float, float, float) GetBarycentricCoordsGlobal(float u, float v, float w)
        {
            float uGlobal = u * V1.u + v * V2.u + w * V3.u;
            float vGlobal = u * V1.v + v * V2.v + w * V3.v;
            float wGlobal = 1 - uGlobal - vGlobal;
             return (uGlobal, vGlobal, wGlobal);
        }

        public double GetArea()
            => GetTriangleArea(V1.Par, V2.Par, V3.Par);

        public static double GetTriangleArea(Vector3 v1, Vector3 v2, Vector3 v3)
        {
            double l1 = Vertex.GetLength(v1, v2);
            double l2 = Vertex.GetLength(v2, v3);
            double l3 = Vertex.GetLength(v3, v1);
            double s = (l1 + l2 + l3) / 2;
            return Math.Sqrt(
                s * 
                (s - l1) * 
                (s - l2) * 
                (s - l3)
                );
        }
        public Vector3 GetNVector(float u, float v, float w)
        {
            return u * (Vector3)V1.Nar! + v * (Vector3)V2.Nar! + w * (Vector3)V3.Nar!;
        }
        public Vector3 GetPuVector(float u, float v, float w)
        {
            return u * (Vector3)V1.PUar! + v * (Vector3)V2.PUar! + w * (Vector3)V3.PUar!;
        }

        public Vector3 GetPvVector(float u, float v, float w)
        {
            return u * (Vector3)V1.PVar! + v * (Vector3)V2.PVar! + w * (Vector3)V3.PVar!;
        }
    }
}
