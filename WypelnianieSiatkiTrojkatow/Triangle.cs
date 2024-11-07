using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow
{
    public class Triangle
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
    }
}
