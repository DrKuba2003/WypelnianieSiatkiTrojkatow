using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypelnianieSiatkiTrojkatow
{
    public class ETClass
    {
        public Dictionary<int, EdgeList> ET { get; init; }
        public int minY { get; private set; }
        public int maxY { get; private set; }

        public ETClass()
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
                ET[min] = new EdgeList();
            }
            else if (min < minY)
            {
                for (int i = min; i < minY; i++) ET[i] = new EdgeList();
                minY = min;
            }
            else if (min > maxY)
            {
                for (int i = maxY + 1; i <= min; i++) ET[i] = new EdgeList();
                maxY = min;
            }

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
