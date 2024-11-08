using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow
{
    public class Triangle : IPolygon
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

        public ETClass GetET()
        {
            ETClass ET = new();

            ET.Add(GetEdgeET(V1, V2));
            ET.Add(GetEdgeET(V2, V3));
            ET.Add(GetEdgeET(V3, V1));

            return ET;
        }

        private (int, Edge)? GetEdgeET(Vertex v1, Vertex v2)
        {
            // private so no need
            //if ((v1 != V1 && v1 != V2 && v1 != V3) ||
            //    (v2 != V1 && v2 != V2 && v2 != V3)) return null;
            if (Math.Abs(v1.Y - v2.Y) < 1) return null;

            return v1.Y > v2.Y ?
                ((int)v2.Y, new Edge((int)v1.Y, v2.X, (v2.X - v1.X) / (v2.Y - v1.Y))) :
                ((int)v1.Y, new Edge((int)v2.Y, v1.X, (v1.X - v2.X) / (v1.Y - v2.Y)));
        }
    }
}
