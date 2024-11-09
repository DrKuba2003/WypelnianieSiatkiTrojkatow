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
        public Vector3 GetNVector(float x, float y, float z);
    }
}
