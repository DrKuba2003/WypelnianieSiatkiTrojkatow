using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow.Edges
{
    public class Edge
    {
        public int ymax { get; set; }
        public float x { get; set; }
        public float delta { get; set; }
        public Edge? next { get; set; }

        public Edge(int ymax, float x, float delta, Edge? next = null)
        {
            this.ymax = ymax;
            this.x = x;
            this.delta = delta;
            this.next = next;
        }
    }
}
