using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow.Interfaces
{
    public interface IFillablePolygon
    {
        public EdgesTable GetET();
        public float CalculateZ(float x, float y);
        public Vector3 GetNVector(float x, float y, float z);
    }
}
