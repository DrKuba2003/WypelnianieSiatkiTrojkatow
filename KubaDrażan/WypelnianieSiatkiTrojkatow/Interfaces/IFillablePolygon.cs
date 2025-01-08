using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using WypelnianieSiatkiTrojkatow.Edges;

namespace WypelnianieSiatkiTrojkatow.Interfaces
{
    public interface IFillablePolygon
    {
        public EdgesBucketSorted GetET();
        public float CalculateZ(float x, float y);
        public (float, float, float) GetBarycentricCoords(Vector3 P);
        public (float, float, float) GetBarycentricCoordsGlobal(float u, float v, float w);
        public Vector3 GetNVector(float u, float v, float w);
        public Vector3 GetPuVector(float u, float v, float w);
        public Vector3 GetPvVector(float u, float v, float w);
    }
}
