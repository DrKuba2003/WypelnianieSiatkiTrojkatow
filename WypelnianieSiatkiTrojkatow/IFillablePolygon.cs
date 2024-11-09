using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow
{
    public interface IFillablePolygon
    {
        public EdgesTable GetET();
        public Vector3 GetNVector(int x, int y);
    }
}
