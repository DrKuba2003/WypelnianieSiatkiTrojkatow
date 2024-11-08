using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow
{
    public class EdgesTable
    {
        public Dictionary<int, EdgeList> ET { get; init; }
        public int minY { get; private set; }
        public int maxY { get; private set; }

        public EdgesTable()
        {
            ET = new Dictionary<int, EdgeList>();
            minY = int.MaxValue;
            maxY = int.MinValue;
        }

        public void Add((int, Edge)? t)
        {
            if (t is null) return;
            (int min, Edge e) = t.Value;

            if (minY == int.MaxValue && maxY == int.MinValue)
            {
                minY = min;
                maxY = min;
            }
            else if (min < minY)
                minY = min;
            else if (min > maxY)
                maxY = min;

            if (!ET.ContainsKey(min))
                ET[min] = new EdgeList();
            ET[min].AddAtEnd(e);
        }

        public EdgeList this[int key]
        {
            get
            {
                if (!ET.ContainsKey(key))
                {
                    return ET[minY];
                }
                return ET[key];
            }
            set => ET[key] = value;
        }

        public bool IsEmpty()
            => ET.Count == 0 || ET[ET.Keys.Max()].IsEmpty();
    }
}
